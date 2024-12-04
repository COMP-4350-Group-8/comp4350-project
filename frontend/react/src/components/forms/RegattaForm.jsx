import React, {useState, useRef, useEffect} from "react";
import { useNavigate } from 'react-router-dom';
import Card from '../ui/Card.jsx';
import getRaces from "../../utils/GetRaces.jsx";
import PropTypes from "prop-types";
import classes from "./Form.module.css";

// Define the props that should be passed to this component
RegattaForm.propTypes = {
    serverUrl: PropTypes.string,
    onAddRegatta: PropTypes.func,
}

// Renders a form to create a new regatta
export default function RegattaForm({serverUrl, onAddRegatta}) {
    // Get all the available courses
    const [races, setRaces] = useState([]);
    useEffect(() => {
        getRaces(serverUrl, setRaces);
    }, [serverUrl]);

    // Used to navigate back to the homepage after submitting the regatta form
    const navigate = useNavigate();

    // References to the input fields to get their values
    const regattaTitleInputRef = useRef();

    const [raceCount, setRaceCount] = useState(0);
    const [raceChoices, setRaceChoices] = useState([]);

    const handleRaceSelection = (index) => (event) => {
        // Update the race choices state with the new data
        setRaceChoices(prevRaceChoices => {
            const updatedRaceChoices = [...prevRaceChoices];
            updatedRaceChoices[index] = event.target.value;
            return updatedRaceChoices;
        });
    }

    // Create the necessary elements so the user can select the races they want in the regatta
    const raceDropdowns = [];
    // If no races are available, don't allow the user to create a regatta
    if (races.length === 0) {
        raceDropdowns.push(<p key={0}>Sorry, you haven&apos;t created any races yet</p>);
    } 
    // If there are races available, show a race selection dropdown for each race the user has added to the regatta
    else {
        // Create the options for the dropdown based on the available races
        const raceOptions = [];
        races.map((race) => {
            raceOptions.push(<option key={race.id} value={race.id}>{race.name}</option>)
        });

        // Create an array of cards for each race the user has added to the regatta using the dropdown created above
        for(let i = 0; i < raceCount; i++) {
            // Create a dropdown with all race options
            const dropdown = (
                <select key={i} value={raceChoices[i]} onChange={handleRaceSelection(i)}>
                    {raceOptions}
                </select>
            );

            // Create the card with the dropdown
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
        // Store the race count state (so we don't have to worry about getting the updated state value later)
        const curRaceCount = raceCount;

        // Update the state tracking how many races the user has added to the regatta
        setRaceCount(raceCount + 1);

        // Set the new race choice's value
        setRaceChoices(prevRaceChoices => {
            const updatedRaceChoices = [...prevRaceChoices];
            updatedRaceChoices[curRaceCount] = races[0].id;
            return updatedRaceChoices;
        });
    };

    // Pass (after formatting) the data from the form to the parent component
    function submitHandler(event) {
        event.preventDefault();

        // Don't submit the form if there are no races available
        if(races.length === 0) {
            return;
        }

        // Get the regatta data
        const regattaId = Math.floor(Math.random() * (99999999));
        const regattaTitle = regattaTitleInputRef.current.value;

        // Combine the data into a single object so it can be sent to the parent class
        const regattaData = {
            id: regattaId,
            name: regattaTitle,
            description: "placeholder description"
        }

        // Use the added races to build an array that will be used to update the races so they are linked with this regatta
        const raceDataList = [];
        raceChoices.map((raceChoice) => {
            // Get the race data for this added race
            const id = parseInt(raceChoice, 10);
            const race = races.find(item => item.id === id);

            // Add the regatta id to the race object and add it to the array
            if (race != null) {
                race.regattaId = regattaId;
                raceDataList.push(race);
            }
        });

        // Send the regatta data to the parent class
        onAddRegatta(serverUrl, regattaData, raceDataList);

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
                { /* Only render the submit and add race buttons if there are available races */
                    races.length > 0 &&
                    <div className={classes.actions}>
                        <button type="button" onClick={addRace}>Add Race</button>
                        { /* Only render the submit button if at least one race has been added to the regatta */
                            raceCount > 0 &&
                            <button>Create</button>
                        }
                    </div>
                }
            </form>
        </Card>
    );
}