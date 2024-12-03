import React from "react";
import PropTypes from 'prop-types';
import RegattaForm from "../../components/forms/RegattaForm.jsx";
import addRegattaHandler from "../../utils/AddRegatta.jsx";
import "./Create.css";

// Define the props that should be passed to this component
CreateRegatta.propTypes = {
    serverUrl: PropTypes.string
}

// Render the RegattaForm and pass it a function to call when creating the regatta
export default function CreateRegatta({serverUrl})  {
    return (
        <>
            <h1 className="create-header">Create Regatta</h1>
            <RegattaForm serverUrl={serverUrl} onAddRegatta={addRegattaHandler}/>
        </>
    );
}