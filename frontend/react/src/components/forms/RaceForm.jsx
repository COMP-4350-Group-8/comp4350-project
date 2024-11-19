import React, {useState, useRef} from "react";
import { useNavigate } from 'react-router-dom';
import Card from '../ui/Card.jsx';
import PropTypes from "prop-types";
import classes from "./Form.module.css";

// Define the props that should be passed to this component
RaceForm.propTypes = {
    onAddRace: PropTypes.func,
}

// Renders a form to create a new race, including all the markers it includes
export default function RaceForm({onAddRace}) {
    // Used to navigate back to the homepage after submitting the race form
    const navigate = useNavigate();

    // References to the input fields to get their values
    const raceTitleInputRef = useRef();

    // Pass (after formatting) the data from the form to the parent component
    function submitHandler(event) {
        event.preventDefault();

        const raceTitle = raceTitleInputRef.current.value;

        // Combine the data into a single object so it can be sent to the parent class
        const data = {
            id: Math.floor(Math.random() * (99999999)),
            name: raceTitle,
            courseId: ""
        }

        // Send the race data to the parent class
        onAddRace(data);

        // Move back to the homepage now that the race creation has finished
        navigate('/');
    };

    return(
        <Card>
            <form className={classes.form} onSubmit={submitHandler}>
                <div className={classes.control}>
                    <label htmlFor='title'>Race Title</label>
                    <input type='text' required id='title' ref={raceTitleInputRef}/>
                </div>
            </form>
        </Card>
    );
}