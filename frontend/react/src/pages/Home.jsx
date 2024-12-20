import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import Card from '../components/ui/Card';
import getRegattas from '../utils/GetRegattas';
import getRaces from '../utils/GetRaces';
import getCourses from '../utils/GetCourses';
import PropTypes from 'prop-types';
import "./Home.css";

// Define the props that should be passed to this component
Home.propTypes = {
    serverUrl: PropTypes.string
}

export default  function Home({serverUrl})  {
    const navigate = useNavigate()

    // onClick functions that send the user to the CreateRegatta, CreateRace, or CreateCourse page
    const handleCreateRegattaClick = () => {
        navigate('/create/regatta')
    };
    const handleCreateRaceClick = () => {
        navigate('/create/race')
    };
    const handleCreateCourseClick = () => {
        navigate('/create/course')
    };

    const handleViewRegattaClick = (id) => {
        navigate(`/view/regatta/${id}`)
    };
    const handleViewRaceClick = (id) => {
        navigate(`/view/race/${id}`)
    };
    const handleViewCourseClick = (id) => {
        navigate(`/view/course/${id}`)
    };

    // State for the regatta, race, and course data
    const [regattaData, setRegattaData] = useState([]);
    const [raceData, setRaceData] = useState([]);
    const [courseData, setCourseData] = useState([]);

    useEffect(() => {
        getRegattas(serverUrl, setRegattaData);
    }, [serverUrl]);
    useEffect(() => {
        getRaces(serverUrl, setRaceData);
    }, [serverUrl]);
    useEffect(() => {
        getCourses(serverUrl, setCourseData);
    }, [serverUrl]);

    // Functions to convert the data to an array of buttons so they can be easily rendered later
    const regattas = [];
    regattaData.map(regatta => {
        regattas.push(
            <button className="item-button" key={regatta.id} onClick={() => handleViewRegattaClick(regatta.id)}>
                <p>{regatta.name}</p>
            </button>
        )
    });

    const races = [];
    raceData.map(race => {
        races.push(
            <button className="item-button" key={race.id} onClick={() => handleViewRaceClick(race.id)}>
                <p>{race.name}</p>
            </button>
        )
    });

    const courses = [];
    courseData.map(course => {
        courses.push(
            <button className="item-button" key={course.id} onClick={() => handleViewCourseClick(course.id)}>
                <p>{course.name}</p>
            </button>
        )
    });

    const onClickServerUrl = () => {
        navigate("/setServerUrl");
    }

    return (
        <>
            <div className="home-page">
                <h1 className= "home-header">SAIL MAPPER</h1>
            </div>
            <div className="card-list">
                {/* Regattas */}
                <Card>
                    <>
                        <div className={`card-top-bar ${regattas.length === 0 ? "no-border" : ""}` /* Only show the border if there are items in the list */}>
                            <h1 className="card-header">Regattas</h1>
                            <button onClick={handleCreateRegattaClick} className="create-button">Create Regatta</button>
                        </div>
                        <div className="item-list">
                            {regattas}
                        </div>
                    </>
                </Card>
                {/* Races */}
                <Card>
                    <>
                    <div className={`card-top-bar ${races.length === 0 ? "no-border" : ""}` /* Only show the border if there are items in the list */}>
                            <h1 className="card-header">Races</h1>
                            <button onClick={handleCreateRaceClick} className="create-button">Create Race</button>
                        </div>
                        <div className="item-list">
                            {races}
                        </div>
                    </>
                </Card>
                {/* Courses */}
                <Card>
                    <>
                    <div className={`card-top-bar ${courses.length === 0 ? "no-border" : ""}` /* Only show the border if there are items in the list */}>
                            <h1 className="card-header">Courses</h1>
                            <button onClick={handleCreateCourseClick} className="create-button">Create Course</button>
                        </div>
                        <div className="item-list">
                            {courses}
                        </div>
                    </>
                </Card>
            </div>
            <div>
                <button className="set-server-url-button" onClick={onClickServerUrl}>Set Server URL</button>
            </div>
        </>
    );
}
