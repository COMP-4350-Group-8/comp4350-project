import React from "react";
import PropTypes from 'prop-types';
import RaceForm from "../../components/forms/RaceForm.jsx";
import addRaceHandler from "../../utils/AddRace.jsx";
import "./Create.css";

// Define the props that should be passed to this component
CreateRace.propTypes = {
    serverUrl: PropTypes.string
}

// Render the RaceForm and pass it a function to call when creating the race
export default function CreateRace({serverUrl})  {
    return (
        <>
            <h1 className="create-header">Create Race</h1>
            <RaceForm serverUrl={serverUrl} onAddRace={addRaceHandler}/>
        </>
    );
}