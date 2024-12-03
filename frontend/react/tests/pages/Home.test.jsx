import { it, expect, describe, vi } from "vitest";
import { render, screen } from "@testing-library/react";
import React from 'react';
import { MemoryRouter } from 'react-router-dom';
import Home from  '../../src/pages/Home';

// Mock for the getCourses method from GetCourses that is used in Home and RaceForm
vi.mock("../../src/utils/GetCourses",  () => ({
    default: vi.fn((serverUrl, setCourses) => {
        setCourses([{id: 1, name: "Course1"}, {id: 2, name: "Course2"}]);
    })
}));

// Mock for the getRaces method from GetRaces that is used in Home and RegattaForm
vi.mock("../../src/utils/GetRaces",  () => ({
    default: vi.fn((serverUrl, setRaces) => {
        setRaces([{id: 1, name: "Race1"}, {id: 2, name: "Race2"}]);
    })
}));

// Mock for the getRegattas method from GetRegattas that is used in Home
vi.mock("../../src/utils/GetRegattas",  () => ({
    default: vi.fn((serverUrl, setRegattas) => {
        setRegattas([{id: 1, name: "Regatta1"}, {id: 2, name: "Regatta2"}]);
    })
}));

describe("Home", () => {
    it("should render the header", () => {
        // Render the component in a router so navigation between pages works
        render(<MemoryRouter>
            (<Home/>);
        </MemoryRouter>)

        const heading = screen.getAllByRole("heading");
        expect(heading[0]).toBeInTheDocument();
        expect(heading[0]).toHaveTextContent(/sail mapper/i);
    });

    it("should render the Start Race button", () => {
        // Render the component in a router so navigation between pages works
        render(<MemoryRouter>
            (<Home/>);
        </MemoryRouter>)

        const button = screen.getByRole("button", {name: /create course/i});
        expect(button).toBeInTheDocument();
        expect(button).toHaveTextContent(/create course/i);
    });
});
