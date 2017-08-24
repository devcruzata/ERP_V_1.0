$(document).ready(function () {
    //setInterval('getLeadAssignNotification()', 2000);
    //setInterval('getTaskAssignNotification()', 2000);
    setInterval('getTotalNotification()', 10000);
    //getLeadAssignNotification();
    //getTaskAssignNotification();
});
//$(document).ready(function () {
//    $('#dropdownMenu1').click(function () {
//        alert();
//        showNotification();
//    });
//});


function showNotification() {
    //$('.loaderNf').show();    
    //getLeadAssignNotification();
    //getTaskAssignNotification();
    //ResetTotalNotification();
    //$('.loaderNf').hide();
    getNotificationData()
}

function getNotificationData() {
    $('.loaderNf').show();
    $.ajax({
        url: '/Notification/GetNotificationData',
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: {},
        async: true,
        processData: false,
        cache: false,
        success: function (data) {
            if (data != '') {                
                $("#nonfMessage").hide();
                $("#nfBadge").html('');
                $("#nfBadge").hide();
                if (data.totalLeadAssigned != null) {
                    alert(data.totalLeadAssigned);
                    $('#ntbar').append(createALMarkup(data.totalLeadAssigned));
                }

                if (data.totalTaskAssigned != null) {
                    $('#ntbar').append(createATMarkup(data.totalTaskAssigned));
                }
                
                $('#clrAll').show();
                $('.loaderNf').hide();
            }
            else {
                $('.loaderNf').hide();
            }
        },
        error: function (xhr) {
            $('.loaderNf').hide();
            $('#errorMessage').html("No connection");
        }
    });
}

function getTotalNotification() {
    $.ajax({
        url: '/Notification/GetTotalNotification',
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: {},
        async: true,
        processData: false,
        cache: false,
        success: function (data) {
            if (data != '') {
                //var notificationBAdge = '<span class="badge bg-danger">'+data+'</span>'
                $("#nfBadge").html('');
                $("#nfBadge").html(data);
                $("#nfBadge").show();
            }
            else {
            }
        },
        error: function (xhr) {
            $('#errorMessage').html("No connection");
        }
    });
}

function ResetTotalNotification() {
    
    $.ajax({
        url: '/Notification/ResetTotalNotification',
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: {},
        async: true,
        processData: false,
        cache: false,
        success: function (data) {
            if (data == 'success') {
                $("#nfBadge").html('');
                $("#nfBadge").hide();
            }
            else {
            }
        },
        error: function (xhr) {
            $('#errorMessage').html("No connection");
        }
    });
}

function clearAllNotifications() {
    var nonfmsg = '<li class="media" id="nonfMessage"><h5 class="dropdown-header">You dont have any notifications</h5></li>';
    $('#ntbar').html('');
    $('#ntbar').html(nonfmsg);
    $('#clrAll').hide();
}

function getLeadAssignNotification() {
    $.ajax({
        url: '/Notification/GetLeadAssignToNotification',
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: {},
        async: true,
        processData: false,
        cache: false,
        success: function (data) {
            if (data != '') {
                // $('#taskTable').html('');
                // $("#dropdownMenu1").appendTo('<span class="badge bg-danger">2</span>');
                $("#nonfMessage").hide();
                $('#ntbar').append(createALMarkup(data));
                $('#clrAll').show();
            }
            else {              
            }
        },
        error: function (xhr) {
            $('#errorMessage').html("No connection");
        }
    });
}

function getTaskAssignNotification() {
    $.ajax({
        url: '/Notification/GetTaskAssignToNotification',
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: {},
        async: true,
        processData: false,
        cache: false,
        success: function (data) {
            if (data != '') {
                // $('#taskTable').html('');
                $("#nonfMessage").hide();
                $('#ntbar').append(createATMarkup(data));
                $('#clrAll').show();
            }
            else {
            }
        },
        error: function (xhr) {
            $('#errorMessage').html("No connection");
        }
    });
}

function createALMarkup(noOfleads) {
    var mr = '<li class="media">' +
                            '<a href="javascript:;">' +
                                //'<div class="media-left avatar"><img src="@Url.Content("~/Content/build/images/users/17.jpg")" alt="" class="media-object img-circle"><span class="status bg-warning"></span></div>'+
                                '<div class="media-body">' +
                                    '<h6 class="media-heading">Lead Assigned</h6>' +
                                    '<p class="text-muted mb-0">' + noOfleads + ' Leads Assigned to you.</p>' +
                                '</div>' +
                                //'<div class="media-right text-nowrap">'+
                                //    '<time datetime="2015-12-10T20:27:48+07:00" class="fs-11">5 mins</time>'+
                                //'</div>'+
                            '</a>' +
                        '</li>';
    return mr;
}

function createATMarkup(noOfTasks) {
    var mr = '<li class="media">' +
                            '<a href="javascript:;">' +
                                //'<div class="media-left avatar"><img src="@Url.Content("~/Content/build/images/users/17.jpg")" alt="" class="media-object img-circle"><span class="status bg-warning"></span></div>'+
                                '<div class="media-body">' +
                                    '<h6 class="media-heading">Task Assigned</h6>' +
                                    '<p class="text-muted mb-0">' + noOfTasks + ' Task Assigned to you.</p>' +
                                '</div>' +
                                //'<div class="media-right text-nowrap">'+
                                //    '<time datetime="2015-12-10T20:27:48+07:00" class="fs-11">5 mins</time>'+
                                //'</div>'+
                            '</a>' +
                        '</li>';
    return mr;
}