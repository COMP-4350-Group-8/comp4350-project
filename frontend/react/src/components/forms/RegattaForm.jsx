import React, {useState, useRef} from "react";
import { useNavigate } from 'react-router-dom';
import Card from '../ui/Card.jsx';
import PropTypes from "prop-types";
import classes from "./Form.module.css";

// Define the props that should be passed to this component
RegattaForm.propTypes = {
    onAddRegatta: PropTypes.func,
}

// Renders a form to create a new regatta
export default function RegattaForm({onAddRegatta}) {
    // Used to navigate back to the homepage after submitting the regatta form
    const navigate = useNavigate();

    // References to the input fields to get their values
    const regattaTitleInputRef = useRef();

    // Pass (after formatting) the data from the form to the parent component
    function submitHandler(event) {
        event.preventDefault();

        const regattaTitle = regattaTitleInputRef.current.value;

        // Combine the data into a single object so it can be sent to the parent class
        const data = {
            id: Math.floor(Math.random() * (99999999)),
            name: regattaTitle,
            courseId: ""
        }

        // Send the regatta data to the parent class
        onAddRegatta(data);

        // Move back to the homepage now that the regatta creation has finished
        navigate('/');
    };

    return(
        <Card>
            <form className={classes.form} onSubmit={submitHandler}>
                <div className={classes.control}>
                    <label htmlFor='title'>Regatta Title</label>
                    <input type='text' required id='title' ref={regattaTitleInputRef}/>
                </div>
                <div className={classes.actions}>
                    <button>Create</button>
                </div>
            </form>
        </Card>
    );
}