//Quando si clicca una stella, va ad impostare il Rating al valore corretto.
function onStarClick(e) {
    e = e || window.event;
    var target = e.target || e.srcElement;
    document.getElementById("AspStarRatingValue").value = target.value;
}