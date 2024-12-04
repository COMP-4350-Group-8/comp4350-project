import Gmap from "../map/Gmap.jsx";
import React, {useState, useRef} from "react";
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
    // References to the input fields to get their values
    const descriptionInputRef = useRef();
    const gateInputRef = useRef();

    const [coordinates, setCoordinates] = useState({ lat: STARTING_LATITUDE, lng: STARTING_LONGITUDE});
    const [markerType, setMarkerType] = useState("gate");
    const [roundedIsChecked, setRoundedIsChecked] = useState(false);

    // Handlers for latitude and longitude input changes
    const handleLatitudeChange = (event) => {
        setCoordinates((prev) => ({ ...prev, lat: event.target.value }));
        dataHandler();
    };

    const handleLongitudeChange = (event) => {
        setCoordinates((prev) => ({ ...prev, lng: event.target.value }));
        dataHandler();
    };

    const handleMarkerTypeChange = (event) => {
        const newType = event.target.value;
        if (newType === "gate" || newType === "pylon" || newType === "info") {
            setMarkerType(newType);
        }
    }

    // Handler for the "Rounding" checkbox
    const roundedCheckHandler = () => {
        setRoundedIsChecked(!roundedIsChecked);
        dataHandler();
    };

    // Handler to update the data and pass it up to the parent component
    const dataHandler = () => {
        // Call the passed data changed callback function and pass the relevant data
        onDataChanged(index, {
            latitude: coordinates.lat,
            longitude: coordinates.lng,
            description: descriptionInputRef.current.value,
            round: roundedIsChecked,
            gate: gateInputRef.current.value,
        });
    };

    return (
        <div className={classes.mapbox}>
            <h4>{markerTitle}</h4>
            <div className={classes.control}>
                <label htmlFor={`marker-${index}-description`}>Description</label>
                <textarea id={`marker-${index}-description`} required rows='3' ref={descriptionInputRef} onChange={dataHandler}></textarea>
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
                                        <p>A gate is created when 2 markers share the same gate value (i.e. "A")</p>
                                    </div>
                                    <input type='text' required id='gate' ref={gateInputRef} onChange={dataHandler}/>
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
                {/* <select value={markerType} onChange={handleMarkerTypeChange}>
                    <option value="gate">Gate</option>
                    <option value="pylon">Pylon</option>
                    <option value="info">Info</option>
                </select> */}
                {/* If */
                    // markerType === "gate" ? 
                    //     <div className={classes.control}>
                    //         <label htmlFor='gate'>Gate</label>
                    //         <input type='text' required id='gate' ref={gateInputRef} onChange={dataHandler}/>
                    //     </div>
                    // : markerType === "pylon" ? 
                    //     <div>
                    //         <input type="checkbox" id="checkbox" checked={roundedIsChecked} onChange={roundedCheckHandler}/>
                    //         <label htmlFor="checkbox">Rounding</label>
                    //     </div>
                    // : <></>
                }
            </Card>

            {/* Pass coordinates as props to the map component */}
            <Gmap latitude={coordinates.lat} longitude={coordinates.lng}/>
        </div>
    )
}