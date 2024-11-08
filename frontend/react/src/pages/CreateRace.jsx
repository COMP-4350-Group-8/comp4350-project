import React from "react";
import RaceForm from "../components/forms/RaceForm.jsx";
import addCourseHandler from "./AddCourse.jsx";
import "./CreateRace.css";

// Render the RaceForm and pass it a function to call when creating the race course
export default function CreateRace()  {
    return (
        <RaceForm onAddCourse={addCourseHandler}/>
    );
}