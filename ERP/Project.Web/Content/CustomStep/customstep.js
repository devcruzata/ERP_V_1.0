



$(document).ready(function () {
    $("#e-campaign-form").steps({
        headerTag: "h3",
        bodyTag: "fieldset",
        transitionEffect: "slide",
        stepsOrientation: "vertical",
        onStepChanged: function (e, currentIndex, priorIndex) {
        },
        // Triggered when clicking the Previous/Next buttons
        onStepChanging: function (e, currentIndex, newIndex) {

            if (currentIndex < newIndex) {
                if (currentIndex == 0) {
                    if (!(ValidateStep1())) {
                        // Do not jump to the next step               
                        return false;
                    }
                }
                if (currentIndex == 1) {
                    if (!(validateStep2())) {
                        // Do not jump to the next step               
                        return false;
                    }
                }
                if (currentIndex == 2) {
                    if (!(validateStep3())) {
                        // Do not jump to the next step               
                        return false;
                    }
                }
                if (currentIndex == 3) {
                    if (!(validateStep4())) {
                        // Do not jump to the next step               
                        return false;
                    }
                }
            }
            return true;
        },
        // Triggered when clicking the Finish button
        onFinishing: function (e, currentIndex) {
            // var fv = $('#profileForm').data('formValidation'),
            var $container = $('#e-campaign-form').find('section[data-step="' + currentIndex + '"]');

            // Validate the last step container
            fv.validateContainer($container);

            var isValidStep = validateStep1();
            if (isValidStep === false || isValidStep === null) {
                return false;
            }

            return true;
        },
        onFinished: function (e, currentIndex) {
            // Uncomment the following line to submit the form using the defaultSubmit() method
            // $('#profileForm').formValidation('defaultSubmit');

            // For testing purpose
            //$('#welcomeModal').modal();
        }
    })
});

function ValidateStep1() {
    if ($("#Title").val().trim() == '')
    {
        $("#error_div_Title").show();
        return false;
    }
    else if ($("#MSubject").val().trim() == '') {
        $("#error_div_MSubject").show();
        $("#error_div_Title").hide();
        return false;
    }
    else if ($("#SName").val().trim() == '') {
        $("#error_div_MSubject").hide();
        $("#error_div_Title").hide();
        $("#error_div_SName").show();
        return false;

    }
    else if ($("#SEmail").val().trim() == '') {
        $("#error_div_MSubject").hide();
        $("#error_div_Title").hide();
        $("#error_div_SName").hide();
        $("#error_div_SEmail").show();
        return false;
    }
    else if ($("#REmail").val().trim() == '') {
        $("#error_div_MSubject").hide();
        $("#error_div_Title").hide();
        $("#error_div_SName").hide();
        $("#error_div_SEmail").hide();
        $("#error_div_REmail").show();
        return false;
    }
    else
    {
        $("#error_div_MSubject").hide();
        $("#error_div_Title").hide();
        $("#error_div_SName").hide();
        $("#error_div_SEmail").hide();
        $("#error_div_REmail").hide();
        return true;
    }
}

function ValidateStep2()
{
   
}