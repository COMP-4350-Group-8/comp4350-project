import * as React from 'react';
import { Routes, Route } from 'react-router-dom';

import Home from './pages/Home';
import CreateRace from './pages/CreateRace.jsx';

export default function App() {
    return (
        <div >
            <Routes>

                <Route path="/" element={<Home/>}/>
                <Route path="create" element={<CreateRace/>}/>
            </Routes>
        </div>
    );
}
