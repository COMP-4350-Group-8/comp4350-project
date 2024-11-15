import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import Card from '../components/ui/Card';
import "./Home.css"
import getCourses from '../utils/GetCourses';

export default  function Home()  {
    const navigate = useNavigate()

    // onClick function for the start race button that sends the user to the CreateRace page
    const handleCreateRegattaClick = () => {
        navigate('/create/regatta')
    }
    const handleCreateRaceClick = () => {
        navigate('/create/race')
    }
    const handleCreateCourseClick = () => {
        navigate('/create/course')
    }

    const [courseData, setCourseData] = useState(getCourses());

    // setCourseData();

    const courses = [];
    courseData.map(course => {
        courses.push(
            <div key={course.name}>
                <p>{course.name}</p>
            </div>
        )
    });

    return (
        <>
            <div className="home-page">
                <h1 className= "home-header">SAIL MAPPER</h1>
            </div>
            <Card>
                <>
                    <div className="card-top-bar">
                        <h1 className="card-header">Courses</h1>
                        <button onClick={handleCreateCourseClick} className="create-button">Create Course</button>
                    </div>
                    <div>
                        <p>Hello</p>
                        {courses}
                    </div>
                </>
            </Card>
            <Card>
                <>
                    <div className="card-top-bar">
                        <h1 className="card-header">Courses</h1>
                        <button onClick={handleCreateCourseClick} className="create-button">Create Course</button>
                    </div>
                    <div>
                        <p>Hello</p>
                        {courses}
                    </div>
                </>
            </Card>
            <Card>
                <>
                    <div className="card-top-bar">
                        <h1 className="card-header">Courses</h1>
                        <button onClick={handleCreateCourseClick} className="create-button">Create Course</button>
                    </div>
                    <div>
                        <p>Hello</p>
                        {courses}
                    </div>
                </>
            </Card>
        </>
    );
}
