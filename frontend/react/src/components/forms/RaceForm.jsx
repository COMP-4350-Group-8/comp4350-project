import {useState, useRef} from "react";
import { useNavigate } from 'react-router-dom';
import Card from '../ui/Card';
import {v4 as uuid} from"uuid";
import MarkerForm from './MarkerForm.jsx';
import classes from "./RaceForm.module.css";

export default function RaceForm({onAddCourse}) {
    const navigate = useNavigate();

    const raceTitleInputRef = useRef();
    const raceDescriptionInputRef = useRef();

    const [markerCount, setMarkerCount] = useState(2);
    const [markerData, setMarkerData] = useState([]);

    // Pass (after formatting) the data from the form to the parent component
    function submitHandler(event) {
        event.preventDefault();
        const courseId = uuid();
        const courseTitle = raceTitleInputRef.current.value;
        const courseDesc = raceDescriptionInputRef.current.value;
        let markers = [];

        // Process and format the course markers
        markerData.map((rawMarker, index) => {
            let processedMarker = {
                id: uuid(),
                latitude: rawMarker.latitude,
                longitude: rawMarker.longitude,
                description: rawMarker.description,
                rounding: rawMarker.round,
                isStartLine: index === 0,
                gate: rawMarker.gate,
                course: courseTitle,
                courseId: courseId
            };
            markers.push(processedMarker);
        });

        const data = {
            id: courseId,
            name: courseTitle,
            description: courseDesc,
            courseMarks: markers
        }

        onAddCourse(data);

        navigate('/');
    };

    // Callback passed to the course markers so they can update the data
    const markerDataHandler = (index, data) => {
        console.log(`Marker ${index} changed, ${JSON.stringify(data, null, 4)}`);

        setMarkerData(prevMarkerData => {
            const updatedMarkerData = [...prevMarkerData];
            updatedMarkerData[index] = data;
            return updatedMarkerData;
        });

        console.log(`Markers: ${JSON.stringify(markerData, null, 4)}`);
    };

    function addMarker() {
        setMarkerCount(markerCount+1)
    };

    // Create the forms for each course marker
    const markerForms = [];
    for (let i = 0; i < markerCount; i++) {
        let markerTitle = "";
        switch(i) {
            case 0:
                markerTitle = "Start Marker";
                break;
            case markerCount - 1:
                markerTitle = "Finish Marker";
                break;
            default:
                markerTitle = `Marker ${i}`;
                break;
        }
        markerForms.push(<MarkerForm key={i} index={i} markerTitle={markerTitle} onDataChanged={markerDataHandler}></MarkerForm>);
    };

    return(
        <Card>
            <form className={classes.form} onSubmit={submitHandler}>
                <div className={classes.control}>
                    <label htmlFor='title'>Course Title</label>
                    <input type='text' required id='title' ref={raceTitleInputRef}/>
                </div>
                <div className={classes.control}>
                    <label htmlFor='description'>Description</label>
                    <textarea id='description' required rows='5' ref={raceDescriptionInputRef}></textarea>
                </div>
                <div className={classes.control}>
                    <label htmlFor='notes'>Notes</label>
                    <textarea id='notes' required rows='5'></textarea>
                </div>
                {markerForms}
                <div className={classes.actions}>
                    <button type="button" onClick={addMarker}>Add Marker</button>
                    <button>Create</button>
                </div>
            </form>
        </Card>
    );
}