function setFieldsVisibility() {
    $(".category-form-fields").hide(0);
    var checkboxesArray = document.getElementsByClassName("category-checkboxes");
    for (var i = 0; i < checkboxesArray.length; i++) {
        var checkbox = checkboxesArray[i];
        if (checkbox.checked) {
            var categoryFormFieldsArray = document.getElementsByClassName(checkbox.id);
            for (var j = 0; j < categoryFormFieldsArray.length; j++)
                categoryFormFieldsArray[j].style.display = "block";
        }
    }
}

function disableForward() {
    if (!atLeastOneCategorySelected())
        $("#forward-button").addClass("disabled");
    else
        $("#forward-button").removeClass("disabled");
}

function clickBackButton() {
    $('#page1').show(transitionTime);
    $('#page2').hide(transitionTime);
    $('#back-button').hide(0);

    document.getElementById("forward-button").innerHTML = "Avanti";
    document.getElementById("forward-button").setAttribute("type", "button");
}

function clickRangeButton() {
    var checkbox = document.getElementById("range-checkbox");
    var rangeInput = document.getElementById("range-input");
    if (checkbox.checked) {
        rangeInput.removeAttribute("disabled");
        rangeInput.value = 1;
        setCircle(marker.position, 1);
    } else {
        rangeInput.setAttribute("disabled", "");
        rangeInput.value = 0;
        cityCircle.setMap(null);
    }
}

function atLeastOneCategorySelected() {
    var categoriesCheckBoxes = document.getElementsByClassName("category-checkboxes");
    for (var i = 0; i < categoriesCheckBoxes.length; i++)
        if (categoriesCheckBoxes[i].checked)
            return true;
    return false;
}

function clickForwardButton() {
    if (!atLeastOneCategorySelected()) {
        //$('#createError').show(0);
        //TODO mostrare errore nessuna categoria scelta
        return;
    }

    $(document).scrollTop(0);
    if (document.getElementById("forward-button").innerHTML === "Pubblica") {
        //aggiungiamo al form la posizione di google maps
        if (GMapsOnline()) {
            $("#latitudeInput").val(marker.position.lat());
            $("#longitudeInput").val(marker.position.lng());
        } else { //offline
            $("#latitudeInput").val(0);
            $("#longitudeInput").val(0);
        }

        document.getElementById("forward-button").setAttribute("type", "submit");
    } else
        document.getElementById("forward-button").innerHTML = "Pubblica";

    $('#page1').hide(transitionTime);
    $('#page2').show(transitionTime);
    $('#back-button').show(0);

    if (mapLoaded)
        return;

    setTimeout(loadGmap, 200);
}

var geocoder = new google.maps.Geocoder();
function geocodePosition(pos) {
    geocoder.geocode({
        latLng: pos
    }, function (responses) {
        if (responses && responses.length > 0) {
            $("#pac-input").val(responses[0].formatted_address);
        }
    });
}
