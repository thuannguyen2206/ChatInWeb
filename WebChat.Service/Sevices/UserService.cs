using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebChat.Common.Utilities.Constants;
using WebChat.Common.Utilities.Enums;
using WebChat.Common.Utilities.Storage;
using WebChat.Common.ViewModels.User;
using WebChat.DataAccess.EF;
using WebChat.Entities.Model;
using WebChat.Service.IServices;

namespace WebChat.Service.Sevices
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IFileStorage _fileStorage;
        private readonly WebChatDbContext _context;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager,
            IFileStorage fileStorage, WebChatDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _fileStorage = fileStorage;
            _context = context;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public UserViewModel GetById(Guid id)
        {
            var result = _context.Users.FirstOrDefault(x => x.Id == id);
            return new UserViewModel()
            {
                Id = result.Id,
                Username = result.UserName,
                Email = result.Email,
                Address = result.Address,
                FirstName = result.FirstName,
                LastName = result.LastName,
                AvatarLink = result.AvatarLink,
                Phone = result.PhoneNumber
            };
        }

        public async Task<UserViewModel> GetByName(string username)
        {
            var result = await _userManager.FindByNameAsync(username);
            return new UserViewModel()
            {
                Id = result.Id,
                Username = result.UserName,
                Email = result.Email,
                Address = result.Address,
                FirstName = result.FirstName,
                LastName = result.LastName,
                AvatarLink = result.AvatarLink,
                Phone = result.PhoneNumber
            };
        }

        public async Task<string> GetAvatar(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return string.Empty;

            return user.AvatarLink;
        }

        public async Task<IdentityResult> UpdateProfile(UserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id.ToString());

            user.PhoneNumber = model.Phone;
            user.Address = model.Address;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            if (model.File != null)
            {
                user.AvatarLink = await SaveFile(model.File, "\\img\\");
            }

            return await _userManager.UpdateAsync(user);
        }

        public async Task<string> SaveFile(IFormFile file, string path)
        {
            string uniqueFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + file.FileName.Trim();
            await _fileStorage.SavaFileAsync(file.OpenReadStream(), string.Concat(path + uniqueFileName));
            return _fileStorage.GetFileUrl(string.Concat(path + uniqueFileName));
        }

        public List<ContactViewModel> GetListContacts(Guid userId, string keyword = null, int pageIndex = 1)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);
            if (user == null)
            {
                return null;
            }

            var queryUser = _context.Users.AsQueryable();
            var queryContact = _context.Contacts.Where(x => !x.IsBlock).AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Trim().ToLower();
                queryUser = queryUser.Where(x => x.UserName.ToLower().Contains(keyword));
            }

            var queryResult = (from a in queryUser
                               join b in queryContact on user.Id equals b.UserId
                               where a.IsLocked == false && a.Id == b.FriendId
                               select new ContactViewModel()
                               {
                                   Address = a.Address,
                                   ContactId = a.Id,
                                   AvatarLink = a.AvatarLink,
                                   UserName = a.UserName,
                                   FirstName = a.FirstName,
                                   Status = a.Status,
                                   CreateAt = a.DateCreate
                               }).OrderBy(x => x.UserName)
                              .Skip((pageIndex - 1) * SystemConstant.PageSize)
                              .Take(SystemConstant.PageSize).ToList();

            return queryResult;
        }

        public async Task PermanentDeleted(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        public async Task<ChangePasswordResult> ChangePassword(ChangePasswordViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id.ToString());
            var checkPass = await _userManager.CheckPasswordAsync(user, model.PresentPassword);
            if (checkPass)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.PresentPassword, model.NewPassword);
                if (result.Succeeded)
                    return ChangePasswordResult.Success;
                return ChangePasswordResult.Error;
            }
            return ChangePasswordResult.Invalid;
        }

        public void AddConnection(ConnectionViewModel model)
        {
            var checkConnection = _context.UserConnections.FirstOrDefault(x => x.ConnectionId == model.ConnectionId);
            if (checkConnection != null)
                return;
            var entity = new UserConnection()
            {
                ConnectionId = model.ConnectionId,
                CreatedAt = DateTime.Now,
                UserId = model.UserId,
                IPAddress = model.IPAddress
            };
            var result = _context.UserConnections.Add(entity);
            _context.SaveChanges();
        }

        // Lấy danh sách userId trong group
        // Lặp qua danh sách để lấy ra tất cả connection của các user
        public List<string> GetListConnectionIds(Guid groupId)
        {
            var listUserIdInGroup = _context.UserInChatGroups.Where(x => x.GroupId == groupId).Select(x => x.UserId).ToList();
            var totalConnection = _context.UserConnections.Where(x => listUserIdInGroup.Contains(x.UserId)).Select(x => x.ConnectionId).ToList();
            return totalConnection;
        }

        public void RemoveConnections(Guid userId)
        {
            var userConnections = _context.UserConnections.Where(x => x.UserId == userId).ToList();
            var connectionOverTimes = new List<UserConnection>();
            if (userConnections != null && userConnections.Count > 0)
            {
                foreach (var item in userConnections)
                {
                    if ((DateTime.Now - item.CreatedAt.Value).TotalHours > 24)
                    {
                        connectionOverTimes.Add(item);
                    }
                }
                _context.UserConnections.RemoveRange(connectionOverTimes);
                _context.SaveChanges();
            }
        }

        // Get current contacts (include this user logon)
        // Iterate through the database to get a list of users that are not in the contact list above
        public List<ContactViewModel> GetListToAddContact(Guid userId, string keyword, int pageIndex)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);
            if (user == null)
            {
                return null;
            }
            var myContacts = _context.Contacts.Where(x => x.UserId == user.Id).Select(x => x.FriendId).ToList();
            myContacts.Add(user.Id);
            var queryUser = _context.Users.Where(x => x.IsAdmin == false && !x.IsLocked).AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Trim().ToLower();
                queryUser = queryUser.Where(x => x.UserName.ToLower().Contains(keyword));
            }

            queryUser = queryUser.Where(x => !myContacts.Contains(x.Id));

            var results = queryUser.OrderBy(x => x.UserName)
                .Select(x => new ContactViewModel()
                {
                    ContactId = x.Id,
                    AvatarLink = x.AvatarLink,
                    UserName = x.UserName,
                    Address = x.Address
                }).Skip((pageIndex - 1) * SystemConstant.PageSize)
                .Take(SystemConstant.PageSize).ToList();

            return results;
        }

        public NewContactResult AcceptNewContactRequest(Guid userId, Guid contactId)
        {
            try
            {
                // Nếu đã có contact thì return
                var check = _context.Contacts.Any(x => (x.UserId == userId && x.FriendId == contactId) || (x.UserId == contactId && x.FriendId == userId));
                if (check)
                {
                    return NewContactResult.Exist;
                }
                // tạo 2 entity vì phải thêm contact cho cả 2 user (A->B thì B->A)
                var entity = new Contact()
                {
                    CreatedAt = DateTime.Now,
                    UserId = userId,
                    FriendId = contactId,
                    IsBlock = false
                };
                var entity2 = new Contact()
                {
                    CreatedAt = DateTime.Now,
                    UserId = contactId,
                    FriendId = userId,
                    IsBlock = false
                };
                List<Contact> listEntity = new List<Contact>() { entity, entity2 };
                _context.Contacts.AddRange(listEntity);
                _context.SaveChanges();
                return NewContactResult.Successed;
            }
            catch (Exception)
            {
                return NewContactResult.Error;
            }
        }

        public bool ChangeStatus(Guid userId, bool status)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);
            if (user == null)
                return false;

            user.Status = status;
            _context.Users.Update(user);
            _context.SaveChanges();
            return true;
        }

        public List<string> GetConnectionIdsWhenLog(Guid userId)
        {
            var listContacts = _context.Contacts.Where(x => x.UserId == userId).Select(x => x.FriendId).ToList();
            var totalConnection = (from a in _context.UserConnections
                                   where listContacts.Contains(a.UserId)
                                   select a.ConnectionId).ToList();
            return totalConnection;
        }

        public List<string> GetUserConnectionIds(Guid userId)
        {
            var totalConnection = _context.UserConnections.Where(x => x.UserId == userId).Select(x => x.ConnectionId).ToList();
            return totalConnection;
        }

        // get list friend of user (the condition is that the user is not blocked) and not belong this group
        public List<ContactViewModel> GetListToAddMember(Guid userId, Guid groupId, string keyword = null, int pageIndex = 1)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);
            if (user == null)
            {
                return null;
            }

            // get my contacts (include me)
            var myContacts = _context.Contacts.Where(x => x.UserId == user.Id && !x.IsBlock).Select(x => x.FriendId).ToList();
            myContacts.Add(user.Id);
            // get list members of group
            var memberOfGroups = _context.UserInChatGroups.Where(x => x.GroupId == groupId).Select(x => x.UserId).ToList();

            // Filter list members not belong group
            var contactExcept = myContacts.Except(memberOfGroups).ToList();
            //var contactExcept = myContacts.Where(x => !memberOfGroups.Contains(x)).ToList();

            var queryUser = _context.Users.Where(x => x.IsAdmin == false && x.IsLocked == false).AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Trim().ToLower();
                queryUser = queryUser.Where(x => x.UserName.ToLower().Contains(keyword));
            }

            var result = queryUser.Where(x => contactExcept.Contains(x.Id)).OrderBy(x => x.UserName)
                .Select(x => new ContactViewModel()
                {
                    ContactId = x.Id,
                    AvatarLink = x.AvatarLink,
                    UserName = x.UserName
                }).Skip((pageIndex - 1) * SystemConstant.PageSize)
                .Take(SystemConstant.PageSize).ToList();

            return result;
        }

        public Guid Unfriend(Guid groupId, Guid userId)
        {
            var contactId = _context.UserInChatGroups.Where(x => x.GroupId == groupId && x.UserId != userId).Select(x => x.UserId).FirstOrDefault();
            if (contactId == null)
            {
                return Guid.Empty;
            }
            var contacts = _context.Contacts.Where(x => (x.UserId == userId && x.FriendId == contactId) || (x.UserId == contactId && x.FriendId == userId)).ToList();
            if (contacts.Count > 0)
            {
                _context.Contacts.RemoveRange(contacts);
                _context.SaveChanges();
                return contactId;
            }
            return Guid.Empty;
        }

        public Guid BlockOrUnBlockFriend(Guid groupId, Guid userId)
        {
            var friendId = _context.UserInChatGroups.Where(x => x.GroupId == groupId && x.UserId != userId).Select(x => x.UserId).FirstOrDefault();
            if (friendId == null)
            {
                return Guid.Empty;
            }
            var contact = _context.Contacts.Where(x => x.UserId == friendId && x.FriendId == userId).FirstOrDefault();
            if (contact != null)
            {
                contact.IsBlock = !contact.IsBlock;
                _context.Contacts.Update(contact);
                _context.SaveChanges();
                return friendId;
            }
            return Guid.Empty;
        }

    }
}
