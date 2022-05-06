var select = document.querySelector("select");
$(document).ready(function () {
    $("#title").html('Your basket');
    $(".total_panier").show("");
})
$('input[type="number"]').change( function () {
    //alert('hi');
   // var value = $("select").find(":selected").attr("value");
    //$("#my_form").submit();
    $(this).prev().submit();
    //alert(value);
    //document.querySelector('option[value="' + CSS.escape(value) + '"]').selected="selected";
    //$("select").find(":selected").attr("selected", true);
    
})