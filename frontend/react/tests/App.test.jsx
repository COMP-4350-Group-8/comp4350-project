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
    default: vi.fn((serverUrl, setCourses) => {
        setCourses([{id: 1, name: "Course1"}, {id: 2, name: "Course2"}]);
    })
}));

// Mock for the getRaces method from GetRaces that is used in Home and RegattaForm
vi.mock("../src/utils/GetRaces",  () => ({
    default: vi.fn((serverUrl, setRaces) => {
        setRaces([{id: 1, name: "Race1"}, {id: 2, name: "Race2"}]);
    })
}));

// Mock for the getRegattas method from GetRegattas that is used in Home
vi.mock("../src/utils/GetRegattas",  () => ({
    default: vi.fn((serverUrl, setRegattas) => {
        setRegattas([{id: 1, name: "Regatta1"}, {id: 2, name: "Regatta2"}]);
    })
}));

// Mock for the getRegatta method from GetRegatta that is used in ViewRegatta
vi.mock("../src/utils/GetRegatta",  () => ({
    default: vi.fn((serverUrl, id, setRegattaData) => {
        setRegattaData({id: 1, name: "Regatta1", races: [{id: 1, name: "Race1"}]});
    })
}));

// Mock for the getRace method from GetRace that is used in ViewRace
vi.mock("../src/utils/GetRace",  () => ({
    default: vi.fn((serverUrl, id, setRaceData) => {
        setRaceData({id: 1, name: "Race1", courses: [{id: 1, name: "Course1"}]});
    })
}));

// Mock for the getCourse method from GetCourse that is used in ViewCourse
vi.mock("../src/utils/GetCourse",  () => ({
    default: vi.fn((serverUrl, id, setCourseData, setIsLoading) => {
        setIsLoading(false);
        setCourseData({id: 1, name: "Course1", courseMarks: [{id: 1, description: "Marker description", latitude: "50", longitude: "50.1", rounding: true}]});
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

        // Find the Create Course button and click it (and wait for the click to finish)
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

        // Find the Create Race button and click it (and wait for the click to finish)
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

        // Find the Create Regatta button and click it (and wait for the click to finish)
        const button = screen.getByRole("button", { name: /create regatta/i });
        expect(button).toBeInTheDocument();
        await user.click(button);

        // Fill in the regatta title
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

    it("should view a regatta after clicking on one", async () => {
        // Used to simulate user inputs
        const user = userEvent.setup();

        // Render the component in a router so navigation between pages works
        render(<MemoryRouter>
            (<App/>);
        </MemoryRouter>)

        // Check that we're on the Home page initially
        expect(screen.getByText(/sail mapper/i)).toBeInTheDocument();

        // Find the button for the regatta named Regatta1 and click it (and wait for the click to finish)
        const button = screen.getByRole("button", { name: /regatta1/i });
        expect(button).toBeInTheDocument();
        await user.click(button);

        // Assert that the active page is now the view page for Regatta1
        expect(screen.getByText(/regatta1/i)).toBeInTheDocument();
    });

    it("should view a regatta after clicking on one, and view a race from the regatta after clicking on one", async () => {
        // Used to simulate user inputs
        const user = userEvent.setup();

        // Render the component in a router so navigation between pages works
        render(<MemoryRouter>
            (<App/>);
        </MemoryRouter>)

        // Check that we're on the Home page initially
        expect(screen.getByText(/sail mapper/i)).toBeInTheDocument();

        // Find the button for the regatta named Regatta1 and click it (and wait for the click to finish)
        const button = screen.getByRole("button", { name: /regatta1/i });
        expect(button).toBeInTheDocument();
        await user.click(button);

        // Assert that the active page is now the view page for Regatta1
        expect(screen.getByText(/regatta1/i)).toBeInTheDocument();

        // Find the button for the race named Race1 and click it (and wait for the click to finish)
        const raceButton = screen.getByRole("button", { name: /race1/i });
        expect(raceButton).toBeInTheDocument();
        await user.click(raceButton);

        // Assert that the active page is now the view page for Race1
        expect(screen.getByText(/race1/i)).toBeInTheDocument();
    });

    it("should view a race after clicking on one", async () => {
        // Used to simulate user inputs
        const user = userEvent.setup();

        // Render the component in a router so navigation between pages works
        render(<MemoryRouter>
            (<App/>);
        </MemoryRouter>)

        // Check that we're on the Home page initially
        expect(screen.getByText(/sail mapper/i)).toBeInTheDocument();

        // Find the button for the race named Race1 and click it (and wait for the click to finish)
        const button = screen.getByRole("button", { name: /race1/i });
        expect(button).toBeInTheDocument();
        await user.click(button);

        // Assert that the active page is now the view page for Race1
        expect(screen.getByText(/race1/i)).toBeInTheDocument();
    });

    it("should view a race after clicking on one, and view a course after clicking on one", async () => {
        // Used to simulate user inputs
        const user = userEvent.setup();

        // Render the component in a router so navigation between pages works
        render(<MemoryRouter>
            (<App/>);
        </MemoryRouter>)

        // Check that we're on the Home page initially
        expect(screen.getByText(/sail mapper/i)).toBeInTheDocument();

        // Find the button for the race named Race1 and click it (and wait for the click to finish)
        const button = screen.getByRole("button", { name: /race1/i });
        expect(button).toBeInTheDocument();
        await user.click(button);

        // Assert that the active page is now the view page for Race1
        expect(screen.getByText(/race1/i)).toBeInTheDocument();

        // Find the button for the race named Course1 and click it (and wait for the click to finish)
        const courseButton = screen.getByRole("button", { name: /course1/i });
        expect(courseButton).toBeInTheDocument();
        await user.click(courseButton);

        // Assert that the active page is now the view page for Course1
        expect(screen.getByText(/course1/i)).toBeInTheDocument();
    });

    it("should view a course after clicking on one", async () => {
        // Used to simulate user inputs
        const user = userEvent.setup();

        // Render the component in a router so navigation between pages works
        render(<MemoryRouter>
            (<App/>);
        </MemoryRouter>)

        // Check that we're on the Home page initially
        expect(screen.getByText(/sail mapper/i)).toBeInTheDocument();

        // Find the button for the course named Course1 and click it (and wait for the click to finish)
        const button = screen.getByRole("button", { name: /course1/i });
        expect(button).toBeInTheDocument();
        await user.click(button);

        // Assert that the active page is now the view page for Course1
        expect(screen.getByText(/course1/i)).toBeInTheDocument();
    });

    it("should be able to set the server url by clicking the Set Server URL button", async () => {
        // Used to simulate user inputs
        const user = userEvent.setup();

        // Render the component in a router so navigation between pages works
        render(<MemoryRouter>
            (<App/>);
        </MemoryRouter>)

        // Check that we're on the Home page initially
        expect(screen.getByText(/sail mapper/i)).toBeInTheDocument();

        // Find the Set Server URL button and click it (and wait for the click to finish)
        const button = screen.getByRole("button", { name: /set server url/i });
        expect(button).toBeInTheDocument();
        await user.click(button);

        // Assert that the active page is now the set server url page
        expect(screen.getByText(/set custom server url/i)).toBeInTheDocument();
    });
});