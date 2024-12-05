import Gmap from "../map/Gmap.jsx";
import React, {useState, useEffect} from "react";
import PropTypes from "prop-types";
import classes from "./Form.module.css";
import Card from "../ui/Card.jsx";

// Coordinates for the UofM as starting coordinates
const STARTING_LATITUDE = 49.808561283776484;
const STARTING_LONGITUDE = -97.13406385382287;

// Define the props that should be passed to this component
MarkerForm.propTypes = {
    index: PropTypes.number,
    markerTitle: PropTypes.string,
    onDataChanged: PropTypes.func,
}

// Renders a form for a single race course Marker
export default function MarkerForm({index, markerTitle, onDataChanged}) {
    // State to hold values for the inputs
    const [description, setDescription] = useState("");
    const [coordinates, setCoordinates] = useState({ lat: STARTING_LATITUDE, lng: STARTING_LONGITUDE});
    const [markerType, setMarkerType] = useState("gate");
    const [gate, setGate] = useState("");
    const [roundedIsChecked, setRoundedIsChecked] = useState(false);

    // Handlers for input changes
    const handleDescriptionChange = (event) => {
        setDescription(event.target.value);
    };
    const handleLatitudeChange = (event) => {
        setCoordinates((prev) => ({ ...prev, lat: event.target.value }));
    };
    const handleLongitudeChange = (event) => {
        setCoordinates((prev) => ({ ...prev, lng: event.target.value }));
    };
    const handleMarkerTypeChange = (event) => {
        const newType = event.target.value;
        if (newType === "gate" || newType === "pylon" || newType === "info") {
            setMarkerType(newType);
        }
    };
    const handleGateChange = (event) => {
        setGate(event.target.value);
    };
    const roundedCheckHandler = () => {
        setRoundedIsChecked(!roundedIsChecked);
    };

    // When the inputs change, collect the data and send it to the CourseForm
    useEffect(() => {
        // Create an object to hold the marker's data
        const markerData = {
            latitude: coordinates.lat,
            longitude: coordinates.lng,
            description: description
        };

        // If the marker is a gate, add the gate info to the object
        if (markerType === "gate") {
            markerData.gate = gate;
        }
        // If the marker is a pylon, add the rounding info to the object
        else if (markerType === "pylon") {
            markerData.round = roundedIsChecked;
        }

        // Call the passed data changed callback function and pass the constructed data
        onDataChanged(index, markerData);
    }, [index, onDataChanged, description, coordinates, markerType, gate, roundedIsChecked]);

    return (
        <div className={classes.mapbox}>
            <h4>{markerTitle}</h4>
            <div className={classes.control}>
                <label htmlFor={`marker-${index}-description`}>Description</label>
                <textarea id={`marker-${index}-description`} required rows='3' onChange={handleDescriptionChange}></textarea>
            </div>
            {/* Inputs to change latitude and longitude */}
            <div className={classes.control}>
                <label>
                    Latitude:
                    <input
                        type="number"
                        value={coordinates.lat}
                        onChange={handleLatitudeChange}
                        placeholder="Latitude"
                    />
                </label>
            </div>
            <div className={classes.control}>
                <label className={classes.control}>
                    Longitude:
                    <input
                        type="number"
                        value={coordinates.lng}
                        onChange={handleLongitudeChange}
                        placeholder="Longitude"
                    />
                </label>
            </div>
            {/* Dropdown to select marker type (gate, pylon, or info) and controls for that marker type */}
            <Card>
                <div className={classes.control}>
                    <div className={classes.mapbox}>
                        <label>Marker Type</label>
                        <select className={classes.dropdown} value={markerType} onChange={handleMarkerTypeChange}>
                            <option value="gate">Gate</option>
                            <option value="pylon">Pylon</option>
                            <option value="info">Info</option>
                        </select>
                        {/* If */
                            markerType === "gate" ? 
                                <>
                                    <div className={classes.horizontal}>
                                        <label htmlFor='gate'>Gate</label>
                                        <p>A gate is created when 2 markers share the same gate value (i.e. &quot;A&quot;)</p>
                                    </div>
                                    <input type='text' required id='gate' onChange={handleGateChange}/>
                                </>
                            : markerType === "pylon" ? 
                                <div className={classes.horizontal}>
                                    <label htmlFor="checkbox">Rounding</label>
                                    <input type="checkbox" id="checkbox" checked={roundedIsChecked} onChange={roundedCheckHandler}/>
                                    <p>Checked: Clockwise (↷), Unchecked: Counter-clockwise (↶)</p>
                                </div>
                            : <></>
                        }
                    </div>
                </div>
            </Card>
            {/* Pass coordinates as props to the map component */}
            <Gmap latitude={coordinates.lat} longitude={coordinates.lng}/>
        </div>
    )
}