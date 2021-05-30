// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



class CoordsLatLng {
    constructor(lat, lng) {
        this.lat = lat
        this.lng = lng
    }
}

function reloadPage() {
    window.location.reload()
}

let directionsDisplay;
let directionsService = new google.maps.DirectionsService();
/*let geocoder = new google.maps.Geocoder();*/

google.maps.event.addDomListener(window, 'load', initMap);

function  defaultMap(){

    let center = new google.maps.LatLng(55.8782557, 37.65372);
    let start = "55.8782557, 37.65372"

    var URL = "https://maps.googleapis.com/maps/api/staticmap?center=" + start + "&zoom=9&size=500x500&maptype=roadmap&key=AIzaSyCNKiFs0wWYTV2FyzAWJdg9cJ8AfdlbIRI";
    console.log(URL)

    document.getElementById("googleStaticPicture").src = URL;

}

function initMap() {
    directionsDisplay = new google.maps.DirectionsRenderer();
    let start = new google.maps.LatLng(55.8782557, 37.65372);
    let mapOptions = {
        zoom: 9,
        center: start
    }
    map = new google.maps.Map(document.getElementById('map'), mapOptions);

    directionsDisplay.setMap(map);

    GenAutocomplete("InputStart");
}

function calcRoute(googleLatLngStart, googleLatLngEnd) {
    console.log('calcRoute');

    console.log(googleLatLngEnd)
    let request = {
        origin: googleLatLngStart,
        destination: googleLatLngEnd,
        travelMode: 'DRIVING',
        drivingOptions: {
            departureTime: new Date(document.getElementById("TimePlain").value),
            trafficModel: 'pessimistic'
        },
        unitSystem: google.maps.UnitSystem.METRIC
    };

    directionsService.route(request, function (response, status) {
        if (status === 'OK') {
            let seconds = response.routes[0].legs[0].duration.value
            document.getElementById("EstimatedTime").value = seconds
            console.log(response.routes)
            var path = google.maps.geometry.encoding.encodePath()
            console.log("lala" + google.maps.geometry.encoding.decodePath(path))

            directionsDisplay.setDirections(response);
        } else {
            alert("directions request failed, status=" + status)
        }
    });

}

function GenAutocomplete(elementid) {
    return new Promise((resolve, reject) => {
        const center = { lat: 50.064192, lng: -130.605469 };
        const defaultBounds = {
            north: center.lat + 0.1,
            south: center.lat - 0.1,
            east: center.lng + 0.1,
            west: center.lng - 0.1,
        };

        const input = document.getElementById(elementid);
        const options = {
            bounds: defaultBounds,
            componentRestrictions: { country: "ru" },
            fields: ["address_components", "geometry", "icon", "name"],
            origin: center,
            strictBounds: false,
            types: ["establishment"],
        };
        let autocomplete = new google.maps.places.Autocomplete(input, options);

        google.maps.event.addListener(autocomplete, 'place_changed', function () {
            let place = autocomplete.getPlace();
            document.getElementById(elementid).value = place.name;
            console.log("addListener-")

            let lat = place.geometry.location.lat()
            let lng = place.geometry.location.lng()

            let googleLatLng = new google.maps.LatLng(lat, lng)

            document.getElementById(elementid + "Latitude").value = lat
            document.getElementById(elementid + "Longitude").value = lng
            resolve(googleLatLng)
        });
    })
}

function isEmptyObject(obj) {
    for (let i in obj) {
        if (obj.hasOwnProperty(i)) {
            return false;
        }
    }
    return true;
}








$('#confirm-delete').on('click', '.btn-ok', function (e) {
    var $modalDiv = $(e.delegateTarget);
    var id = $(this).data('tripId');
    $modalDiv.addClass('loading');
    $.post('/Trip/Delete/' + id).then(function () {
        $modalDiv.modal('hide').removeClass('loading');
    });
});
$('#confirm-delete').on('show.bs.modal', function (e) {
    var data = $(e.relatedTarget).data();
    $('.description', this).text(data.itemDescription);
    $('.btn-ok', this).data('tripId', data.itemId);
});