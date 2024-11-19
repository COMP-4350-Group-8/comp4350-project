import React, {useState, useRef} from "react";
import { useNavigate } from 'react-router-dom';
import Card from '../ui/Card.jsx';
import getRaces from "../../utils/GetRaces.jsx";
import PropTypes from "prop-types";
import classes from "./Form.module.css";

// Define the props that should be passed to this component
RegattaForm.propTypes = {
    onAddRegatta: PropTypes.func,
}

// Renders a form to create a new regatta
export default function RegattaForm({onAddRegatta}) {
    // Get all the available courses
    const races = getRaces();

    // Used to navigate back to the homepage after submitting the regatta form
    const navigate = useNavigate();

    // References to the input fields to get their values
    const regattaTitleInputRef = useRef();

    const [raceCount, setRaceCount] = useState(1);
    const [raceChoices, setRaceChoices] = useState([]);

    const handleRaceSelection = (event) => {
        console.log(event.target.value);
    }

    const raceDropdowns = [];
    if (races.length === 0) {
        raceDropdowns.push(<p>Sorry, you haven't created any races yet</p>);
    } else {
        for(let i = 0; i < raceCount; i++) {
            // Create a dropdown with all the courses as options, or just text if there are no courses available
            const courseOptions = [];
            races.map((race) => {
                courseOptions.push(<option key={race.id} value={race.id}>{race.name}</option>)
            });
            const dropdown = (
                    <select value={2} onChange={handleRaceSelection}>
                        {courseOptions}
                    </select>);
    
            raceDropdowns.push(
                <Card key={i}>
                    <div className={classes.control}>
                        <div className={classes.mapbox}>
                            <label htmlFor='course'>{`Race ${i + 1}`}</label>
                            {dropdown}
                        </div>
                    </div>
                </Card>
            );
        }
    }

    // Function used to update state so a new marker will be rendered in the form
    function addRace() {
        setRaceCount(raceCount + 1)
    };

    // Pass (after formatting) the data from the form to the parent component
    function submitHandler(event) {
        event.preventDefault();

        // Don't submit the form if there are no races available
        if(races.length === 0) {
            return;
        }

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
                {raceDropdowns}
                { /* Only render the submit button if there are available races */
                    races.length > 0 &&
                    <div className={classes.actions}>
                        <button type="button" onClick={addRace}>Add Race</button>
                        <button>Create</button>
                    </div>
                }
            </form>
        </Card>
    );
}