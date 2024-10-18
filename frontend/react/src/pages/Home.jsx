import React from 'react';
import { useNavigate } from 'react-router-dom';
import "./Home.css"

export default  function Home()  {
    const navigate = useNavigate()

    const handleCreateClick = () => {
        navigate('/create')
    }

    return (
        <>
            <div className="home-page">
                <h1 className= "home-header"> SAIL MAPPER</h1>
            </div>
            <button onClick={handleCreateClick} className="start-race-button">Start Race</button>
        </>
    );
}
