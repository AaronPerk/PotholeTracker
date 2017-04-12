var potholeList = [];
var map;
var infoWindowList = [];

function placePotholes(currentPotholes, userType) {

    var cleveland = { lat: 41.505, lng: -81.681 };
    map = new google.maps.Map(document.getElementById('map'), {
        zoom: 15,
        center: cleveland,
        streetViewControl: false
    });

    for (var i = 0; i < currentPotholes.length; i++) {

        var img = '/images/reddot.png';
        var titleText = 'Awaiting Inspection';
        //var contentString = '<form action="/function/update" method="post"><select name="severity"><option value="0">Severity</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="4">4</option><option value="5">5</option></select><br /><input name="PotholeID" type="hidden" value="' + currentPotholes[i].PotholeId + '"> <input name="InspectDate" type="hidden" value="' + Date.now() + '"> <button id="inspectButton" type="submit">Mark For Repair</button> </form>';
        var contentString = '<form action="/function/update" method="post"><select class="form-control" name="severity"><option value="0">Severity</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="4">4</option><option value="5">5</option></select><br /><input name="potholeId" type="hidden" value="' + currentPotholes[i].PotholeID + '"> <input name="status" type="hidden" value="inspect"> <button class="btn-default btn-xs btn-block" id="inspectButton" type="submit">Mark For Repair</button> </form>';

        contentString = contentString + '<form action="/function/delete" method="post"> <input name="id" type="hidden" value="' + currentPotholes[i].PotholeID + '"> <button class="btn-danger btn-xs btn-block" id="deleteButton" type="submit">Delete Pothole</button> </form>'

        if (currentPotholes[i].RepairEndDate != null) {
            img = '/images/greendot.png';
            titleText = 'Repair Complete';
            //undoRepairComplete Button
            contentString = '<form action="/function/undorepaircomplete" method="post"> <input name="id" type="hidden" value="' + currentPotholes[i].PotholeID + '"> <button class="btn-default btn-xs btn-block" id="undoButton" type="submit">Undo Repair Complete</button> </form>'

            contentString = contentString + '<form action="/function/delete" method="post"> <input name="id" type="hidden" value="' + currentPotholes[i].PotholeID + '"> <button class="btn-danger btn-xs btn-block" id="deleteButton" type="submit">Delete Pothole</button> </form>'
        }
        else if (currentPotholes[i].RepairStartDate != null) {
            img = '/images/bluedot.png'
            titleText = 'Repair In Progress';
            contentString = '<form action="/function/update" method="post"><input name="potholeId" type="hidden" value="' + currentPotholes[i].PotholeID + '"> <input name="status" type="hidden" value="repairEnd"> <button class="btn-default btn-xs btn-block" id="inspectButton" type="submit">Repair Complete</button> </form>';

            //undoStartRepair Button
            contentString = contentString + '<form action="/function/undostartrepair" method="post"> <input name="id" type="hidden" value="' + currentPotholes[i].PotholeID + '"> <button class="btn-default btn-xs btn-block" id="undoButton" type="submit">Undo Repair Started</button> </form>'

            contentString = contentString + '<form action="/function/delete" method="post"> <input name="id" type="hidden" value="' + currentPotholes[i].PotholeID + '"> <button class="btn-danger btn-xs btn-block" id="deleteButton" type="submit">Delete Pothole</button> </form>'
        }
        else if (currentPotholes[i].InspectDate != null && currentPotholes[i].RepairStartDate==null) {
            img = '/images/yellowdot.png';
            titleText = 'Awaiting Repair';
            //Begin Repair Button
            contentString = '<form action="/function/update" method="post"><input name="potholeId" type="hidden" value="' + currentPotholes[i].PotholeID + '"> <input name="status" type="hidden" value="repairStart"> <button class="btn-default btn-xs btn-block" id="inspectButton" type="submit">Begin Repair</button> </form>';
            //undoInspect Button
            contentString = contentString + '<form action="/function/undoinspect" method="post"> <input name="id" type="hidden" value="' + currentPotholes[i].PotholeID + '"> <button class="btn-default btn-xs btn-block" id="undoButton" type="submit">Undo Inspect</button> </form>'
            //Delete Button
            contentString = contentString + '<form action="/function/delete" method="post"> <input name="id" type="hidden" value="' + currentPotholes[i].PotholeID + '"> <button class="btn-danger btn-xs btn-block" id="deleteButton" type="submit">Delete Pothole</button> </form>'
        }

        var pothole = {
            pin: new google.maps.Marker({
                position: { lat: currentPotholes[i].Latitude, lng: currentPotholes[i].Longitude },
                map: map,
                icon: img,
                title: titleText,
                windowContent: contentString,
            }),
            info: currentPotholes[i],
            id: currentPotholes[i].PotholeId, 
        }

        pothole.pin.addListener('click', function (event) {

            closeAllInfoWindows();

            var infoWindow = new google.maps.InfoWindow({
                content: this.windowContent,
            })


            infoWindow.open(map, this);
            infoWindowList.push(infoWindow);
        })

        potholeList.push(pothole);

    }

}

function closeAllInfoWindows() {
    for (var i = 0; i < infoWindowList.length; i++) {
        infoWindowList[i].close();
    }
}

function fillAddress(lat, lng, id) {

    var currentUrl = "https://maps.googleapis.com/maps/api/geocode/json?latlng=" + lat + "," + lng + "&key=AIzaSyAPiyVpokXSqtRW8T58W_gFi9YutxN-ZlA"

    // takes lat long and gives street address
    $.ajax({
        url: currentUrl,
        type: "GET",
        dataType: "json",

    }).done(function (data) {
        $("#phAddr" + id).text(data.results[0].formatted_address);
        console.log(data.results[0].formatted_address);
        console.log("#phAddr" + id);
        //var adress = data.results[0].formatted_address;
        //alert(adress);
        //$("#phAddr" + i).html(data.results.formatted_address);
        //alert(data.results.formatted_address);
    }).fail(function (xhr, status, error) {
        console.log(error);
    });
}
function centerOnPoint(latIn, lngIn, id) {
    var newCenter = { lat: latIn, lng: lngIn };
    map.setCenter(newCenter);
    map.setZoom(18);
    //var infoWindow = getPotholeWindowById(id);
    //infoWindow.open();
}

function getPotholeWindowById(id) {
    for (var i = 0; i < infoWindowList.length; i++) {
        if (infoWindowList[i].potholeId == id) {
            return infoWindowList[i];
        }
    }
}