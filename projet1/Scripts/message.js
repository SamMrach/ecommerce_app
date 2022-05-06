$(document).ready(function () {
    $("#title").html('Your Messages');
    $(".total_panier").hide("");
    $(".chat_body").hide();
    $(".toggle_chat").click(function () {
        //$("li").attr("height", "100 %");
        $(this).parent().parent().next().toggle();
        //$(".chat_body").toggle();
    })
})