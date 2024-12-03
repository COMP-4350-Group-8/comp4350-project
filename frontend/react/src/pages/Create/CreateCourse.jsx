import React from "react";
import PropTypes from 'prop-types';
import CourseForm from "../../components/forms/CourseForm.jsx";
import addCourseHandler from "../../utils/AddCourse.jsx";
import "./Create.css";

// Define the props that should be passed to this component
CreateCourse.propTypes = {
    serverUrl: PropTypes.string
}

// Render the CourseForm and pass it a function to call when creating the race course
export default function CreateCourse({serverUrl})  {
    return (
        <>
            <h1 className="create-header">Create Course</h1>
            <CourseForm serverUrl={serverUrl} onAddCourse={addCourseHandler}/>
        </>
    );
}