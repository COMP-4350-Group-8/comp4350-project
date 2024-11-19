import React from "react";
import RaceForm from "../components/forms/RaceForm.jsx";
import addRaceHandler from "../utils/AddRace.jsx";
import "./Create.css";

// Render the RaceForm and pass it a function to call when creating the race
export default function CreateRace()  {
    return (
        <>
            <h1 className="create-header">Create Race</h1>
            <RaceForm onAddrace={addRaceHandler}/>
        </>
    );
}