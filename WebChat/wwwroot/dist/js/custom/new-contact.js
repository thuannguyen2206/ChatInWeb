
$(document).ready(function () {
    $("#contact").select2({
        language: 'vi',
        placeholder: 'Tìm bạn bè',
        ajax: {
            url: '/Contact/GetListToNewContact',
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
                    }),
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

$('#btn-new-contact').off("click").on("click", function () {
    let contact = $('#contact').val();
    let messageWelcome = $('#welcome').val();
    AddContact(contact, messageWelcome);
});

function AddContact(contact, messageWelcome) {
    if (contact != null) {
        $.ajax({
            type: "post",
            url: "/Contact/SendNotifyAddNewContact",
            //contentType: "application/json; charset=utf-8",
            data: { contactId: contact, message: messageWelcome },
            //dataType: "json",
            success: function (res) {
                if (res === true) {
                    //console.log(res);
                }
            }
        });
    }
}

