import React from "react";
import CourseForm from "../components/forms/CourseForm.jsx";
import addCourseHandler from "../utils/AddCourse.jsx";
import "./Create.css";

// Render the CourseForm and pass it a function to call when creating the race course
export default function CreateCourse()  {
    return (
        <>
            <h1 className="create-header">Create Course</h1>
            <CourseForm onAddCourse={addCourseHandler}/>
        </>
    );
}