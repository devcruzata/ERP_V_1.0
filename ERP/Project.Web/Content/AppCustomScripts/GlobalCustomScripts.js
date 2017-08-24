

 

//Task Section Scripts*********** 
function GetUserListing() {   
    $.post('/Common/GetUserListing', {}, function (data) {
        if (data != '') {
            $('#GAssignToSelect').empty();
            $.each(data, function (i, foo) {
                $('#GAssignToSelect').append('<option value="' + this.Value + '">' + this.Text + '</option');
            });
        }
    });
}

$('#GTRelateTo').change(function () {
    $.post('/Common/GetRelateToListing', { RelatedTable: $('#GTRelateTo').val() }, function (data) {
        if (data != '') {
            $('#GTRelateToName').empty();
            $.each(data, function (i, foo) {
                $('#GTRelateToName').append('<option value="' + this.Value + '">' + this.Text + '</option');
            });
        }
    });

});
 function GSetAssignTo() {
    $("#AssignTo").val($("#GAssignToSelect").val());
 }

 function GvalidateTaskForm() {
    if ($('#GTTitle').val().trim() == '') {
        $('#error_div_GTTitle').show();
        return false;
    }
    else if ($('#GTRelateTo').val().trim() == '0') {
        $('#error_div_GTRelateTo').show();
        $('#error_div_GTTitle').hide();
        return false;
    }
    else if ($('#GTRelateToName').val().trim() == '0') {
        $('#error_div_GTRelateToName').show();
        $('#error_div_GTRelateTo').hide();
        $('#error_div_GTTitle').hide();
        return false;
    }
    else if ($('#GDescription').val().trim() == '') {
        $('#error_div_GDescription').show();
        $('#error_div_GTRelateToName').hide();
        $('#error_div_GTRelateTo').hide();
        $('#error_div_GTTitle').hide();
        return false;
    }
    else {
        $('#error_div_GDescription').hide();
        $('#error_div_GTTitle').hide();
        return true;
    }
}

 function GSheduleTask() {

    if (GvalidateTaskForm()) {
        $(".se-pre-con").show();
        var notificationFlag = '0';
        if ($("#TNotification").is(':checked')) {
            notification_Flag = '1';
        }
        else {
            notification_Flag = '0';
        }
        alert($('#GTRelateToName').val());
        $.ajax({
            url: '/Task/AjaxAddTask',
            dataType: "html",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ Titele: $("#GTTitle").val(), RelateTo: $('#GTRelateToName').val(), Description: $("#GDescription").val(), notificationFlag: notification_Flag, AssignTo: $("#GAssignToSelect").val(), RelatedTable: $('#GTRelateTo').val() }),
            async: true,
            processData: false,
            cache: false,
            success: function (data) {
                if (data != '') {
                    $("#GTask_Modal").modal('hide');
                    $("#GtaskForm")[0].reset();                   
                    $(".se-pre-con").fadeOut("slow");
                }
                else {
                    $("#GTask_Modal").modal('hide');
                    $("#taskForm")[0].reset();
                    $(".se-pre-con").fadeOut("slow");
                }
            },
            error: function (xhr) {
                $('#errorMessage').html("No connection");
            }
        });
    }
}

 function GCancelTask() {
     $("#GtaskForm")[0].reset();
 }
 function CancelToDo() {
     $("#GDescription").val('');
     $('#GTRelateTo').val("0");
     $("#GTTitle").val('');
     $("#GTRelateToName").val('');
     $("#error_div_GTTitle").hide();
     $("#error_div_GTRelateTo").hide();
     $("#error_div_GTRelateToName").hide();
     $("#error_div_Description").hide();
     $('#TNotification').attr('checked', false);

 }



// Notes Section**********

 $('#GNRelateTo').change(function () {
     $.post('/Common/GetRelateToListing', { RelatedTable: $('#GNRelateTo').val() }, function (data) {
         if (data != '') {
             $('#GNRelateToName').empty();
             $.each(data, function (i, foo) {
                 $('#GNRelateToName').append('<option value="' + this.Value + '">' + this.Text + '</option');
             });
         }
     });

 });

 function GvalidateNotesForm() {
 if ($('#GNRelateTo').val().trim() == '0') {
     $('#error_div_GNRelateTo').show();   
     return false;
 }
 else if ($('#GNRelateToName').val().trim() == '0') {
     $('#error_div_GNRelateToName').show();
     $('#error_div_GNRelateTo').hide();    
     return false;
 }
     else if ($("#GNote").val().trim() == '') {
         $('#error_div_GNote').show();
         $('#error_div_GNRelateToName').hide();
         $('#error_div_GNRelateTo').hide();
         return false;
     }
     else {
         $('#error_div_GNote').hide();
         $('#error_div_GNRelateToName').hide();
         $('#error_div_GNRelateTo').hide();
         return true;
     }
 }
 function GAddNote() {

     if (GvalidateNotesForm()) {
         $(".se-pre-con").show();
         $.ajax({
             url: '/Notes/AjaxAddNote',
             dataType: "html",
             type: "POST",
             contentType: 'application/json; charset=utf-8',
             data: JSON.stringify({ RelateTo: $('#GNRelateToName').val(), Note: $("#GNote").val(), RelatedTable: $('#GNRelateTo').val() }),
             async: true,
             processData: false,
             cache: false,
             success: function (data) {
                 if (data != '') {
                     $("#GNotesModal").modal('hide');
                     $("#GnotesForm")[0].reset();                     
                     $(".se-pre-con").hide();
                     //location.reload();
                 }
                 else {
                     $("#GNotesModal").modal('hide');
                     $("#GnotesForm")[0].reset();
                     $(".se-pre-con").hide();
                     //  location.reload();
                 }
             },
             error: function (xhr) {
                 $(".se-pre-con").hide();
                 $('#errorMessage').html("No connection");
             }
         });
     }
 }

 function CancelGNote() {
     $('#GNRelateToName').val('');
     $("#GNote").val('');
     $('#GNRelateTo').val('0');
     $('#error_div_GNote').hide();
     $('#error_div_GNRelateToName').hide();
     $('#error_div_GNRelateTo').hide();
 }

// Docs Section **************

 $('#GDRelateTo').change(function () {
     $.post('/Common/GetRelateToListing', { RelatedTable: $('#GDRelateTo').val() }, function (data) {
         if (data != '') {
             $('#GDRelateToName').empty();
             $.each(data, function (i, foo) {
                 $('#GDRelateToName').append('<option value="' + this.Value + '">' + this.Text + '</option');
             });
         }
     });

 });

 function GvalidateDocForm() {
     if ($("#GDTitle").val().trim() == '') {
         $('#error_div_GDTitle').show();
         return false;
     }
     else if ($("#GDocument").val().trim() == '') {
         $('#error_div_GDocument').show();
         $('#error_div_GDTitle').hide();
         return false;
     }
     else if ($('#GDRelateTo').val().trim() == '0') {
         $('#error_div_GDRelateTo').show();
         $('#error_div_GDocument').hide();
         $('#error_div_GDTitle').hide();
         return false;
     }
     else if ($('#GDRelateToName').val().trim() == '0') {
         $('#error_div_GDRelateToName').show();
         $('#error_div_GDRelateTo').hide();
         $('#error_div_GDocument').hide();
         $('#error_div_GDTitle').hide();
         return false;
     }
     else {
         $('#error_div_GDRelateToName').hide();
         $('#error_div_GDRelateTo').hide();
         $('#error_div_GDocument').hide();
         $('#error_div_GDTitle').hide();
         return true;
     }
 }
 function GAddDoc() {
     if (GvalidateDocForm()) {
         $(".se-pre-con").show();
         GAddDocument();
     }
 }

 function GAddDocument() {
     // Checking whether FormData is available in browser 
     var RelateTO = $('#GDRelateToName').val();
     var title = $("#GDTitle").val();
     if (window.FormData !== undefined) {

         var fileUpload = $("#GDocument").get(0);
         var files = fileUpload.files;

         // Create FormData object  
         var fileData = new FormData();

         // Looping over all files and add it to FormData object  
         for (var i = 0; i < files.length; i++) {
             fileData.append(files[i].name, files[i]);
         }

         // Adding one more key to FormData object  
         fileData.append('Title', $("#GDTitle").val());
         fileData.append('RelateTo', $('#GDRelateToName').val());
         fileData.append('RelatedTable', $('#GDRelateTo').val());
         // $("#loader").show();
         $.ajax({
             url: '/Doc/AddNewDoc',
             type: "POST",
             contentType: false, // Not to set any content header  
             processData: false, // Not to process data  
             data: fileData,
             success: function (data) {
                 if (data != '') {
                     $("#GDocModal").modal('hide');                     
                     $("#GdocForm")[0].reset();
                     $(".se-pre-con").hide();
                 }
                 else {
                     $("#GDocModal").modal('hide');
                     $("#GdocForm")[0].reset();
                     $(".se-pre-con").hide();
                 }
             },
             error: function (err) {
                 $(".se-pre-con").hide();
                 alert(err.statusText);
             }
         });
     } else {
         alert("FormData is not supported.");
     }
 }

 function CancelGDoc() {
     $("#GDTitle").val('');
     $('#GDRelateToName').val('');
     $('#GDRelateTo').val('0');
     $("#GDocument").val('');
     $('#error_div_GDRelateToName').hide();
     $('#error_div_GDRelateTo').hide();
     $('#error_div_GDocument').hide();
     $('#error_div_GDTitle').hide();
 }

//Event Section Scripts

 $('#GERelateTo').change(function () {
     alert();
     $.post('/Common/GetRelateToListing', { RelatedTable: $('#GERelateTo').val() }, function (data) {
         if (data != '') {
             $('#GERelateToName').empty();
             $.each(data, function (i, foo) {
                 $('#GERelateToName').append('<option value="' + this.Value + '">' + this.Text + '</option');
             });
         }
     });

 });

 function GvalidateEventsForm() {
     if ($("#GinputTitleEvent").val().trim() == '') {
         $('#error_div_GinputTitleEvent').show();
         return false;
     }
     else if ($("#GstartDate").val().trim() == '') {
         $('#error_div_GstartDate').show();
         $('#error_div_GinputTitleEvent').hide();
         return false;
     }
     else if ($("#GstartTime").val().trim() == '') {
         $('#error_div_GstartTime').show();
         $('#error_div_GstartDate').hide();
         $('#error_div_GinputTitleEvent').hide();
         return false;
     }
     else if ($("#GendDate").val().trim() == '') {

         $('#error_div_GendDate').show();
         $('#error_div_GstartTime').hide();
         $('#error_div_GstartDate').hide();
         $('#error_div_GinputTitleEvent').hide();
         return false;
     }
     else if ($("#GendTime").val() == '') {
       
         $('#error_div_GendTime').show();
         $('#error_div_GendDate').hide();
         $('#error_div_GstartTime').hide();
         $('#error_div_GstartDate').hide();
         $('#error_div_GinputTitleEvent').hide();
         return false;
     }
     else {
         $('#error_div_GendTime').hide();
         $('#error_div_GendDate').hide();
         $('#error_div_GstartTime').hide();
         $('#error_div_GstartDate').hide();
         $('#error_div_GinputTitleEvent').hide();
         return true;
     }
 }
 function GAddEvents() {

     if (GvalidateEventsForm()) {
         $(".se-pre-con").show();
         var sd;
         var ed;
         if ($("#GstartDate").val().indexOf('/') > -1) {
             sd = $("#GstartDate").val().replace(/\//g, "-");
         }
         else {
             sd = $("#GstartDate").val();
         }

         if ($("#GendDate").val().indexOf('/') > -1) {
             ed = $("#GendDate").val().replace(/\//g, "-");
         }
         else {
             ed = $("#GendDate").val();
         }

         $.ajax({
             url: '/Events/AddRelatedEvent',
             dataType: "html",
             type: "POST",
             contentType: 'application/json; charset=utf-8',
             data: JSON.stringify({ Title: $("#GinputTitleEvent").val(), StartDate: sd + ' ' + $("#GstartTime").val(), EndDate: ed + ' ' + $("#GendTime").val(), RelatedTo: $('#GERelateToName').val(), RelatedTable: $('#GERelateTo').val(), uColour: $("#Gecolor").val() }),
             async: true,
             processData: false,
             cache: false,
             success: function (data) {
                 if (data != "") {                    

                     $("#GEventModal").modal('hide');
                     $("#GeForm")[0].reset();
                     $(".se-pre-con").hide();
                 }
                 else {
                     $("#GaddNewEvent").modal('hide');
                     $("#GeForm")[0].reset();
                     $(".se-pre-con").hide();
                 }
             },
             error: function (xhr) {
                 $("#GaddNewEvent").modal('hide');
                 $("#eForm")[0].reset();
                 $('#errorMessage').html("No connection");
                 $(".se-pre-con").hide();
             }
         });
     }
 }




 



  