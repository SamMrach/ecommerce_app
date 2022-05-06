$(document).ready(function () {
    $("#title").html('Vos Commandes');
    $(".total_panier").hide("");
    $(".details_container").hide();
    $(".details_btn").click(function () {
        $(this).parent().parent().parent().next().toggle();
    })
})