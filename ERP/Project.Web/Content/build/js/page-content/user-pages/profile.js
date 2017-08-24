$(document).ready(function () {
    function e() {
        var e = {
            center: new google.maps.LatLng(34.052234, -118.243685),
            zoom: 10,
            mapTypeId: google.maps.MapTypeId.ROADMAP,
            styles: [{
                featureType: "water",
                elementType: "geometry",
                stylers:
                    [{
                    color: "#004358"
                    }]
            },
            {
                featureType: "landscape",
                elementType: "geometry",
                stylers:
                    [{
                        color: "#1f8a70"
                    }]
            },
            {
                featureType: "poi",
                elementType: "geometry",
                stylers:
                    [{
                        color: "#1f8a70"
                    }]
            },
            {
                featureType: "road.highway",
                elementType: "geometry",
                stylers:
                    [{
                        color: "#fd7400"
                    }]
            },
            {
                featureType: "road.arterial",
                elementType: "geometry",
                stylers:
                    [{
                        color: "#1f8a70"
                    },
                    {
                        lightness: -20
                    }]
            },
            {
                featureType: "road.local",
                elementType: "geometry",
                stylers:
                    [{
                        color: "#1f8a70"
                    },
                    {
                        lightness: -17
                    }]
            },
            {
                elementType: "labels.text.stroke",
                stylers:
                    [{
                        color: "#ffffff"
                    },
                    {
                        visibility: "on"
                    },
                    {
                        weight: .9
                    }]
            },
            {
                elementType: "labels.text.fill",
                stylers:
                    [{
                        visibility: "on"
                    },
                    {
                        color: "#ffffff"
                    }]
            },
            {
                featureType: "poi",
                elementType: "labels",
                stylers:
                    [{
                        visibility: "simplified"
                    }]
            },
            {
                elementType: "labels.icon",
                stylers:
                    [{
                        visibility: "off"
                    }]
            },
            {
                featureType: "transit",
                elementType: "geometry",
                stylers:
                    [{
                        color: "#1f8a70"
                    },
                    {
                        lightness: -10
                    }]
            },
            {
            },
            {
                featureType: "administrative",
                elementType: "geometry",
                stylers:
                    [{
                        color: "#1f8a70"
                    },
                    {
                        weight: .7
                    }]
            }]
        }; new google.maps.Map(document.getElementById("simpleMap"), e)
    } google.maps.event.addDomListener(window, "load", e)
});