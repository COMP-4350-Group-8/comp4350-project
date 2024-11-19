import React, {useState, useRef} from "react";
import { useNavigate } from 'react-router-dom';
import Card from '../ui/Card.jsx';
import getCourses from "../../utils/GetCourses.jsx";
import PropTypes from "prop-types";
import classes from "./Form.module.css";

// Define the props that should be passed to this component
RaceForm.propTypes = {
    onAddRace: PropTypes.func,
}

// Renders a form to create a new race, including all the markers it includes
export default function RaceForm({onAddRace}) {
    // Get all the available courses
    const courses = getCourses();

    // Used to navigate back to the homepage after submitting the race form
    const navigate = useNavigate();

    // References to the input fields to get their values
    const raceTitleInputRef = useRef();

    // Store and update the currently selected course's id
    const [selectedCourse, setSelectedCourse] = useState(courses.length > 0 ? courses[0].id : -1);
    const handleCourseSelection = (event) => {
        setSelectedCourse(event.target.value);
    }

    // Create a dropdown with all the courses as options, or just text if there are no courses available
    let courseDropdown = <></>;
    if (courses.length === 0) {
        courseDropdown = <p>Sorry, you haven't created any courses yet</p>
    } else {
        const courseOptions = [];
        courses.map((course) => {
            courseOptions.push(<option key={course.id} value={course.id}>{course.name}</option>)
        })
        courseDropdown =
            <select value={selectedCourse} onChange={handleCourseSelection}>
                {courseOptions}
            </select>
    }

    // Pass (after formatting) the data from the form to the parent component
    function submitHandler(event) {
        event.preventDefault();

        // Don't submit the form if there are no courses available
        if(courses.length === 0) {
            return;
        }

        const raceTitle = raceTitleInputRef.current.value;

        // Combine the data into a single object so it can be sent to the parent class
        const data = {
            id: Math.floor(Math.random() * (99999999)),
            name: raceTitle,
            courseId: selectedCourse
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
                {/* Render the dropdown to select the course for the race */}
                <div className={classes.control}>
                    <label htmlFor='course'>Course</label>
                    {courseDropdown}
                </div>
                { /* Only render the submit button if there are available courses */
                    courses.length > 0 &&
                    <div className={classes.actions}>
                        <button>Create</button>
                    </div>
                }
            </form>
        </Card>
    );
}