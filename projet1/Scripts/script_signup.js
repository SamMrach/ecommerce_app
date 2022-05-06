var select = document.getElementById("select_type");
select.addEventListener("change", function (event) {
    //alert("hi" + event.target.value);
    if (event.target.value == "particulier") {
        document.getElementById("client").style.display="none";
        document.getElementById("societe").style.display = "none";
        document.getElementById("particulier").style.display = "block";
        document.getElementById("cin").style.display = "block";
        document.getElementById("type_activite").style.display = "none";
        document.getElementById("SIRN").style.display = "none";
    }
    if (event.target.value == "societe") {
        document.getElementById("client").style.display = "none";
        document.getElementById("societe").style.display = "block";
        document.getElementById("particulier").style.display = "none";

        document.getElementById("cin").style.display = "none";
        document.getElementById("type_activite").style.display = "block";
        document.getElementById("SIRN").style.display = "block";
    }
    if (event.target.value == "client") {
        document.getElementById("client").style.display = "block";
        document.getElementById("societe").style.display = "none";
        document.getElementById("particulier").style.display = "none";
        document.getElementById("cin").style.display = "none";
        document.getElementById("type_activite").style.display = "none";
        document.getElementById("SIRN").style.display = "none";
    }
})

