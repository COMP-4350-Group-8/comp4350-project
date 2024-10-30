import Card from '../ui/Card';
import Gmap from "../map/Gmap.jsx"
import classes from "./RaceForm.module.css"
import {useState, useRef} from "react";
import { useNavigate } from 'react-router-dom';
import {v4 as uuid} from"uuid";

export default function RaceForm(props) {
    const navigate = useNavigate()

    const raceTitleInputRef = useRef();
    const raceDescriptionInputRef = useRef();

    const startDescriptionInputRef = useRef();
    const startGateInputRef = useRef();

    const finishDescriptionInputRef = useRef();
    const finishGateInputRef = useRef();

    const marker1DescriptionInputRef = useRef();
    const marker1GateInputRef = useRef();

    const marker2DescriptionInputRef = useRef();
    const marker2GateInputRef = useRef();

    const marker3DescriptionInputRef = useRef();
    const marker3GateInputRef = useRef();

    const marker4DescriptionInputRef = useRef();
    const marker4GateInputRef = useRef();

    const marker5DescriptionInputRef = useRef();
    const marker5GateInputRef = useRef();

    const marker6DescriptionInputRef = useRef();
    const marker6GateInputRef = useRef();

    const marker7DescriptionInputRef = useRef();
    const marker7GateInputRef = useRef();

    const marker8DescriptionInputRef = useRef();
    const marker8GateInputRef = useRef();

    // Initial state of the "Rounding" checkbox for starting coordinates
    const [startRoundIsChecked, setStartRoundIsChecked] = useState(false)

    // Initial state of the "Rounding" checkbox for finishing coordinates
    const [finishRoundIsChecked, setFinishRoundIsChecked] = useState(false)

    // Initial state of the "Rounding" checkbox for Marker 1 coordinates
    const [marker1RoundIsChecked, setMarker1RoundIsChecked] = useState(false)

    // Initial state of the "Rounding" checkbox for Marker 2 coordinates
    const [marker2RoundIsChecked, setMarker2RoundIsChecked] = useState(false)

    // Initial state of the "Rounding" checkbox for Marker 3 coordinates
    const [marker3RoundIsChecked, setMarker3RoundIsChecked] = useState(false)

    // Initial state of the "Rounding" checkbox for Marker 4 coordinates
    const [marker4RoundIsChecked, setMarker4RoundIsChecked] = useState(false)

    // Initial state of the "Rounding" checkbox for Marker 5 coordinates
    const [marker5RoundIsChecked, setMarker5RoundIsChecked] = useState(false)

    // Initial state of the "Rounding" checkbox for Marker 6 coordinates
    const [marker6RoundIsChecked, setMarker6RoundIsChecked] = useState(false)

    // Initial state of the "Rounding" checkbox for Marker 7 coordinates
    const [marker7RoundIsChecked, setMarker7RoundIsChecked] = useState(false)

    // Initial state of the "Rounding" checkbox for Marker 8 coordinates
    const [marker8RoundIsChecked, setMarker8RoundIsChecked] = useState(false)

    // Initial state of the "Do you need this Marker" checkbox in Marker 1
    const [isMarker1Needed, setIsMarker1Needed] = useState(false)

    // Initial state of the "Do you need this Marker" checkbox in Marker 2
    const [isMarker2Needed, setIsMarker2Needed] = useState(false)

    // Initial state of the "Do you need this Marker" checkbox in Marker 3
    const [isMarker3Needed, setIsMarker3Needed] = useState(false)

    // Initial state of the "Do you need this Marker" checkbox in Marker 4
    const [isMarker4Needed, setIsMarker4Needed] = useState(false)

    // Initial state of the "Do you need this Marker" checkbox in Marker 5
    const [isMarker5Needed, setIsMarker5Needed] = useState(false)

    // Initial state of the "Do you need this Marker" checkbox in Marker 6
    const [isMarker6Needed, setIsMarker6Needed] = useState(false)

    // Initial state of the "Do you need this Marker" checkbox in Marker 7
    const [isMarker7Needed, setIsMarker7Needed] = useState(false)

    // Initial state of the "Do you need this Marker" checkbox in Marker 8
    const [isMarker8Needed, setIsMarker8Needed] = useState(false)

    // Initial coordinates for the start of race location
    const [startCoordinates, setStartCoordinates] = useState({ lat: 1000, lng: 1000});

    // Initial coordinates for the  end of race location
    const [finishCoordinates, setFinishCoordinates] = useState({ lat: 1000, lng: 1000});

    // Initial coordinates for the Marker 1 of race location
    const [marker1Coordinates, setMarker1Coordinates] = useState({ lat: 1000, lng: 1000});

    // Initial coordinates for the Marker 2 of race location
    const [marker2Coordinates, setMarker2Coordinates] = useState({ lat: 1000, lng: 1000});

    // Initial coordinates for the Marker 3 of race location
    const [marker3Coordinates, setMarker3Coordinates] = useState({ lat: 1000, lng: 1000});

    // Initial coordinates for the Marker 4 of race location
    const [marker4Coordinates, setMarker4Coordinates] = useState({ lat: 1000, lng: 1000});

    // Initial coordinates for the Marker 5 of race location
    const [marker5Coordinates, setMarker5Coordinates] = useState({ lat: 1000, lng: 1000});

    // Initial coordinates for the Marker 6 of race location
    const [marker6Coordinates, setMarker6Coordinates] = useState({ lat: 1000, lng: 1000});

    // Initial coordinates for the Marker 7 of race location
    const [marker7Coordinates, setMarker7Coordinates] = useState({ lat: 1000, lng: 1000});

    // Initial coordinates for the Marker 8 of race location
    const [marker8Coordinates, setMarker8Coordinates] = useState({ lat: 1000, lng: 1000});

    // Handlers for latitude and longitude input changes for starting coordinates
    const handleStartLatChange = (event) => {
        setStartCoordinates((prev) => ({ ...prev, lat: event.target.value }));
    };

    const handleStartLngChange = (event) => {
        setStartCoordinates((prev) => ({ ...prev, lng: event.target.value }));
    };

    // Handlers for latitude and longitude input changes for finishing coordinates
    const handleFinishLatChange = (event) => {
        setFinishCoordinates((prev) => ({ ...prev, lat: event.target.value }));
    };

    const handleFinishLngChange = (event) => {
        setFinishCoordinates((prev) => ({ ...prev, lng: event.target.value }));
    };

    // Handlers for latitude and longitude input changes for marker1 coordinates
    const handleMarker1LatChange = (event) => {
        setMarker1Coordinates((prev) => ({ ...prev, lat: event.target.value }));
    };

    const handleMarker1LngChange = (event) => {
        setMarker1Coordinates((prev) => ({ ...prev, lng: event.target.value }));
    };

    // Handlers for latitude and longitude input changes for marker2 coordinates
    const handleMarker2LatChange = (event) => {
        setMarker2Coordinates((prev) => ({ ...prev, lat: event.target.value }));
    };

    const handleMarker2LngChange = (event) => {
        setMarker2Coordinates((prev) => ({ ...prev, lng: event.target.value }));
    };

    // Handlers for latitude and longitude input changes for marker3 coordinates
    const handleMarker3LatChange = (event) => {
        setMarker3Coordinates((prev) => ({ ...prev, lat: event.target.value }));
    };

    const handleMarker3LngChange = (event) => {
        setMarker3Coordinates((prev) => ({ ...prev, lng: event.target.value }));
    };

    // Handlers for latitude and longitude input changes for marker4 coordinates
    const handleMarker4LatChange = (event) => {
        setMarker4Coordinates((prev) => ({ ...prev, lat: event.target.value }));
    };

    const handleMarker4LngChange = (event) => {
        setMarker4Coordinates((prev) => ({ ...prev, lng: event.target.value }));
    };

    // Handlers for latitude and longitude input changes for marker5 coordinates
    const handleMarker5LatChange = (event) => {
        setMarker5Coordinates((prev) => ({ ...prev, lat: event.target.value }));
    };

    const handleMarker5LngChange = (event) => {
        setMarker5Coordinates((prev) => ({ ...prev, lng: event.target.value }));
    };

    // Handlers for latitude and longitude input changes for marker6 coordinates
    const handleMarker6LatChange = (event) => {
        setMarker6Coordinates((prev) => ({ ...prev, lat: event.target.value }));
    };

    const handleMarker6LngChange = (event) => {
        setMarker6Coordinates((prev) => ({ ...prev, lng: event.target.value }));
    };

    // Handlers for latitude and longitude input changes for marker7 coordinates
    const handleMarker7LatChange = (event) => {
        setMarker7Coordinates((prev) => ({ ...prev, lat: event.target.value }));
    };

    const handleMarker7LngChange = (event) => {
        setMarker7Coordinates((prev) => ({ ...prev, lng: event.target.value }));
    };

    // Handlers for latitude and longitude input changes for marker8 coordinates
    const handleMarker8LatChange = (event) => {
        setMarker8Coordinates((prev) => ({ ...prev, lat: event.target.value }));
    };

    const handleMarker8LngChange = (event) => {
        setMarker8Coordinates((prev) => ({ ...prev, lng: event.target.value }));
    };


    function submitHandler(event) {
        event.preventDefault();
        const courseId = uuid()
        const courseTitle = raceTitleInputRef.current.value
        const courseDesc = raceDescriptionInputRef.current.value
        let markers = []


        addMarkers(markers, markers.length, startCoordinates.lat, startCoordinates.lng, startDescriptionInputRef.current.value, startRoundIsChecked, true, startGateInputRef.current.value, courseTitle, courseId)
        addMarkers(markers, markers.length, finishCoordinates.lat, finishCoordinates.lng, finishDescriptionInputRef.current.value, finishRoundIsChecked, false, finishGateInputRef.current.value, courseTitle, courseId)
        {isMarker1Needed && addMarkers(markers, markers.length, marker1Coordinates.lat, marker1Coordinates.lng, marker1DescriptionInputRef.current.value, marker1RoundIsChecked, false, marker1GateInputRef.current.value, courseTitle, courseId)}
        {isMarker2Needed && addMarkers(markers, markers.length, marker2Coordinates.lat, marker2Coordinates.lng, marker2DescriptionInputRef.current.value, marker2RoundIsChecked, false, marker2GateInputRef.current.value, courseTitle, courseId)}
        {isMarker3Needed && addMarkers(markers, markers.length, marker3Coordinates.lat, marker3Coordinates.lng, marker3DescriptionInputRef.current.value, marker3RoundIsChecked, false, marker3GateInputRef.current.value, courseTitle, courseId)}
        {isMarker4Needed && addMarkers(markers, markers.length, marker4Coordinates.lat, marker4Coordinates.lng, marker4DescriptionInputRef.current.value, marker4RoundIsChecked, false, marker4GateInputRef.current.value, courseTitle, courseId)}
        {isMarker5Needed && addMarkers(markers, markers.length, marker5Coordinates.lat, marker5Coordinates.lng, marker5DescriptionInputRef.current.value, marker5RoundIsChecked, false, marker5GateInputRef.current.value, courseTitle, courseId)}
        {isMarker6Needed && addMarkers(markers, markers.length, marker6Coordinates.lat, marker6Coordinates.lng, marker6DescriptionInputRef.current.value, marker6RoundIsChecked, false, marker6GateInputRef.current.value, courseTitle, courseId)}
        {isMarker7Needed && addMarkers(markers, markers.length, marker7Coordinates.lat, marker7Coordinates.lng, marker7DescriptionInputRef.current.value, marker7RoundIsChecked, false, marker7GateInputRef.current.value, courseTitle, courseId)}
        {isMarker8Needed && addMarkers(markers, markers.length, marker8Coordinates.lat, marker8Coordinates.lng, marker8DescriptionInputRef.current.value, marker8RoundIsChecked, false, marker8GateInputRef.current.value, courseTitle, courseId)}

        const data = {
            id: courseId,
            name: courseTitle,
            description: courseDesc,
            courseMarks: markers
        }

        props.onAddCourse(data);

        navigate('/')

    }

    function addMarkers(array,pos,  latitude, longitude, desc, round, start, gateInfo, courseName, cId) {
        let mId = uuid()
        let str = {
            id: mId,
            latitude: latitude,
            longitude: longitude,
            description: desc,
            rounding: round,
            isStartLine: start,
            gate: gateInfo,
            course: courseName,
            courseId:cId
        }
        array[pos] = str
    }

    //changing state of the "Rounding" checkbox of the start coordinates
    const startRoundedCheckHandler = () => {
        setStartRoundIsChecked(!startRoundIsChecked)
    }

    //changing state of the "Rounding" checkbox of the start coordinates
    const finishRoundedCheckHandler = () => {
        setFinishRoundIsChecked(!finishRoundIsChecked)
    }

    //changing state of the "Rounding" checkbox of the Marker 1 coordinates
    const marker1RoundedCheckHandler = () => {
        setMarker1RoundIsChecked(!marker1RoundIsChecked)
    }

    //changing state of the "Rounding" checkbox of the Marker 2 coordinates
    const marker2RoundedCheckHandler = () => {
        setMarker2RoundIsChecked(!marker2RoundIsChecked)
    }

    //changing state of the "Rounding" checkbox of the Marker 3 coordinates
    const marker3RoundedCheckHandler = () => {
        setMarker3RoundIsChecked(!marker3RoundIsChecked)
    }

    //changing state of the "Rounding" checkbox of the Marker 4 coordinates
    const marker4RoundedCheckHandler = () => {
        setMarker4RoundIsChecked(!marker4RoundIsChecked)
    }

    //changing state of the "Rounding" checkbox of the Marker 5 coordinates
    const marker5RoundedCheckHandler = () => {
        setMarker5RoundIsChecked(!marker5RoundIsChecked)
    }

    //changing state of the "Rounding" checkbox of the Marker 6 coordinates
    const marker6RoundedCheckHandler = () => {
        setMarker6RoundIsChecked(!marker6RoundIsChecked)
    }

    //changing state of the "Rounding" checkbox of the Marker 7 coordinates
    const marker7RoundedCheckHandler = () => {
        setMarker7RoundIsChecked(!marker7RoundIsChecked)
    }

    //changing state of the "Rounding" checkbox of the Marker 8 coordinates
    const marker8RoundedCheckHandler = () => {
        setMarker8RoundIsChecked(!marker8RoundIsChecked)
    }

    //changing state of the "Do you need this Marker?" checkbox of the Marker 1 coordinates
    const needMarker1CheckHandler = () => {
        setIsMarker1Needed(!isMarker1Needed)
    }

    //changing state of the "Do you need this Marker?" checkbox of the Marker 2 coordinates
    const needMarker2CheckHandler = () => {
        setIsMarker2Needed(!isMarker2Needed)
    }

    //changing state of the "Do you need this Marker?" checkbox of the Marker 3 coordinates
    const needMarker3CheckHandler = () => {
        setIsMarker3Needed(!isMarker3Needed)
    }

    //changing state of the "Do you need this Marker?" checkbox of the Marker 4 coordinates
    const needMarker4CheckHandler = () => {
        setIsMarker4Needed(!isMarker4Needed)
    }

    //changing state of the "Do you need this Marker?" checkbox of the Marker 5 coordinates
    const needMarker5CheckHandler = () => {
        setIsMarker5Needed(!isMarker5Needed)
    }

    //changing state of the "Do you need this Marker?" checkbox of the Marker 6 coordinates
    const needMarker6CheckHandler = () => {
        setIsMarker6Needed(!isMarker6Needed)
    }

    //changing state of the "Do you need this Marker?" checkbox of the Marker 7 coordinates
    const needMarker7CheckHandler = () => {
        setIsMarker7Needed(!isMarker7Needed)
    }

    //changing state of the "Do you need this Marker?" checkbox of the Marker 8 coordinates
    const needMarker8CheckHandler = () => {
        setIsMarker8Needed(!isMarker8Needed)
    }

    return (
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
                <div className={classes.mapbox}>
                    <h4>Starting Coordinates</h4>

                    {/* Inputs to change latitude and longitude */}
                    <div className={classes.control}>
                        <label>
                            Latitude:
                            <input
                                type="number"
                                value={startCoordinates.lat}
                                onChange={handleStartLatChange}
                                placeholder="Latitude"
                            />
                        </label>
                    </div>
                    <div className={classes.control}>
                        <label className={classes.control}>
                            Longitude:
                            <input
                                type="number"
                                value={startCoordinates.lng}
                                onChange={handleStartLngChange}
                                placeholder="Longitude"
                            />
                        </label>
                    </div>
                    <div className={classes.control}>
                        <label htmlFor='description'>Description</label>
                        <textarea id='description' required rows='1' ref={startDescriptionInputRef}></textarea>
                    </div>
                    <div>
                        <input type="checkbox" id="checkbox" checked={startRoundIsChecked} onChange={startRoundedCheckHandler}/>
                        <label htmlFor="checkbox">Rounding</label>
                    </div>
                    <div className={classes.control}>
                        <label htmlFor='title'>Gate</label>
                        <input type='text' required id='title' ref={startGateInputRef}/>
                    </div>

                    {/* Pass coordinates as props to the map component */}
                    <Gmap latitude={startCoordinates.lat} longitude={startCoordinates.lng}/>
                </div>

                <div className={classes.mapbox}>
                    <h4>Finishing Coordinates</h4>

                    {/* Inputs to change latitude and longitude */}
                    <div className={classes.control}>
                        <label>
                            Latitude:
                            <input
                                type="number"
                                value={finishCoordinates.lat}
                                onChange={handleFinishLatChange}
                                placeholder="Latitude"
                            />
                        </label>
                    </div>
                    <div className={classes.control}>
                        <label>
                            Longitude:
                            <input
                                type="number"
                                value={finishCoordinates.lng}
                                onChange={handleFinishLngChange}
                                placeholder="Longitude"
                            />
                        </label>
                    </div>
                    <div className={classes.control}>
                        <label htmlFor='description'>Description</label>
                        <textarea id='description' required rows='1' ref={finishDescriptionInputRef}></textarea>
                    </div>
                    <div>
                        <input type="checkbox" id="checkbox" checked={finishRoundIsChecked} onChange={finishRoundedCheckHandler}/>
                        <label htmlFor="checkbox">Rounding</label>
                    </div>
                    <div className={classes.control}>
                        <label htmlFor='title'>Gate</label>
                        <input type='text' required id='title' ref={finishGateInputRef}/>
                    </div>

                    {/* Pass coordinates as props to the map component */}
                    <Gmap latitude={finishCoordinates.lat} longitude={finishCoordinates.lng}/>
                </div>

                <div className={classes.mapbox}>
                    <h4>Marker 1</h4>
                    <input type="checkbox" id="checkbox" checked={isMarker1Needed} onChange={needMarker1CheckHandler}/>
                    <label htmlFor="checkbox">Do you need this Marker?</label>

                    {isMarker1Needed && <div>{/* Inputs to change latitude and longitude */}
                        <div className={classes.control}>
                            <label>
                                Latitude:
                                <input
                                    type="number"
                                    value={marker1Coordinates.lat}
                                    onChange={handleMarker1LatChange}
                                    placeholder="Latitude"
                                />
                            </label>
                        </div>
                        <div className={classes.control}>
                            <label>
                                Longitude:
                                <input
                                    type="number"
                                    value={marker1Coordinates.lng}
                                    onChange={handleMarker1LngChange}
                                    placeholder="Longitude"
                                />
                            </label>
                        </div>
                        <div className={classes.control}>
                            <label htmlFor='description'>Description</label>
                            <textarea id='description' required rows='1' ref={marker1DescriptionInputRef}></textarea>
                        </div>
                        <div>
                            <input type="checkbox" id="checkbox" checked={marker1RoundIsChecked} onChange={marker1RoundedCheckHandler}/>
                            <label htmlFor="checkbox">Rounding</label>
                        </div>

                        <div className={classes.control}>
                            <label htmlFor='title'>Gate</label>
                            <input type='text' required id='title' ref={marker1GateInputRef}/>
                        </div>

                        {/* Pass coordinates as props to the map component */}
                        <Gmap latitude={marker1Coordinates.lat} longitude={marker1Coordinates.lng}/></div>}

                </div>

                <div className={classes.mapbox}>
                    <h4>Marker 2</h4>
                    <input type="checkbox" id="checkbox" checked={isMarker2Needed} onChange={needMarker2CheckHandler}/>
                    <label htmlFor="checkbox">Do you need this Marker?</label>

                    {isMarker2Needed && <div>{/* Inputs to change latitude and longitude */}
                        <div className={classes.control}>
                            <label>
                                Latitude:
                                <input
                                    type="number"
                                    value={marker2Coordinates.lat}
                                    onChange={handleMarker2LatChange}
                                    placeholder="Latitude"
                                />
                            </label>
                        </div>
                        <div className={classes.control}>
                            <label>
                                Longitude:
                                <input
                                    type="number"
                                    value={marker2Coordinates.lng}
                                    onChange={handleMarker2LngChange}
                                    placeholder="Longitude"
                                />
                            </label>
                        </div>
                        <div className={classes.control}>
                            <label htmlFor='description'>Description</label>
                            <textarea id='description' required rows='1' ref={marker2DescriptionInputRef}></textarea>
                        </div>

                        <div>
                            <input type="checkbox" id="checkbox" checked={marker2RoundIsChecked}
                                   onChange={marker2RoundedCheckHandler}/>
                            <label htmlFor="checkbox">Rounding</label>
                        </div>

                        <div className={classes.control}>
                            <label htmlFor='title'>Gate</label>
                            <input type='text' required id='title' ref={marker2GateInputRef}/>
                        </div>

                        {/* Pass coordinates as props to the map component */}
                        <Gmap latitude={marker2Coordinates.lat} longitude={marker2Coordinates.lng}/></div>}

                </div>

                <div className={classes.mapbox}>
                    <h4>Marker 3</h4>
                    <input type="checkbox" id="checkbox" checked={isMarker3Needed} onChange={needMarker3CheckHandler}/>
                    <label htmlFor="checkbox">Do you need this Marker?</label>

                    {isMarker3Needed && <div>{/* Inputs to change latitude and longitude */}
                        <div className={classes.control}>
                            <label>
                                Latitude:
                                <input
                                    type="number"
                                    value={marker3Coordinates.lat}
                                    onChange={handleMarker3LatChange}
                                    placeholder="Latitude"
                                />
                            </label>
                        </div>
                        <div className={classes.control}>
                            <label>
                                Longitude:
                                <input
                                    type="number"
                                    value={marker3Coordinates.lng}
                                    onChange={handleMarker3LngChange}
                                    placeholder="Longitude"
                                />
                            </label>
                        </div>
                        <div className={classes.control}>
                            <label htmlFor='description'>Description</label>
                            <textarea id='description' required rows='1' ref={marker3DescriptionInputRef}></textarea>
                        </div>

                        <div>
                            <input type="checkbox" id="checkbox" checked={marker3RoundIsChecked}
                                   onChange={marker3RoundedCheckHandler}/>
                            <label htmlFor="checkbox">Rounding</label>
                        </div>

                        <div className={classes.control}>
                            <label htmlFor='title'>Gate</label>
                            <input type='text' required id='title' ref={marker3GateInputRef}/>
                        </div>

                        {/* Pass coordinates as props to the map component */}
                        <Gmap latitude={marker3Coordinates.lat} longitude={marker3Coordinates.lng}/></div>}

                </div>

                <div className={classes.mapbox}>
                    <h4>Marker 4</h4>
                    <input type="checkbox" id="checkbox" checked={isMarker4Needed} onChange={needMarker4CheckHandler}/>
                    <label htmlFor="checkbox">Do you need this Marker?</label>

                    {isMarker4Needed && <div>{/* Inputs to change latitude and longitude */}
                        <div className={classes.control}>
                            <label>
                                Latitude:
                                <input
                                    type="number"
                                    value={marker4Coordinates.lat}
                                    onChange={handleMarker4LatChange}
                                    placeholder="Latitude"
                                />
                            </label>
                        </div>
                        <div className={classes.control}>
                            <label>
                                Longitude:
                                <input
                                    type="number"
                                    value={marker4Coordinates.lng}
                                    onChange={handleMarker4LngChange}
                                    placeholder="Longitude"
                                />
                            </label>
                        </div>
                        <div className={classes.control}>
                            <label htmlFor='description'>Description</label>
                            <textarea id='description' required rows='1' ref={marker4DescriptionInputRef}></textarea>
                        </div>

                        <div>
                            <input type="checkbox" id="checkbox" checked={marker4RoundIsChecked}
                                   onChange={marker4RoundedCheckHandler}/>
                            <label htmlFor="checkbox">Rounding</label>
                        </div>

                        <div className={classes.control}>
                            <label htmlFor='title'>Gate</label>
                            <input type='text' required id='title' ref={marker4GateInputRef}/>
                        </div>

                        {/* Pass coordinates as props to the map component */}
                        <Gmap latitude={marker4Coordinates.lat} longitude={marker4Coordinates.lng}/></div>}

                </div>

                <div className={classes.mapbox}>
                    <h4>Marker 5</h4>
                    <input type="checkbox" id="checkbox" checked={isMarker5Needed} onChange={needMarker5CheckHandler}/>
                    <label htmlFor="checkbox">Do you need this Marker?</label>

                    {isMarker5Needed && <div>{/* Inputs to change latitude and longitude */}
                        <div className={classes.control}>
                            <label>
                                Latitude:
                                <input
                                    type="number"
                                    value={marker5Coordinates.lat}
                                    onChange={handleMarker5LatChange}
                                    placeholder="Latitude"
                                />
                            </label>
                        </div>
                        <div className={classes.control}>
                            <label>
                                Longitude:
                                <input
                                    type="number"
                                    value={marker5Coordinates.lng}
                                    onChange={handleMarker5LngChange}
                                    placeholder="Longitude"
                                />
                            </label>
                        </div>
                        <div className={classes.control}>
                            <label htmlFor='description'>Description</label>
                            <textarea id='description' required rows='1' ref={marker5DescriptionInputRef}></textarea>
                        </div>

                        <div>
                            <input type="checkbox" id="checkbox" checked={marker5RoundIsChecked}
                                   onChange={marker5RoundedCheckHandler}/>
                            <label htmlFor="checkbox">Rounding</label>
                        </div>

                        <div className={classes.control}>
                            <label htmlFor='title'>Gate</label>
                            <input type='text' required id='title' ref={marker5GateInputRef}/>
                        </div>

                        {/* Pass coordinates as props to the map component */}
                        <Gmap latitude={marker5Coordinates.lat} longitude={marker5Coordinates.lng}/></div>}

                </div>

                <div className={classes.mapbox}>
                    <h4>Marker 6</h4>
                    <input type="checkbox" id="checkbox" checked={isMarker6Needed} onChange={needMarker6CheckHandler}/>
                    <label htmlFor="checkbox">Do you need this Marker?</label>

                    {isMarker6Needed && <div>{/* Inputs to change latitude and longitude */}
                        <div className={classes.control}>
                            <label>
                                Latitude:
                                <input
                                    type="number"
                                    value={marker6Coordinates.lat}
                                    onChange={handleMarker6LatChange}
                                    placeholder="Latitude"
                                />
                            </label>
                        </div>
                        <div className={classes.control}>
                            <label>
                                Longitude:
                                <input
                                    type="number"
                                    value={marker6Coordinates.lng}
                                    onChange={handleMarker6LngChange}
                                    placeholder="Longitude"
                                />
                            </label>
                        </div>
                        <div className={classes.control}>
                            <label htmlFor='description'>Description</label>
                            <textarea id='description' required rows='1' ref={marker6DescriptionInputRef}></textarea>
                        </div>

                        <div>
                            <input type="checkbox" id="checkbox" checked={marker6RoundIsChecked}
                                   onChange={marker6RoundedCheckHandler}/>
                            <label htmlFor="checkbox">Rounding</label>
                        </div>

                        <div className={classes.control}>
                            <label htmlFor='title'>Gate</label>
                            <input type='text' required id='title' ref={marker6GateInputRef}/>
                        </div>

                        {/* Pass coordinates as props to the map component */}
                        <Gmap latitude={marker6Coordinates.lat} longitude={marker6Coordinates.lng}/></div>}

                </div>

                <div className={classes.mapbox}>
                    <h4>Marker 7</h4>
                    <input type="checkbox" id="checkbox" checked={isMarker7Needed} onChange={needMarker7CheckHandler}/>
                    <label htmlFor="checkbox">Do you need this Marker?</label>

                    {isMarker7Needed && <div>{/* Inputs to change latitude and longitude */}
                        <div className={classes.control}>
                            <label>
                                Latitude:
                                <input
                                    type="number"
                                    value={marker7Coordinates.lat}
                                    onChange={handleMarker7LatChange}
                                    placeholder="Latitude"
                                />
                            </label>
                        </div>
                        <div className={classes.control}>
                            <label>
                                Longitude:
                                <input
                                    type="number"
                                    value={marker7Coordinates.lng}
                                    onChange={handleMarker7LngChange}
                                    placeholder="Longitude"
                                />
                            </label>
                        </div>
                        <div className={classes.control}>
                            <label htmlFor='description'>Description</label>
                            <textarea id='description' required rows='1' ref={marker7DescriptionInputRef}></textarea>
                        </div>

                        <div>
                            <input type="checkbox" id="checkbox" checked={marker7RoundIsChecked}
                                   onChange={marker7RoundedCheckHandler}/>
                            <label htmlFor="checkbox">Rounding</label>
                        </div>

                        <div className={classes.control}>
                            <label htmlFor='title'>Gate</label>
                            <input type='text' required id='title' ref={marker7GateInputRef}/>
                        </div>

                        {/* Pass coordinates as props to the map component */}
                        <Gmap latitude={marker7Coordinates.lat} longitude={marker7Coordinates.lng}/></div>}

                </div>

                <div className={classes.mapbox}>
                    <h4>Marker 8</h4>
                    <input type="checkbox" id="checkbox" checked={isMarker8Needed} onChange={needMarker8CheckHandler}/>
                    <label htmlFor="checkbox">Do you need this Marker?</label>

                    {isMarker8Needed && <div>{/* Inputs to change latitude and longitude */}
                        <div className={classes.control}>
                            <label>
                                Latitude:
                                <input
                                    type="number"
                                    value={marker8Coordinates.lat}
                                    onChange={handleMarker8LatChange}
                                    placeholder="Latitude"
                                />
                            </label>
                        </div>
                        <div className={classes.control}>
                            <label>
                                Longitude:
                                <input
                                    type="number"
                                    value={marker8Coordinates.lng}
                                    onChange={handleMarker8LngChange}
                                    placeholder="Longitude"
                                />
                            </label>
                        </div>
                        <div className={classes.control}>
                            <label htmlFor='description'>Description</label>
                            <textarea id='description' required rows='1' ref={marker8DescriptionInputRef}></textarea>
                        </div>

                        <div>
                            <input type="checkbox" id="checkbox" checked={marker8RoundIsChecked}
                                   onChange={marker8RoundedCheckHandler}/>
                            <label htmlFor="checkbox">Rounding</label>
                        </div>

                        <div className={classes.control}>
                            <label htmlFor='title'>Gate</label>
                            <input type='text' required id='title' ref={marker8GateInputRef}/>
                        </div>

                        {/* Pass coordinates as props to the map component */}
                        <Gmap latitude={marker8Coordinates.lat} longitude={marker8Coordinates.lng}/></div>}

                </div>

                <div className={classes.control}>
                    <label htmlFor='notes'>Notes</label>
                    <textarea id='notes' required rows='5'></textarea>
                </div>
                <div className={classes.actions}>
                    <button>Create</button>
                </div>
            </form>
        </Card>
    );
}