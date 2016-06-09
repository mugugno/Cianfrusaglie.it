var antiForgeryToken = $("input[name=__RequestVerificationToken]").val();
$('#popoverData').keyup(passwordCheck);
$("#confirmPassword").keyup(passwordCheck);
$("#uname").focusout(function () {
    var uname = $('#uname').val();
    $.post("/Account/CheckUserNameAlreadyExists",
        {
            user: uname,
            __RequestVerificationToken: antiForgeryToken

        })
        .success(function (res) {
            if (res) {
                $('#unamedanger').html("Il nome utente inserito esiste già.");
            } else {
                $('#unamedanger').html("");
            }
        });
});
$("#email").focusout(function () {
    var email = $('#email').val();
    $.post("/Account/CheckEmailAlreadyExists",
        {
            email: email,
            __RequestVerificationToken: antiForgeryToken

        })
        .success(function (res) {
            if (res) {
                $('#emaildanger').html("L'email inserita esiste già.");
            } else {
                $('#emaildanger').html("");
            }
        });
});

function passwordCheck() {
    if ($('#popoverData').val().length < 8)
        $('#pwdlen').html('La password deve contenere almeno 8 caratteri!');
    else
        $('#pwdlen').html('');

    if ($("#popoverData").val() != $("#confirmPassword").val())
        $("#wrongpwd").html('La password di conferma è diversa dalla password!');
    else
        $("#wrongpwd").html('');
}

function clickNextButton(tmp) {
    if (tmp === 1) {
        checkValidAddress();
        if ($("#pac-input").val().length === 0 || validAddress) {
            $("#pac-input").popover("hide");
            $('.form-register1').hide();
            $('.form-register3').hide();
            $('.form-register2').show();
        } else
            $("#pac-input").popover("show");
    }
    else if (tmp === 2) {
        $('.form-register2').hide();
        $('.form-register1').hide();
        $('.form-register3').show();
    }
}

function clickBackButton(tmp) {
    if (tmp === 2) {
        $('.form-register1').show();
        $('.form-register2').hide();
        $('.form-register3').hide();
    }
    else if (tmp === 3) {
        $('.form-register2').show();
        $('.form-register1').hide();
        $('.form-register3').hide();
    }
}

$("#formRegister").submit(function (event) {
    var l = Ladda.create(document.querySelector("#submit-button"));
    l.start();
});