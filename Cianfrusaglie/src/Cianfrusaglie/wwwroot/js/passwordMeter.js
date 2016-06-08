var strength = {
    0: "Inaccettabile",
    1: "Scarsa",
    2: "Debole",
    3: "Buona",
    4: "Ottima"
}

var suggestion = {
    0: "",
    1: "Una password sicura dovrebbe contenere numeri",
    2: "Una password sicura dovrebbe contenere lettere maiuscole e minuscole",
    3: "Una password sicura dovrebbe contenere caratteri speciali",
    4: ""
}

function passwordStrength(password) {
    if( password.length < 8 )
        return 0;

    var strength = 1;

    if( /[0-9]/.test(password) )
        strength++;

    if( /(?=.*[a-z])(?=.*[A-Z]).+/.test(password) )
        strength++;

    if( /[_\W]/.test(password) )
        strength++;

    return strength;
}

var text = document.getElementById('password-strength-text');

$("#popoverData").change(function () {
    var val= $("#popoverData").val();
    var score= passwordStrength(val);

    // Update the password strength meter
    $("#password-strength-meter").val(score);

    // Update the text indicator
    if (val !== "") {
        text.innerHTML =
            "Sicurezza: " + "<b>" + strength[score] + "</b>" + "<span class='feedback'>" + suggestion[score] + "</span>";
    }
    else {
        text.innerHTML = "";
    }
});