import * as React from 'react';
import { Routes, Route } from 'react-router-dom';

import Home from './pages/Home';
import CreateRace from './pages/CreateRace.jsx';
import ViewCourse from './pages/ViewCourse.jsx';

export default function App() {
    return (
        <div >
            <Routes>

                <Route path="/" element={<Home/>}/>
                <Route path="create" element={<CreateRace/>}/>
                <Route path="view-course/:id" element={<ViewCourse/>}/>
            </Routes>
        </div>
    );
}
