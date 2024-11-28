import React from "react";
import RegattaForm from "../components/forms/RegattaForm.jsx";
import addRegattaHandler from "../utils/AddRegatta.jsx";
import "./Create.css";

// Render the RegattaForm and pass it a function to call when creating the regatta
export default function CreateRegatta()  {
    return (
        <>
            <h1 className="create-header">Create Regatta</h1>
            <RegattaForm onAddRegatta={addRegattaHandler}/>
        </>
    );
}