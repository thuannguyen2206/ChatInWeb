var utils = {

    SendMessageItem: function (data, userId) {
        let html = '';
        if (data.senderId === userId) {
            html = `<div class="message me">
                                <div class="text-main">
                                    <div class="text-group me">
                                        <div class="text me">
                                            <p>${data.content}</p>
                                        </div>
                                    </div>

                                </div>
                            </div>`;
        } else {
            html = `<div class="message">
                                <img class="avatar-md" src="${data.avatarLink}" title="${data.userName}" alt="avatar">
                                <div class="text-main">
                                    <div class="text-group">
                                        <div class="text">
                                            <span><b>${data.userName}</b></span>
                                            <p>${data.content}</p>
                                        </div>
                                    </div>

                                </div>
                            </div>`;
            utils.SendNewNoti(data.chatGroupId);
        }
        $('#content .container .chat-content').append(html);
        utils.ScrollLastMessage();
    },

    SendFileItem: function (data, userId) {
        let html = '';
        let content = '';
        if (data.typeFile === 2) { // video
            content = `<video width="320" height="240" controls>
                         <source src="${data.attachedFile}">
                         Your browser does not support the video tag.
                       </video>`;
        }
        else if (data.typeFile === 1) { // image
            content = `<img src="${data.attachedFile}" alt="Image" width="200" height="200">`;
        }
        else { // file
            content = `<div class="attachment">
						<button class="btn attach"><i class="material-icons md-18">insert_drive_file</i></button>
						<div class="file">
							<h5><a href="${data.attachedFile}">${data.fileName}</a></h5>
						</div>
					</div>`;
        }

        if (data.senderId === userId) {
            html = `<div class="message me">
                        <div class="text-main">
                            <div class="text-group me">
                                <div class="text me">
                                    ${content}
                                </div>
                            </div>
                        </div>
                    </div>`;
        } else {
            html = `<div class="message">
                        <img class="avatar-md" src="${data.avatarLink}" title="${data.userName}" alt="avatar">
                        <div class="text-main">
                            <div class="text-group">
                                <div class="text">
                                    <span><b>${data.userName}</b></span>
                                    ${content}
                                </div>
                            </div>
                        </div>
                    </div>`;
            utils.SendNewNoti(data.chatGroupId);
        }
        $('#content .container .chat-content').append(html);
        utils.ScrollLastMessage();
    },

    ScrollLastMessage: function () {
        $('#content').animate({ scrollTop: $('#content')[0].scrollHeight }, 1000);
    },

    DeleteContentNoMessage: function () {
        let check = $('#content').hasClass('empty');
        if (check) {
            $('#content .container .chat-content').html('');
            $('#content').removeClass('empty');
        }
    },

    SendNewNoti: function (groupId) {
        let unread = '';
        let isExistNoti = $(`#chats a#${groupId} div`).hasClass("new");
        if (isExistNoti) {
            unread = $(`#chats a#${groupId} div.new span`).text();
            if (unread !== null && unread !== '') {
                try {
                    unread = parseInt(unread);
                    unread += 1;
                    $(`#chats #${groupId} div.new span`).text(unread);
                } catch (e) {
                    console.log("Too many notification.");
                }
            }
        }
        else {
            unread = 1;
            let text = `<div class="new bg-red">
                        <span>${unread}</span>
                        </div>`;
            $(text).insertAfter(`#chats a#${groupId} div.status`);
        } 
    },

    LogoutStatus: function (userId) {
        let item = $(`#contacts a#${userId} .status i`);
        let status = item.hasClass("online");
        if (status) {
            item.removeClass("online").addClass("offline");
        }
    },

    LoginStatus: function (userId) {
        let item = $(`#contacts a#${userId} .status i`);
        let status = item.hasClass("offline");
        if (status) {
            item.removeClass("offline").addClass("online");
        }
    },

    NewDiscussionItem: function (data) {
        if ($('#chats').children('h5').length > 0) {
            $('#chats').children('h5').remove();
        }
        let html = `<a href="#" id="${data.groupId}" class="filterDiscussions all unread single active">
                         <img class="avatar-md" src="${data.avatarDiscussion}" title="${data.nameDiscussion}" alt="avatar">
                         <div class="status">
                             <i class="material-icons online">fiber_manual_record</i>
                         </div>
                         <div class="data">
                             <h5 style="text-overflow:ellipsis" title="${data.nameDiscussion}">${data.nameDiscussion}</h5>
                             <span>${data.timeSentAsString}</span>
                             <p>${data.shortMessage}</p>
                         </div>
                     </a>`;
        $('#chats').prepend(html);
    },

    NewContactNotiItem: function (data) {
        if ($('#alerts').children('h5').length > 0) {
            $('#alerts').children('h5').remove();
        }
        let html = `<div class="noti">
                         <a href="#" class="filterNotifications all latest notification">
                             <img class="avatar-md" src="${data.avatarLink}" title="${data.userName}" alt="avatar">
                             <div class="status">
                                 <i class="material-icons online">fiber_manual_record</i>
                             </div>
                             <div class="data">
                                 <p>${data.contentNotify}</p>
                                 <span>${data.timeSendAsString}</span>
                             </div>
                         </a>
                         <div class="options row">
                             <button type="button" class="btn button col-5 accept" data-senderid="${data.senderId}" data-notificationid="${data.notificationId}">Đồng ý</button>
                             <div class="col-1"></div>
                             <button type="button" class="btn button col-5 ignore" data-senderid="${data.senderId}" data-notificationid="${data.notificationId}">Bỏ qua</button>
                         </div>
                     </div> `;
        $('#alerts').prepend(html);

    },

    AcceptContactItem: function (data) {
        if ($('#contacts').children('h5').length > 0) {
            $('#contacts').children('h5').remove();
        }
        let html = `<a href="#" id="${data.contactId}" class="filterMembers all online contact">
                        <img class="avatar-md" src="${data.avatarLink}" title="${data.firstName}" alt="avatar">
                        <div class="status">
                            <i class="material-icons ${ data.status == true ? "online" : "offline" }">fiber_manual_record</i>
                        </div>
                        <div class="data">
                            <h5>${data.userName}</h5>
                            <p>${data.address != null ? data.address : ""}</p>
                        </div>
                        <div class="person-add">
                            <i class="material-icons">person</i>
                        </div>
                    </a>`;
        $('#contacts').prepend(html);
    },

    BindNotiInDiscussionItem: function (data) {
        if ($(`#chats #${data.chatGroupId}`).length > 0) {
            $(`#chats #${data.chatGroupId} .data span`).text(data.timeSentAsString);
            $(`#chats #${data.chatGroupId} .data p`).text(data.content);
            let discussion = $(`#chats #${data.chatGroupId}`).outerHTML;
            $(`#chats #${data.chatGroupId}`).remove();
            $('#chats').prepend(discussion);
        }
    },

    DeleteItemById: function (data) {
        if ($("#" + data).length > 0) {
            $("#" + data).remove();
        }
    },

    RemoveGroupItem: function (data) {
        utils.DeleteItemById(data);
        let groupId = $('.hidden-field #chatGroupId').val();
        if (groupId != null && groupId == data) {
            let html = `<div class="babble tab-pane fade active show" id="list-chat">
                           <div class="chat">
                               <div class="content empty">
                                   <div class="container">
                                       <div class="col-md-12">
                                           <div class="no-messages request">
                                               <h3>Welcome to <strong>WebChat</strong></h3>
                                               <h5>Let's start connecting with your friends.</h5>
                                           </div>
                                       </div>
                                   </div>
                               </div>
                           </div>
                       </div>`;
            $("#group-modal").modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $("#nav-tabContent").html(html); 
        }
    },

    DeleteItemWhenBlock: function (data) {
        utils.DeleteItemById(data.friendId);
        utils.RemoveGroupItem(data.groupId);
    },

    UnfriendItem: function (data) {
        for (let id of data) {
            utils.DeleteItemById(id);
        }
    }

}