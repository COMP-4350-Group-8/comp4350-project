import React, {useState, useRef} from "react";
import {
    APIProvider,
    Map,
    AdvancedMarker,
    Pin,
    InfoWindow
} from '@vis.gl/react-google-maps';
import PropTypes from "prop-types";
import classes from './ViewMap.module.css';

// Define the props that should be passed to this component
ViewMap.propTypes = {
    markers: PropTypes.array
}

export default function ViewMap({markers}) {
    const pos = { lat: 49.138872,   lng: -94.295050 };
    const [activeInfo, setActiveInfo] = useState(null); //state for deciding which is the marker for which the info window is currently active
    let posi = ""
    let marks = []

    //updates the state to the id of the marker clicked
    const handleMarkerClick = (markerId) => {
        setActiveInfo(markerId);
    };

    //updates the state to the id of the marker clicked
    const handleInfoWindowClose = () => {
        setActiveInfo(null);
    };


    //reads the list and pushes the marker display information onto an array
    if(markers.length > 0){
        marks = markers.map((marker, i) => {
            posi = { lat: Number(marker.latitude) , lng:  Number(marker.longitude) }

            if(marker.rounding !== null && marker.rounding !== "" && marker.rounding !== undefined) {
                if(marker.rounding === true) {
                    return(<div key = {i}> <AdvancedMarker key = {i} index = {i} position={posi} onClick={() => handleMarkerClick(i)}> <Pin background={"#9ACD32"} glyph={"↷"} glyphColor={"#0F3D0F"} borderColor={"#0F3D0F"}
                    ></Pin></AdvancedMarker>
                        {activeInfo === i && (<InfoWindow
                            position={posi}
                            onCloseClick={handleInfoWindowClose}>
                            <div className={classes.infobox}>
                                Name: Marker {i + 1}<br/>
                                Latitude: {markers[i].latitude}<br/>
                                Longitude: {markers[i].longitude}<br/>
                                Description: {markers[i].description}<br/>
                            </div>
                        </InfoWindow>)}
                    </div>)
                } else if(marker.rounding === false) {
                    return(<div key = {i}><AdvancedMarker key = {i} index = {i} position={posi} onClick={() => handleMarkerClick(i)}> <Pin glyph={"↶"} glyphColor={"#330000"} borderColor={"#330000"} ></Pin></AdvancedMarker>
                        {activeInfo === i && (<InfoWindow
                            position={posi}
                            onCloseClick={handleInfoWindowClose}>
                            <div className={classes.infobox}>
                                Name: Marker {i + 1}<br/>
                                Latitude: {markers[i].latitude}<br/>
                                Longitude: {markers[i].longitude}<br/>
                                Description: {markers[i].description}<br/>
                            </div>
                        </InfoWindow>)}
                    </div>)
                }
            } else {
                if (marker.gate !== null && marker.gate !== "" && marker.gate !== undefined) {
                    return (<div key = {i}> <AdvancedMarker key={i} index={i} position={posi} onClick={() => handleMarkerClick(i)}> <Pin
                        background={"#586DB0"} glyphColor={"#0F153D"} borderColor={"#0F153D"} ></Pin></AdvancedMarker>
                        {activeInfo === i && (<InfoWindow
                            position={posi}
                            onCloseClick={handleInfoWindowClose}>
                            <div className={classes.infobox}>
                                Name: Marker {i + 1}<br/>
                                Latitude: {markers[i].latitude}<br/>
                                Longitude: {markers[i].longitude}<br/>
                                Description: {markers[i].description}<br/>
                            </div>
                        </InfoWindow>)}
                    </div>)
                } else {
                    return(<div key={i}> <AdvancedMarker key = {i} index = {i} position={posi} onClick={() => handleMarkerClick(i)}> <Pin background={"#FF8C00"} borderColor={"#5E3015"} glyph={"\u26A0"} glyphColor={"#5E3015"} scale={1.7} ></Pin></AdvancedMarker>
                        {activeInfo === i && (<InfoWindow
                            position={posi}
                            onCloseClick={handleInfoWindowClose}>
                            <div className={classes.infobox}>
                                Name: Marker {i + 1}<br/>
                                Latitude: {markers[i].latitude}<br/>
                                Longitude: {markers[i].longitude}<br/>
                                Description: {markers[i].description}<br/>
                            </div>
                        </InfoWindow>)}
                    </div>)
                }
            }
        })
    }

    return(
        <APIProvider apiKey={import.meta.env.VITE_API_KEY} onLoad={() => console.log('Maps API has loaded.')}>
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