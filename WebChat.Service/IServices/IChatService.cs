using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebChat.Common.ViewModels.Chat;

namespace WebChat.Service.IServices
{
    public interface IChatService
    {
        /// <summary>
        /// Get list message in group chat
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        List<ChatLogViewModel> ListChatLogs(Guid groupId, int pageIndex = 1);

        /// <summary>
        /// Get chat group from specific contact id, if group not exist then create a new group
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="contactId"></param>
        /// <returns></returns>
        ChatGroupViewModel GetChatGroup(Guid userId, Guid contactId);

        /// <summary>
        /// Get chat group from group id
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        ChatGroupViewModel GetChatGroup(Guid groupId);

        /// <summary>
        /// Add message to group
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddChatLog(ChatLogViewModel model);

        /// <summary>
        /// Check exist chat group of two user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="contactId"></param>
        /// <returns></returns>
        Guid CheckChatGroup(Guid userId, Guid contactId);

        /// <summary>
        /// Create new chat group
        /// </summary>
        /// <param name="nameGroup"></param>
        /// <returns></returns>
        Guid CreateChatGroup(string nameGroup);

        /// <summary>
        /// Add list user to group
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="listId"></param>
        /// <returns></returns>
        bool AddUsersToGroup(Guid groupId, List<Guid> listId);

    }
}
