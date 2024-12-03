import { it, expect, describe, vi, afterEach } from "vitest";
import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { MemoryRouter } from "react-router-dom";
import React from "react";
import RaceForm from "../../../src/components/forms/RaceForm";
import getCourses from "../../../src/utils/GetCourses";

// Mock for the getCourses method from GetCourses that is used in RaceForm
vi.mock("../../../src/utils/GetCourses",  () => ({
    default: vi.fn((serverUrl, setCourses) => {
        setCourses([{id: 1, name: "Course1"}, {id: 2, name: "Course2"}]);
    })
}));

describe("RaceForm", () => {
    it("should render the main form inputs", () => {
        // Render the component in a router so navigation between pages works
        render(
            <MemoryRouter>
                <RaceForm/>
            </MemoryRouter>
        );

        // Confirm that the core race form elements are being rendered
        const raceTitle = screen.getByText(/race title/i);
        expect(raceTitle).toBeInTheDocument();

        const raceDropdown = screen.getByRole("combobox");
        expect(raceDropdown).toBeInTheDocument();

        const button = screen.getByRole("button");
        expect(button).toBeInTheDocument();
        expect(button).toHaveTextContent(/create/i);
    });

    it("should not create a new race when not all the inputs have been filled in", async () => {
        // Used to simulate user inputs
        const user = userEvent.setup();

        // Mocked function for checking if the form can be submitted
        const addRaceClick = vi.fn();
        
        // Render the component in a router so navigation between pages works
        render(
            <MemoryRouter>
                <RaceForm onAddRace={addRaceClick}/>
            </MemoryRouter>
        );

        // Click the Create button without filling in the title field
        const button = screen.getByRole("button");
        expect(button).toBeInTheDocument();
        expect(button).toHaveTextContent(/create/i);
        await user.click(button);

        // Make sure the form did not get submitted (no inputs -> not submission)
        expect(addRaceClick).toHaveBeenCalledTimes(0);
    });

    it("create a new race when all the inputs are filled in but the dropdown has not been touched", async () => {
        // Used to simulate user inputs
        const user = userEvent.setup();

        // Mocked function for checking if the form can be submitted
        const addRaceClick = vi.fn();
        
        // Render the component in a router so navigation between pages works
        render(
            <MemoryRouter>
                <RaceForm onAddRace={addRaceClick}/>
            </MemoryRouter>
        );

        // Fill in the race title
        const input = screen.getByRole("textbox");
        expect(input).toBeInTheDocument();
        user.click(input);
        user.keyboard("Hello");

        // Click the Create button
        const button = screen.getByRole("button");
        expect(button).toBeInTheDocument();
        expect(button).toHaveTextContent(/create/i);
        await user.click(button);

        // Make sure the form got submitted
        expect(addRaceClick).toHaveBeenCalledTimes(1);
    });

    it("create a new race when all the inputs are filled and the dropdown has been changed", async () => {
        // Used to simulate user inputs
        const user = userEvent.setup();

        // Mocked function for checking if the form can be submitted
        const addRaceClick = vi.fn();
        
        // Render the component in a router so navigation between pages works
        render(
            <MemoryRouter>
                <RaceForm onAddRace={addRaceClick}/>
            </MemoryRouter>
        );

        // Fill in the race title
        const input = screen.getByRole("textbox");
        expect(input).toBeInTheDocument();
        user.click(input);
        user.keyboard("Hello");

        // Select the second race from the dropdown
        const dropdown = screen.getByRole("combobox");
        expect(dropdown).toBeInTheDocument();
        user.click(dropdown);
        const options = screen.getAllByRole("option");
        expect(options).toHaveLength(2);
        user.click(options[1]);

        // Click the Create button
        const button = screen.getByRole("button");
        expect(button).toBeInTheDocument();
        expect(button).toHaveTextContent(/create/i);
        await user.click(button);

        // Make sure the form got submitted
        expect(addRaceClick).toHaveBeenCalledTimes(1);
    });
});