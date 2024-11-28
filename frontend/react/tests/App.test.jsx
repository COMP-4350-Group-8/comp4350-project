import { it, expect, describe, vi } from "vitest";
import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import React from 'react';
import { MemoryRouter } from 'react-router-dom';
import App from "../src/App";

// Mock for the addCourseHandler method from AddCourse that is used in CreateCourse
vi.mock("../src/utils/AddCourse",  () => ({
    default: vi.fn()
}));

// Mock for the addRaceHandler method from AddRace that is used in CreateRace
vi.mock("../src/utils/AddRace",  () => ({
    default: vi.fn()
}));

// Mock for the addRegattaHandler method from AddRegatta that is used in CreateRegatta
vi.mock("../src/utils/AddRegatta",  () => ({
    default: vi.fn()
}));

// Mock for the getCourses method from GetCourses that is used in Home and RaceForm
vi.mock("../src/utils/GetCourses",  () => ({
    default: vi.fn((setCourses) => {
        setCourses([{id: 1, name: "Course1"}, {id: 2, name: "Course2"}]);
    })
}));

// Mock for the getRaces method from GetRaces that is used in Home and RegattaForm
vi.mock("../src/utils/GetRaces",  () => ({
    default: vi.fn((setRaces) => {
        setRaces([{id: 1, name: "Race1"}, {id: 2, name: "Race2"}]);
    })
}));

// Mock for the getRegattas method from GetRegattas that is used in Home
vi.mock("../src/utils/GetRegattas",  () => ({
    default: vi.fn((setRegattas) => {
        setRegattas([{id: 1, name: "Regatta1"}, {id: 2, name: "Regatta2"}]);
    })
}));

describe("App", () => {
    it("should navigate to the Create Course page when the Create Course button is clicked", async () => {
        // Used to simulate user inputs
        const user = userEvent.setup();

        // Render the component in a router so navigation between pages works
        render(<MemoryRouter>
            (<App/>);
        </MemoryRouter>)

        // Check that we're on the Home page initially
        expect(screen.getByText(/sail mapper/i)).toBeInTheDocument();

        // Find the Start Course button and click it (and wait for the click to finish)
        const button = screen.getByRole("button", { name: /create course/i });
        expect(button).toBeInTheDocument();
        await user.click(button);

        // Assert that the page changed to the Create Course page
        expect(screen.getByText(/course title/i)).toBeInTheDocument();
    });

    it("should return to the Home page after creating a course", async () => {
        // Used to simulate user inputs
        const user = userEvent.setup();

        // Render the component in a router so navigation between pages works
        render(<MemoryRouter>
            (<App/>);
        </MemoryRouter>)

        // Check that we're on the Home page initially
        expect(screen.getByText(/sail mapper/i)).toBeInTheDocument();

        // Find the Create Course button and click it (and wait for the click to finish)
        const button = screen.getByRole("button", { name: /create course/i });
        expect(button).toBeInTheDocument();
        await user.click(button);

        // Fill in each text input with "Hello"
        let inputs = screen.getAllByRole("textbox");
        expect(inputs).toHaveLength(7);
        inputs.forEach((input) => {
            user.click(input);
            user.keyboard("Hello");
        });

        // Create a course with the inputted data
        const createButton = screen.getByRole("button", { name: /create/i });
        await user.click(createButton);

        // Assert that the active page is now the Home page
        expect(screen.getByText(/sail mapper/i)).toBeInTheDocument();
    });

    it("should not return to the Home page if there are any unfilled inputs", async () => {
        // Used to simulate user inputs
        const user = userEvent.setup();

        // Render the component in a router so navigation between pages works
        render(<MemoryRouter>
            (<App/>);
        </MemoryRouter>)

        // Check that we're on the Home page initially
        expect(screen.getByText(/sail mapper/i)).toBeInTheDocument();

        // Find the Start Course button and click it (and wait for the click to finish)
        const startButton = screen.getByRole("button", { name: /create course/i });
        expect(startButton).toBeInTheDocument();
        await user.click(startButton);

        // Assert that the page changed to the Create Course page
        expect(screen.getByText(/course title/i)).toBeInTheDocument();

        // Create a course without inputting data
        const createButton = screen.getByRole("button", { name: /create/i });
        await user.click(createButton);

        // Assert that the active page is still the Create Course page
        expect(screen.getByText(/course title/i)).toBeInTheDocument();
    });

    it("should return to the Home page after creating a race", async () => {
        // Used to simulate user inputs
        const user = userEvent.setup();

        // Render the component in a router so navigation between pages works
        render(<MemoryRouter>
            (<App/>);
        </MemoryRouter>)

        // Check that we're on the Home page initially
        expect(screen.getByText(/sail mapper/i)).toBeInTheDocument();

        // Find the Create Course button and click it (and wait for the click to finish)
        const button = screen.getByRole("button", { name: /create race/i });
        expect(button).toBeInTheDocument();
        await user.click(button);

        // Fill in the race title
        const input = screen.getByRole("textbox");
        expect(input).toBeInTheDocument();
        user.click(input);
        user.keyboard("Hello");

        // Create a course with the inputted data
        const createButton = screen.getByRole("button", { name: /create/i });
        await user.click(createButton);

        // Assert that the active page is now the Home page
        expect(screen.getByText(/sail mapper/i)).toBeInTheDocument();
    });

    it("should return to the Home page after creating a regatta", async () => {
        // Used to simulate user inputs
        const user = userEvent.setup();

        // Render the component in a router so navigation between pages works
        render(<MemoryRouter>
            (<App/>);
        </MemoryRouter>)

        // Check that we're on the Home page initially
        expect(screen.getByText(/sail mapper/i)).toBeInTheDocument();

        // Find the Create Course button and click it (and wait for the click to finish)
        const button = screen.getByRole("button", { name: /create regatta/i });
        expect(button).toBeInTheDocument();
        await user.click(button);

        // Fill in the race title
        const input = screen.getByRole("textbox");
        expect(input).toBeInTheDocument();
        user.click(input);
        user.keyboard("Hello");

        // Add a race to the regatta
        const addRaceButton = screen.getByRole("button", { name: /add race/i });
        await user.click(addRaceButton);

        // Create a course with the inputted data
        const createButton = screen.getByRole("button", { name: /create/i });
        await user.click(createButton);

        // Assert that the active page is now the Home page
        expect(screen.getByText(/sail mapper/i)).toBeInTheDocument();
    });
});