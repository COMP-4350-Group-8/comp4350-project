import * as React from 'react';
import { Routes, Route } from 'react-router-dom';

import Home from './pages/Home';
import CreateRegatta from './pages/CreateRegatta.jsx';
import CreateRace from './pages/CreateRace.jsx';
import CreateCourse from './pages/CreateCourse.jsx';

export default function App() {
    return (
        <div >
            <Routes>

                <Route path="/" element={<Home/>}/>
                <Route path="create/regatta" element={<CreateRegatta/>}/>
                <Route path="create/race" element={<CreateRace/>}/>
                <Route path="create/course" element={<CreateCourse/>}/>
            </Routes>
        </div>
    );
}
