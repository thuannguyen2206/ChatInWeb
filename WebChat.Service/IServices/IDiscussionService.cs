using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebChat.Common.ViewModels.Discussion;

namespace WebChat.Service.IServices
{
    public interface IDiscussionService
    {
        /// <summary>
        /// Get list discussion of users
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="keyword"></param>
        /// <param name="pageIndex"></param>
        /// <returns>Return list discussion of user</returns>
        List<DiscussionViewModel> GetListDiscussions(Guid userId, string keyword = null, int pageIndex = 1);

        /// <summary>
        /// Create new group and add list users to this group
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Guid CreateNewGroup(NewChatViewModel model);
        
        /// <summary>
        /// Get total user of a group
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns>Return total user of group</returns>
        int GetTotalUserInGroup(Guid groupId);

        /// <summary>
        /// Get discussion detail and list members
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        DiscussionDetailViewModel GetDiscussionDetail(Guid groupId, Guid userId, int pageIndex = 1);

        /// <summary>
        /// Update discussion information and add members to group (if can) 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> UpdateDiscussion(DiscussionDetailViewModel model);

        /// <summary>
        /// Load more members when user scroll down in list members of group
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        LoadMemberViewModel LoadMembers(Guid groupId, Guid userId, int pageIndex = 1);

        /// <summary>
        /// Remove a member of specific group
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="memberId"></param>
        /// <returns></returns>
        bool RemoveMember(Guid groupId, Guid memberId);

        /// <summary>
        /// Remove group by id
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns>Return true if successed, otherwise false</returns>
        bool RemoveDiscussion(Guid groupId);

    }
}
