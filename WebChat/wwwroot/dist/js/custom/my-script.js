
// Load page
$(document).ready(function () {
    $('#nav_discussion').click();
});

$("#nav_contact").click(function () { 
    $.ajax({
        url: '/Contact/GetListContact',
        type: 'GET',
        dataType: 'html',
        success: function (result) {
            $('#side_content').html(result);
            ActiveClass('#members');
        }
    });
});

$("#nav_discussion").click(function () {
    $.ajax({
        url: '/Discussion/GetListDiscussion',
        type: 'GET',
        dataType: 'html',
        success: function (result) {
            $('#side_content').html(result);
            ActiveClass('#discussions');
        }
    });
});

$("#nav_notification").click(function () {
    $.ajax({
        url: '/Notification/GetListNotification',
        type: 'GET',
        dataType: 'html',
        success: function (result) {
            $('#side_content').html(result);
            ActiveClass('#notifications');
        }
    });
});

$("#nav_setting").click(function () {
    $.ajax({
        url: '/Setting/LoadSetting',
        type: 'GET',
        success: function (result) {
            $('#side_content').html(result);
            ActiveClass('#settings');
        }
    });
});

function ActiveClass(selector) {
    $('#sidebar #side_content div').removeClass("active");
    $(selector).addClass("active");
}
