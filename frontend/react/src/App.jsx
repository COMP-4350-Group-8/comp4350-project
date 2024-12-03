import React, {useState} from "react";
import { Routes, Route } from 'react-router-dom';

import Home from './pages/Home';
import CreateRegatta from './pages/Create/CreateRegatta.jsx';
import CreateRace from './pages/Create/CreateRace.jsx';
import CreateCourse from './pages/Create/CreateCourse.jsx';
import ViewRegatta from './pages/View/ViewRegatta.jsx';
import ViewRace from './pages/View/ViewRace.jsx';
import ViewCourse from './pages/View/ViewCourse.jsx';
import SetServerURL from './pages/SetServerURL/SetServerURL.jsx';
import "./App.css";

export default function App() {
    const [serverUrl, setServerUrl] = useState("http://localhost:5000");

    return (
        <div className="app">
            <Routes>
                <Route path="/" element={<Home serverUrl={serverUrl}/>}/>
                <Route path="create/regatta" element={<CreateRegatta serverUrl={serverUrl}/>}/>
                <Route path="create/race" element={<CreateRace serverUrl={serverUrl}/>}/>
                <Route path="create/course" element={<CreateCourse serverUrl={serverUrl}/>}/>
                <Route path="view/regatta/:id" element={<ViewRegatta serverUrl={serverUrl}/>}/>
                <Route path="view/race/:id" element={<ViewRace serverUrl={serverUrl}/>}/>
                <Route path="view/course/:id" element={<ViewCourse serverUrl={serverUrl}/>}/>
                <Route path="setServerUrl" element={<SetServerURL serverUrl={serverUrl} setServerUrl={setServerUrl}/>}/>
            </Routes>
        </div>
    );
}
