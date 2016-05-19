function initialize() {
    navigator.geolocation.getCurrentPosition(function (location) {
        initializeGMaps(new google.maps.LatLng(location.coords.latitude, location.coords.longitude));
    }, unableToGeoLocalize);
}

function unableToGeoLocalize(positionError) {
    //posizione genova brignole
    initializeGMaps(new google.maps.LatLng(44.40678, 8.93391));
}

var map, marker, cityCircle;

/// inizializza Google Maps alla posizione position
/// onlyView è true se non si può modificare la posizione del marker (usato con onlyView = true in visualizza annuncio o profilo utente)
function initializeGMaps(position, onlyView, radius) {
    if( onlyView == null )
        onlyView = false;

    var mapProp = {
        center: position,
        zoom: 5,
        streetViewControl: false,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    map = new google.maps.Map(document.getElementById("googleMap"), mapProp);

    placeMarker(position);

    if( !onlyView ) { //parte di input della posizione e click utente per riposizionare il marker
        var input = document.getElementById('pac-input');
        var searchBox = new google.maps.places.SearchBox(input);
        map.controls[ google.maps.ControlPosition.TOP_LEFT ].push(input);

        searchBox.addListener('places_changed', function() {
            var places = searchBox.getPlaces();

            if( places.length == 0 )
                return;

            var bounds = new google.maps.LatLngBounds();

            if( places[ 0 ].geometry.viewport ) {
                bounds.union(places[ 0 ].geometry.viewport);
            } else {
                bounds.extend(places[ 0 ].geometry.location);
            }
            map.fitBounds(bounds);

            placeMarker(places[ 0 ].geometry.location, places[ 0 ].title);
        });

        setCircle(marker.position, 1);
        google.maps.event.addListener(map, 'click', function(event) { placeMarker(event.latLng); });
    } else if( radius != null && radius > 0 ) {
        //creo un cerchio intorno alla posizione
        setCircle(marker.position, radius);
    }
}

function setCircle(position, radius) {
    if( cityCircle != null )
        cityCircle.setMap(null); //eliminare vecchio cerchio

    cityCircle = new google.maps.Circle({
        strokeColor: '#FF0000',
        strokeOpacity: 0.8,
        strokeWeight: 2,
        fillColor: '#FF0000',
        fillOpacity: 0.35,
        map: map,
        center: position,
        radius: radius * 1000
    });
}

function placeMarker(location, title) {
    if (marker != null)
        marker.setMap(null); //eliminare vecchio marker

    marker = new google.maps.Marker({
        position: location,
        title: title,
        animation: google.maps.Animation.BOUNCE,
        map: map
    });
}