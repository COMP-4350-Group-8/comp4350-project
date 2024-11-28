import React, {useState, useRef} from "react";
import { useNavigate } from 'react-router-dom';
import Card from '../ui/Card.jsx';
import PropTypes from "prop-types";
import MarkerForm from './MarkerForm.jsx';
import classes from "./Form.module.css";

// Define the props that should be passed to this component
CourseForm.propTypes = {
    onAddCourse: PropTypes.func,
}

// Renders a form to create a new race course, including all the markers it includes
export default function CourseForm({onAddCourse}) {
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
        markerData.map((rawMarker) => {
            let processedMarker = {
                id: Math.floor(Math.random() * (99999999)),
                latitude: Number(rawMarker.latitude),
                longitude: Number(rawMarker.longitude),
                description: rawMarker.description,
                rounding: rawMarker.round,
                // isStartLine: index === 0,
                isStartLine: false,
                //gate: rawMarker.gate,
                //course: courseTitle,
                courseId: courseId
            };
            markers.push(processedMarker);
        });

        // Combine the data into a single object so it can be sent to the parent class
        const data = {
            id: courseId,
            name: courseTitle,
            description: courseDesc,
            courseMarks: markers
        }

        // Send the course data to the parent class
        onAddCourse(data);

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
                <div className={classes.control}>
                    <label htmlFor='notes'>Notes</label>
                    <textarea id='notes' required rows='5'></textarea>
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