import Gmap from "../map/Gmap.jsx";
import {useState, useRef} from "react";
import classes from "./RaceForm.module.css";

// Coordinates for the UofM as starting coordinates
const STARTING_LATITUDE = 49.808561283776484;
const STARTING_LONGITUDE = -97.13406385382287;

export default function MarkerForm({index, markerTitle, onDataChanged}) {
    const descriptionInputRef = useRef();
    const gateInputRef = useRef();

    const [coordinates, setCoordinates] = useState({ lat: STARTING_LATITUDE, lng: STARTING_LONGITUDE});
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

    // Handler for the "Rounding" checkbox
    const roundedCheckHandler = () => {
        setRoundedIsChecked(!roundedIsChecked);
    };

    // Handler to update the data and pass it up to the parent component
    const dataHandler = () => {
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
            <div className={classes.control}>
                <label htmlFor='description'>Description</label>
                <textarea id='description' required rows='1' ref={descriptionInputRef} onChange={dataHandler}></textarea>
            </div>
            <div>
                <input type="checkbox" id="checkbox" checked={roundedIsChecked} onChange={roundedCheckHandler}/>
                <label htmlFor="checkbox">Rounding</label>
            </div>
            <div className={classes.control}>
                <label htmlFor='title'>Gate</label>
                <input type='text' required id='title' ref={gateInputRef} onChange={dataHandler}/>
            </div>

            {/* Pass coordinates as props to the map component */}
            <Gmap latitude={coordinates.lat} longitude={coordinates.lng}/>
        </div>
    )
}