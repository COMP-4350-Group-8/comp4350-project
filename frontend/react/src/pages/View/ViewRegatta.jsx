import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import PropTypes from 'prop-types';
import Card from '../../components/ui/Card';
import getRegatta from '../../utils/GetRegatta';
import "../Home.css";
import "./ViewRegatta.css"

// Define the props that should be passed to this component
ViewRegatta.propTypes = {
    serverUrl: PropTypes.string
}

// Render the CourseForm and pass it a function to call when creating the race course
export default function ViewRegatta({serverUrl})  {
    const navigate = useNavigate();
    const { id } = useParams();

    const handleViewRaceClick = (id) => {
        navigate(`/view/race/${id}`)
    };

    const [regattaData, setRegattaData] = useState([]);
    useEffect(() => {
        getRegatta(serverUrl, id, setRegattaData);
    }, [serverUrl, id]);

    const races = [];
    if (regattaData.races != null) {
        regattaData.races.map(race => {
            races.push(
                <button className="item-button" key={race.name} onClick={() => handleViewRaceClick(race.id)}>
                    <p>{race.name}</p>
                </button>
            )
        });
    }

    return (
        <>
            <h1 className="create-header">{regattaData.name}</h1>
            <Card>
                <>
                    <div className="regatta-description">
                        <div className="regatta-boxhead">DESCRIPTION</div>
                        <div className="regatta-para">{regattaData.description}</div>
                    </div>
                    <div className={`card-top-bar ${races.length === 0 ? "no-border" : ""}` /* Only show the border if there are items in the list */}>
                        <h1 className="card-header">Races</h1>
                    </div>
                    <div className="item-list">
                        {races}
                    </div>
                </>
            </Card>
        </>
    );
}