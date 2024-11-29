import * as React from 'react';
import { Routes, Route } from 'react-router-dom';

import Home from './pages/Home';
import CreateRegatta from './pages/CreateRegatta.jsx';
import CreateRace from './pages/CreateRace.jsx';
import CreateCourse from './pages/CreateCourse.jsx';
import ViewRegatta from './pages/ViewRegatta.jsx';
import ViewRace from './pages/ViewRace.jsx';
import ViewCourse from './pages/ViewCourse.jsx';
import "./App.css";

export default function App() {
    return (
        <div className="app">
            <Routes>
                <Route path="/" element={<Home/>}/>
                <Route path="create/regatta" element={<CreateRegatta/>}/>
                <Route path="create/race" element={<CreateRace/>}/>
                <Route path="create/course" element={<CreateCourse/>}/>
                <Route path="view/regatta/:id" element={<ViewRegatta/>}/>
                <Route path="view/race/:id" element={<ViewRace/>}/>
                <Route path="view/course/:id" element={<ViewCourse/>}/>
            </Routes>
        </div>
    );
}
