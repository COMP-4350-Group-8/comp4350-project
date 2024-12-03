import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import PropTypes from 'prop-types';
import Card from '../../components/ui/Card';
import getRace from '../../utils/GetRace';
import "../Home.css";

// Define the props that should be passed to this component
ViewRace.propTypes = {
    serverUrl: PropTypes.string
}

// Render the CourseForm and pass it a function to call when creating the race course
export default function ViewRace({serverUrl})  {
    const navigate = useNavigate();
    const { id } = useParams();

    const handleViewCourseClick = (id) => {
        navigate(`/view/course/${id}`)
    };

    const [raceData, setRaceData] = useState([]);
    useEffect(() => {
        getRace(serverUrl, id, setRaceData);
    }, [id]);

    const courses = [];
    if (raceData.courses != null) {
        raceData.courses.map(course => {
            courses.push(
                <button className="item-button" key={course.name} onClick={() => handleViewCourseClick(course.id)}>
                    <p>{course.name}</p>
                </button>
            )
        });
    }

    return (
        <>
            <h1 className="create-header">{raceData.name}</h1>
            <Card>
                <>
                    <div className={`card-top-bar ${courses.length === 0 ? "no-border" : ""}` /* Only show the border if there are items in the list */}>
                        <h1 className="card-header">Courses</h1>
                    </div>
                    <div className="item-list">
                        {courses}
                    </div>
                </>
            </Card>
        </>
    );
}