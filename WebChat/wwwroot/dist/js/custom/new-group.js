
$(document).ready(function () {
    $("#participant").select2({
        language: 'vi',
        placeholder: 'Tìm kiếm',
        ajax: {
            url: '/Discussion/GetMyContacts',
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return {
                    keyword: params.term,
                    page: params.page || 1
                };
            },
            processResults: function (data, page) {
                return {
                    pagination: {
                        more: (data.length > 0) ? true : false
                    },
                    results: $.map(data, function (item) {
                        return {
                            id: item.contactId,
                            text: item.userName,
                            html: '<div><img src="' + item.avatarLink + '" class="avatar-md" /> ' + item.userName + '</div>',
                        };
                    })
                };
            }
        },
        templateResult: function (data) {
            return data.html;
        },
        escapeMarkup: function (m) {
            return m;
        }
    });
});

$('#btn-create-group').click(function (event) {
    let members = $('#participant').val();
    let groupName = $('#group-name').val();
    CreateGroup(members, groupName);
});

function CreateGroup(members, groupName) {
    if (members != null && members.length > 0) {
        let formData = {
            NameGroup: groupName,
            ListMembers: members
        };

        $.ajax({
            type: "POST",
            url: "/Discussion/CreateNewChat",
            //contentType: "application/json",
            data: formData,
            //dataType: 'json',
            success: function (res) {
                if (res.status === true) {
                    //$('#side_content').html(res.html.result);
                } else {
                    //onsole.log("Nothing");
                }
            }
        });
    }
}