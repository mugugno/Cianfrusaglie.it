//Quando si clicca una stella, va ad impostare il Rating al valore corretto.
function onStarClick(e) {
    e = e || window.event;
    var target = e.target || e.srcElement;
    document.getElementById("AspStarRatingValue").value = target.value;
}

//function onStarOver(e) {
    /*e = e || window.event;
    if()
    document.getElementById("AspStarRatingValue").value = "this.style.color='yellow'";
    document.getElementById("AspStarRatingValue").value = "this.style.color='grey'";
}*/