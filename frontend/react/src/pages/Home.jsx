import React from 'react';
import { useNavigate } from 'react-router-dom';
import "./Home.css"

export default  function Home()  {
    const navigate = useNavigate()

    // onClick function for the start race button that sends the user to the CreateRace page
    const handleCreateClick = () => {
        navigate('/create')
    }

    const handleViewClick = () => {
        navigate('/view-course')
    }

    return (
        <>
            <div className="home-page">
                <h1 className="home-header"> SAIL MAPPER</h1>
            </div>
            <button onClick={handleCreateClick} className="start-race-button">Start Race</button>
            <button onClick={handleViewClick} className="start-race-button">View Race</button>
        </>
    );
}
