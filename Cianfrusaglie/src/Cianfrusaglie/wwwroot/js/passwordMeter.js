var strength = {
    0: "Inaccettabile",
    1: "Scarsa",
    2: "Debole",
    3: "Buona",
    4: "Ottima"
}

function passwordStrength(password) {
    var result = {
        score: 0,
        message: ""
    }

    if( password.length < 8 )
        return result;

    result.score= 1;

    if( /[0-9]/.test(password) )
        result.score++;
    else if (result.message === "")
        result.message = "Una password sicura dovrebbe contenere numeri";

    if( /(?=.*[a-z])(?=.*[A-Z]).+/.test(password) )
        result.score++;
    else if (result.message === "")
        result.message = "Una password sicura dovrebbe contenere lettere maiuscole e minuscole";

    if( /[_\W]/.test(password) )
        result.score++;
    else if (result.message === "")
        result.message = "Una password sicura dovrebbe contenere caratteri speciali";

    return result;
}

var text = document.getElementById('password-strength-text');

$("#popoverData").keyup(function () {
    var val= $("#popoverData").val();
    var result= passwordStrength(val);

    // Update the password strength meter
    $("#password-strength-meter").val(result.score);

    // Update the text indicator
    if (val !== "") {
        text.innerHTML =
            "Sicurezza: <b>" + strength[result.score] + "</b> <span class='feedback'>" + result.message + "</span>";
    }
    else {
        text.innerHTML = "";
    }
});