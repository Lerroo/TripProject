// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

google.maps.event.addDomListener(window, 'load', initMap);
let currentZoom = 9;
let currentCenter = new google.maps.LatLng(55.8782557, 37.65372);

function initMap() {
    const directionsService = new google.maps.DirectionsService();
    const directionsRenderer = new google.maps.DirectionsRenderer();
    let start = new google.maps.LatLng(55.8782557, 37.65372);
    let mapOptions = {
        zoom: currentZoom,
        center: currentCenter
    }
    map = new google.maps.Map(document.getElementById('map'), mapOptions);

    directionsRenderer.setMap(map);        

    const onChangeHandler = function () {
        calculateAndDisplayRoute(directionsService, directionsRenderer);
    };

    google.maps.event.addListener(map, "zoom_changed", function () {
        currentZoom = map.getZoom();
    })
    google.maps.event.addListener(map, "center_changed", function () {
        currentCenter = map.getCenter();
    })

    GenAutocomplete("Address_Start").then(() => onChangeHandler);
    GenAutocomplete("Address_End").then(() => onChangeHandler);

    document.getElementById("Address_Start").addEventListener("change", onChangeHandler);
    document.getElementById("Address_End").addEventListener("change", onChangeHandler);
    document.getElementById("TimeBeforeDeparture_ApproximateStart").addEventListener("change", onChangeHandler);    
}

function calculateAndDisplayRoute(directionsService, directionsRenderer) {
    let startCoords = document.getElementById("Address_StartCoords").value;
    let endCoords = document.getElementById("Address_EndCoords").value;
    let approximateStart = document.getElementById("TimeBeforeDeparture_ApproximateStart").value;

    if (startCoords != "" && endCoords != "" && approximateStart != "") {
        directionsService.route(
            {
                origin: JSON.parse(startCoords),
                destination: JSON.parse(endCoords),
                travelMode: google.maps.TravelMode.DRIVING,
                drivingOptions: {
                    departureTime: new Date(approximateStart),
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
            let place = autocomplete.getPlace()           

            document.getElementById(elementid).value = place.name;

            let lat = place.geometry.location.lat()
            let lng = place.geometry.location.lng()
            let googleLatLng = JSON.stringify(new google.maps.LatLng(lat, lng))

            document.getElementById(elementid+"Coords").value = googleLatLng
            resolve(googleLatLng)
        });
    })
}

function getStaticMap(pathArray) {
        let markersStartEnd = getStartLabel(pathArray) + getEndLabel(pathArray);
        let clearPath = preparePath(pathArray);

        let clearStart = prepareLatLng(currentCenter)
        var URL = "https://maps.googleapis.com/maps/api/staticmap?center="
            + clearStart + "&zoom=" + currentZoom + "&size=500x500&maptype=roadmap&path="
        + clearPath + markersStartEnd + "&key=AIzaSyCNKiFs0wWYTV2FyzAWJdg9cJ8AfdlbIRI";
        document.getElementById("StaticImageWayUrl").value = URL;    
}

function getStartLabel(pathArray) {
    let first = pathArray[0];
    let markerLatLng = prepareLatLng(first)
    return '&markers=color:red|label:A|' + markerLatLng;
}

function getEndLabel(pathArray) {
    let last = pathArray[pathArray.length - 1];
    let markerLatLng = prepareLatLng(last)
    return '&markers=color:red|label:B|' + markerLatLng;
}

function preparePath(pathArray) {
    let preparePath = "color:0x0000ff80|weight:1";

    var blkstr = [];
    pathArray.forEach(function (item, i, arr) {
        var clearLatLng = prepareLatLng(item)
        blkstr.push(clearLatLng);
    });
    preparePath += blkstr.join("|")
    return preparePath
}

function prepareLatLng(obj) {
    return obj.lat() + "," + obj.lng();
}

function showReviewModalWindow() {
    $.ajaxSetup({ cache: false });
        $('#modDialog').modal('show');
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

function loadAppratialStarsRating() {
    var listDivReview = document.getElementsByName('divReview');

    for (var step = 0; step < listDivReview.length; step++) {
        var divReview = listDivReview[step];
        var listStras = divReview.getElementsByClassName('rating')
        var appraisal = divReview.getElementsByClassName('reviewAppraisal')[0].attributes.getNamedItem('value').value;
        listStras[5 - appraisal].checked = true;
    }
}