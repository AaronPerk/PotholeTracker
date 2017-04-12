$(document).ready(function reportMap() {

    var cleveland = { lat: 41.505, lng: -81.681 };
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 15,
        center: cleveland,
        streetViewControl: false

    });
    
    var pothole;

    google.maps.event.addListener(map, 'click', function (event) {
        if (pothole) {
            pothole.window.close();
            pothole.pin.setMap(null);
        }

        pothole = placePothole(event.latLng); //places marker at latitude & longitude of click and fills pothole object
        var contentString = '<form action="/function/report" method="post"> <input name="Latitude" type="hidden" value="' + pothole.pin.position.lat() + '"> <input name="Longitude" type="hidden" value="' + pothole.pin.position.lng() + '"> <button class="btn-default btn-xs btn-block" id="potholeButton" type="submit">Submit Pothole</button> </form>';
        //pothole.contentString = contentString;

        var infowindow = new google.maps.InfoWindow({
            content: contentString
        });

        infowindow.open(map, pothole.pin);

        pothole.window = infowindow;

        google.maps.event.addListener(pothole.window, 'closeclick', function (event) {
            pothole.pin.setMap(null);
        });

    });

    //var image = 'https://cdn2.iconfinder.com/data/icons/bad-day/64/flat_tire_unhappy_bad_luck_depression_fail_failure_defeat-512.png';

    function placePothole(location) {

        var marker;

        if (marker) {
            marker.setPosition(location);
        } else {
            marker = new google.maps.Marker({
                position: location,
                map: map,
                draggable: true,
                //icon: image
            });
        }

        return { //returns pothole object
            pin: marker,
            window: null,
        }
    }
});