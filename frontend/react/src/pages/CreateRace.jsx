import React from "react";
import RaceForm from "../components/forms/RaceForm.jsx";
import addCourseHandler from "./AddCourse.jsx";
import "./CreateRace.css";

export default function CreateRace()  {
    return (
        <RaceForm  onAddCourse ={addCourseHandler}/>
    );
}