using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebChat.Common.Utilities.Enums;
using WebChat.Common.ViewModels.User;

namespace WebChat.Service.IServices
{
    public interface IUserService
    {
        /// <summary>
        /// User log out
        /// </summary>
        /// <returns></returns>
        Task Logout();

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserViewModel GetById(Guid id);

        /// <summary>
        /// Get user by name
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<UserViewModel> GetByName(string username);

        /// <summary>
        /// Get avatar of user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<string> GetAvatar(Guid userId);

        /// <summary>
        /// Update profile of user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IdentityResult> UpdateProfile(UserViewModel model);

        /// <summary>
        /// Get list contact of user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="keywrod"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        List<ContactViewModel> GetListContacts(Guid userId, string keyword = null, int pageIndex = 1);

        /// <summary>
        /// Permanent delete user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task PermanentDeleted(string email);

        /// <summary>
        /// Change password of user
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return status success, error or invalid</returns>
        Task<ChangePasswordResult> ChangePassword(ChangePasswordViewModel model);

        /// <summary>
        /// Add connection of user 
        /// </summary>
        /// <param name="model"></param>
        void AddConnection(ConnectionViewModel model);

        /// <summary>
        /// Get list connections of group
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns>Total connections of a group</returns>
        List<string> GetListConnectionIds(Guid groupId);

        /// <summary>
        /// Get list connections of specific user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Return list connections of user</returns>
        List<string> GetUserConnectionIds(Guid userId);

        /// <summary>
        /// Get list connections of user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<string> GetConnectionIdsWhenLog(Guid userId);

        /// <summary>
        /// Removed connection of user over 24 hours
        /// </summary>
        /// <param name="userId"></param>
        void RemoveConnections(Guid userId);

        /// <summary>
        /// Get user list to add new contact
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="keywrod"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        List<ContactViewModel> GetListToAddContact(Guid userId, string keyword = null, int pageIndex = 1);

        /// <summary>
        /// Accept request new contact from contact id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="contactId"></param>
        /// <returns></returns>
        NewContactResult AcceptNewContactRequest(Guid userId, Guid contactId);

        /// <summary>
        /// Save file to database
        /// </summary>
        /// <param name="file"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<string> SaveFile(IFormFile file, string path);

        /// <summary>
        /// Change status of user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="status"></param>
        /// <returns>Return true if active, otherwise false</returns>
        bool ChangeStatus(Guid userId, bool status);

        /// <summary>
        /// Get a list of friends who are not in this group
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="groupId"></param>
        /// <param name="keywrod"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        List<ContactViewModel> GetListToAddMember(Guid userId, Guid groupId, string keyword = null, int pageIndex = 1);

        /// <summary>
        /// Unfriend with a contact
        /// </summary>
        /// <param name="groupId">Your group id</param>
        /// <param name="userId">Your id</param>
        /// <returns>Return contact id was deleted, otherwise empty Guid value</returns>
        Guid Unfriend(Guid groupId, Guid userId);

        /// <summary>
        /// Block or unblock friend
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="userId"></param>
        /// <returns>Return friend id was blocked or unblocked</returns>
        Guid BlockOrUnBlockFriend(Guid groupId, Guid userId);

    }
}
