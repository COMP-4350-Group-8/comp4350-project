import React, { useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import Card from '../components/ui/Card';
import getRegatta from '../utils/GetRegatta';
import "./Home.css";

// Render the CourseForm and pass it a function to call when creating the race course
export default function ViewRegatta()  {
    const navigate = useNavigate();
    const { regattaId } = useParams();

    const [regattaData,] = useState(getRegatta(regattaId));

    const races = [];
    regattaData.races.map(race => {
        races.push(
            <button className="item-button" key={race.name}>
                <p>{race.name}</p>
            </button>
        )
    });

    return (
        <>
            <h1 className="create-header">{regattaData.name}</h1>
            <Card>
                <>
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