import React, {useState, useRef} from "react";
import {
    APIProvider,
    Map,
    AdvancedMarker,
    Pin,
    InfoWindow
} from '@vis.gl/react-google-maps';
import PropTypes from "prop-types";

ViewMap.propTypes = {
    markers: PropTypes.array
}

export default function ViewMap({markers}) {
    const pos = { lat: 49.138872,   lng: -94.295050 };
    const marks = [];
    const [activeInfo, setActiveInfo] = useState(null);

    const handleMarkerClick = (markerId) => {
        setActiveInfo(markerId);
    };

    const handleInfoWindowClose = () => {
        setActiveInfo(null);
    };



    if(markers.length > 0){
        let posi = ""
        for(let i = 0; i < markers.length; i++) {
            posi = { lat: Number(markers[i].latitude) , lng:  Number(markers[i].longitude) }
            if(markers[i].rounding !== null) {
                if(markers[i].rounding === true) {
                    marks.push(<AdvancedMarker index = {i} position={posi}> <Pin background={"#FF8C00"} glyph={"->"} glyphColor={"#5E390A"} borderColor={"#FF8C00"}></Pin></AdvancedMarker>)
                } else if(markers[i].rounding === false) {
                    marks.push(<AdvancedMarker index = {i} position={posi}> <Pin background={"#FFD700"} glyph={"<-"} glyphColor={"#5E390A"} borderColor={"#FFD700"}></Pin></AdvancedMarker>)
                }
            } else {
                if(markers[i].gate !== null) {
                    marks.push(<AdvancedMarker index = {i} position={posi}> <Pin background={"#9ACD32"} glyphColor={"#556B2F"} borderColor={"#556B2F"}></Pin></AdvancedMarker>)
                } else {
                    marks.push(<AdvancedMarker index = {i} position={posi}> <Pin glyph={"!!"} glyphColor={"#FFFFFF"} scale={1.7}></Pin></AdvancedMarker>)
                }
            }

        }
    }

    return(

        <APIProvider apiKey={''} onLoad={() => console.log('Maps API has loaded.')}>
            <div style={{height:'500px'}} >
                <Map key = "12345" defaultZoom={13}
                     defaultCenter={ pos }
                     mapId='DEMO_MAP_ID'>
                    {marks}
                </Map>
            </div>
        </APIProvider>
    )
}