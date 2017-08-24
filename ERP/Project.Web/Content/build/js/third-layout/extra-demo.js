"use strict";
$(document).ready(function () {
    $(".setting-toggle").on("click", function () {
        $(".setting").toggleClass("closed")
    }),
    $(".body-bg").on("click", function () {
        var t = "../Content/build/images/backgrounds/" + $(this).attr("data-bg");        
        $("body").css("background-image", "url(" + t + ")")
    })
});