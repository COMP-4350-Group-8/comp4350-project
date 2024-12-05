import React, {useState, useRef} from "react";
import { useNavigate } from 'react-router-dom';
import Card from '../ui/Card.jsx';
import PropTypes from "prop-types";
import MarkerForm from './MarkerForm.jsx';
import classes from "./Form.module.css";

// Define the props that should be passed to this component
CourseForm.propTypes = {
    serverUrl: PropTypes.string,
    onAddCourse: PropTypes.func,
}

// Renders a form to create a new race course, including all the markers it includes
export default function CourseForm({serverUrl, onAddCourse}) {
    // Used to navigate back to the homepage after submitting the course form
    const navigate = useNavigate();

    // References to the input fields to get their values
    const courseTitleInputRef = useRef();
    const courseDescriptionInputRef = useRef();

    // State for tracking the marker data, including that the course should start with 2 markers (start and finish)
    const [markerCount, setMarkerCount] = useState(2);
    const [markerData, setMarkerData] = useState([]);

    // Pass (after formatting) the data from the form to the parent component
    function submitHandler(event) {
        event.preventDefault();

        // Get the course data
        const courseId = Math.floor(Math.random() * (99999999)); 
        const courseTitle = courseTitleInputRef.current.value;
        const courseDesc = courseDescriptionInputRef.current.value;
        
        // Process and format the data for the course markers
        let markers = [];
        let gates = [];
        markerData.forEach((rawMarker, index) => {
            const markerId = Math.floor(Math.random() * (99999999));

            // Create the new marker object and add it to the array of markers
            let processedMarker = {
                id: markerId,
                latitude: Number(rawMarker.latitude),
                longitude: Number(rawMarker.longitude),
                description: rawMarker.description,
                rounding: rawMarker.round,
                isStartLine: index === 0,
                courseId: courseId
            };
            markers.push(processedMarker);

            // If the marker is a gate as well, record that so it can be used later to link the gates together
            if (rawMarker.gate != null && rawMarker.gate != undefined && rawMarker.gate != "") {
                // If a gate already exists for this marker's gate value, increment the count and record the marker's id
                const gateIndex = gates.findIndex(gate => gate.value === rawMarker.gate);
                if (gateIndex >= 0) {
                    gates[gateIndex].count += 1;
                    gates[gateIndex].gateIds.push(processedMarker.id);
                }
                // If a gate doesn't exist for this gate value, create a new one and record the marker's id
                else {
                    const gate = {
                        value: rawMarker.gate,
                        count: 1,
                        gateIds: [processedMarker.id]
                    }
                    gates.push(gate);
                }
            }
        });

        // Verify that each gate is in a set of 2
        let invalidData = false;
        gates.forEach((gate) => {
            if (gate.count != 2) {
                invalidData = true;
            }
        });
        if (invalidData) {
            alert("Your gates are not setup correctly");
            return;
        }

        // Combine the data into a single object so it can be sent to the parent class
        const data = {
            id: courseId,
            name: courseTitle,
            description: courseDesc,
            courseMarks: markers
        }

        // For every gate in the markers, create updated markers that are linked together in a new array.
        // This needs to happen in a separate array so it can be given separately to the add course handler.
        // This is because the gate ids can't be linked until after the corresponding markers are already created in the database.
        let gateMarkerData = [];
        gates.forEach((gate) => {
            // Get the markers that make up this gate
            const markerOne = markers.find(marker => marker.id === gate.gateIds[0]);
            const markerTwo = markers.find(marker => marker.id === gate.gateIds[1]);

            // This gate is the start line if either of the markers are the start line
            let isStartLine = false;
            if (markerOne.isStartLine || markerTwo.isStartLine) {
                isStartLine = true;
            }

            // Create the updated markers
            const gateOne = { ...markerOne };
            gateOne.gateId = markerTwo.id;
            gateOne.isStartLine = isStartLine;

            const gateTwo = { ...markerTwo };
            gateTwo.gateId = markerOne.id;
            gateTwo.isStartLine = isStartLine;

            gateMarkerData.push(gateOne);
            gateMarkerData.push(gateTwo);
        });

        // Send the course data to the parent class
        onAddCourse(serverUrl, data, gateMarkerData);

        // Move back to the homepage now that the course creation has finished
        navigate('/');
    };

    // Callback passed to the course markers so they can update the data when it changes
    const markerDataHandler = (index, data) => {
        // Update the marker data state with the new data, making sure to add new elements for new markers
        setMarkerData(prevMarkerData => {
            const updatedMarkerData = [...prevMarkerData];
            updatedMarkerData[index] = data;
            return updatedMarkerData;
        });
    };

    // Function used to update state so a new marker will be rendered in the form
    function addMarker() {
        setMarkerCount(markerCount+1)
    };

    // Create the forms for each course marker. This array is used while rendering to display the marker forms
    const markerForms = [];
    for (let i = 0; i < markerCount; i++) {
        let markerTitle = "";
        switch(i) {
            case 0:
                markerTitle = "Start Marker";
                break;
            case markerCount - 1:
                markerTitle = "Finish Marker";
                break;
            default:
                markerTitle = `Marker ${i}`;
                break;
        }
        markerForms.push(<MarkerForm key={i} index={i} markerTitle={markerTitle} onDataChanged={markerDataHandler}></MarkerForm>);
    };

    return(
        <Card>
            <form className={classes.form} onSubmit={submitHandler}>
                {/* Render the form elements for the course other than the markers */}
                <div className={classes.control}>
                    <label htmlFor='title'>Course Title</label>
                    <input type='text' required id='title' ref={courseTitleInputRef}/>
                </div>
                <div className={classes.control}>
                    <label htmlFor='description'>Description</label>
                    <textarea id='description' required rows='5' ref={courseDescriptionInputRef}></textarea>
                </div>
                {/* Render all the marker forms */}
                {markerForms}
                {/* Render the control buttons (Add Marker, Create) at the bottom of the form */}
                <div className={classes.actions}>
                    <button type="button" onClick={addMarker}>Add Marker</button>
                    <button>Create</button>
                </div>
            </form>
        </Card>
    );
}