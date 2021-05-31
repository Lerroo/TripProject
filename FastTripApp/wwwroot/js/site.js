// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

google.maps.event.addDomListener(window, 'load', initMap);

async function initMap() {
    const directionsService = new google.maps.DirectionsService();
    const directionsRenderer = new google.maps.DirectionsRenderer();
    let start = new google.maps.LatLng(55.8782557, 37.65372);
    let mapOptions = {
        zoom: 9,
        center: start
    }
    map = new google.maps.Map(document.getElementById('map'), mapOptions);

    directionsRenderer.setMap(map);

    GenAutocomplete("Address_Start").then(() => calculateAndDisplayRoute(
        directionsService,
        directionsRenderer
    ));
    GenAutocomplete("Address_End").then(() => calculateAndDisplayRoute(
        directionsService,
        directionsRenderer
    ));


    const onChangeHandler = function () {
        calculateAndDisplayRoute(directionsService, directionsRenderer);
    };

    document.getElementById("Address_Start").addEventListener("change", onChangeHandler);
    document.getElementById("Address_End").addEventListener("change", onChangeHandler);
}

function calculateAndDisplayRoute(directionsService, directionsRenderer) {
    let startCoords = document.getElementById("Address_StartCoords").value;
    let endCoords = document.getElementById("Address_EndCoords").value;

    if (startCoords != "" && endCoords != "") {
        directionsService.route(
            {
                origin: JSON.parse(startCoords),
                destination: JSON.parse(endCoords),
                travelMode: google.maps.TravelMode.DRIVING,
                drivingOptions: {
                    departureTime: new Date(document.getElementById("TimeBeforeDeparture_ApproximateStart").value),
                    trafficModel: 'pessimistic'
                },
                unitSystem: google.maps.UnitSystem.METRIC
            },
            (response, status) => {
                if (status === "OK") {
                    directionsRenderer.setDirections(response);
                    let seconds = response.routes[0].legs[0].duration.value
                    document.getElementById("TimeBeforeDeparture_Estimated").value = seconds

                    var path = response.routes[0].overview_path
                    getStaticMap(path)
                } else {
                    window.alert("Directions request failed due to " + status);
                }
            }
        );
    }
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

            let lat = place.geometry.location.lat()
            let lng = place.geometry.location.lng()
            let googleLatLng = JSON.stringify( new google.maps.LatLng(lat, lng))

            document.getElementById(elementid+"Coords").value = googleLatLng
            resolve(googleLatLng)
        });
    })
}

function isNotEmptyObject(obj) {
    for (let i in obj) {
        if (obj.hasOwnProperty(i)) {
            return true;
        }
    }
    return false;
}

function getStaticMap(path) {
    //let clearPath = preparePath(path)
    //let clearStart = prepareLatLng(map.getCenter())
    //console.log(clearStart)
    //console.log(map.getZoom())
    //var URL = "https://maps.googleapis.com/maps/api/staticmap?center="
    //    + clearStart + "&zoom=9&size=500x500&maptype=roadmap&path="
    //    + clearPath + "&key=AIzaSyCNKiFs0wWYTV2FyzAWJdg9cJ8AfdlbIRI";
    //document.getElementById("googleStaticPicture").src = URL;
}

$(function () {
    $('#modal-delete').on('show.bs.modal', function (event) {
        let button = $(event.relatedTarget);
        let id = button.data("id");
        let modal = $(this);
        modal.find('.modal-content input').val(id);
    });

    $("#modalDeleteButton").click(function () {
        $("#myForm").submit();

    });
});

function setAppratialStarsRating() {
    var inp = document.getElementsByName('rating');
    for (var i = 0; i < inp.length; i++) {
        if (inp[i].type == "radio" && inp[i].checked) {
            document.getElementById('appraisal').value = inp[i].value;
            break;
        }
    }
}