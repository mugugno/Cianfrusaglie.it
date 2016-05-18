function initialize() {
    navigator.geolocation.getCurrentPosition(function (location) {
        initializeGMaps(new google.maps.LatLng(location.coords.latitude, location.coords.longitude));
    }, unableToGeoLocalize);
}

function unableToGeoLocalize(positionError) {
    //posizione genova brignole
    initializeGMaps(new google.maps.LatLng(44.40678, 8.93391));
}

var map;
function initializeGMaps(position) {
    var mapProp = {
        center: position,
        zoom: 5,
        streetViewControl: false,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    map = new google.maps.Map(document.getElementById("googleMap"), mapProp);

    placeMarker(position);

    var input = document.getElementById('pac-input');
    var searchBox = new google.maps.places.SearchBox(input);
    map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

    searchBox.addListener('places_changed', function () {
        var places = searchBox.getPlaces();

        if (places.length == 0)
            return;

        var bounds = new google.maps.LatLngBounds();

        if (places[0].geometry.viewport) {
            bounds.union(places[0].geometry.viewport);
        } else {
            bounds.extend(places[0].geometry.location);
        }
        map.fitBounds(bounds);

        placeMarker(places[0].geometry.location, places[0].title);
    });

    google.maps.event.addListener(map, 'click', function (event) {
        placeMarker(event.latLng);
    });
}

var marker = null;
function placeMarker(location, title) {
    if (marker != null)
        marker.setMap(null);

    marker = new google.maps.Marker({
        position: location,
        title: title,
        animation: google.maps.Animation.BOUNCE,
        map: map
    });

    var infowindow = new google.maps.InfoWindow({
        content: 'Latitude: ' + location.lat() +
        '<br>Longitude: ' + location.lng()
    });
    infowindow.open(map, marker);
}