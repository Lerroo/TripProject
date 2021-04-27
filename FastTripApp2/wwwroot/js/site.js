// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function reloadPage() {
    window.location.reload()
}


let directionsDisplay;
let directionsService = new google.maps.DirectionsService();
let geocoder = new google.maps.Geocoder();

google.maps.event.addDomListener(window, 'load', initMap);

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

function calcRoute(objCoord, objCoord2) {
    console.log('calcRoute');
    console.log(objCoord2)
    if (!isEmptyObject(objCoord) && !isEmptyObject(objCoord2)) {
        let start = new google.maps.LatLng(objCoord.lat, objCoord.lng);
        let end = new google.maps.LatLng(objCoord2.lat, objCoord2.lng);
        let request = {
            origin: start,
            destination: end,
            travelMode: 'DRIVING'
        };

        directionsService.route(request, function (response, status) {
            if (status === 'OK') {
                directionsDisplay.setDirections(response);

            } else {
                alert("directions request failed, status=" + status)
            }

            //document.getElementById("InputEstimatedTime").value = response.routes[0].legs[0].duration.text
            //document.getElementById("InputEstimatedTime").value = response.routes[0].legs[0].duration.text
        });
    }
}

function GenAutocomplete(elementid) {
    return new Promise((resolve, reject) => {
        const center = { lat: 50.064192, lng: -130.605469 };
        // Create a bounding box with sides ~10km away from the center point
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

            let coords = new CoordsLatLng(place.geometry.location.lat(), place.geometry.location.lng())

            document.getElementById(elementid + "Latitude").value = coords.lat
            document.getElementById(elementid + "Longitude").value = coords.lng
            resolve(coords)
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
