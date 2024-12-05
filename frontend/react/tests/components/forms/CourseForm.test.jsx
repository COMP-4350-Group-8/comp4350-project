import { it, expect, describe, vi } from "vitest";
import { fireEvent, render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { MemoryRouter } from "react-router-dom";
import React from "react";
import CourseForm from "../../../src/components/forms/CourseForm";

describe("CourseForm", () => {
    it("should render the main form inputs", () => {
        // Render the component in a router so navigation between pages works
        render(
            <MemoryRouter>
                <CourseForm/>
            </MemoryRouter>
        );

        // Confirm that the core course form elements are being rendered
        const courseTitle = screen.getByText(/course title/i);
        expect(courseTitle).toBeInTheDocument();

        const description = screen.getAllByText(/description/i);
        expect(description[0]).toBeInTheDocument();

        const buttons = screen.getAllByRole("button");
        expect(buttons[0]).toBeInTheDocument();
        expect(buttons[0]).toHaveTextContent(/add marker/i);
        expect(buttons[1]).toBeInTheDocument();
        expect(buttons[1]).toHaveTextContent(/create/i);
    });

    it("should render the initial 2 marker forms", () => {
        // Render the component in a router so navigation between pages works
        render(
            <MemoryRouter>
                <CourseForm/>
            </MemoryRouter>
        );

        // Confirm the titles for the 2 initial markers are being rendered
        const startMarker = screen.getByText(/start marker/i);
        expect(startMarker).toBeInTheDocument();

        const finishMarker = screen.getByText(/finish marker/i);
        expect(finishMarker).toBeInTheDocument();
    });

    it("should add a new marker when Add Marker is clicked", async () => {
        // Used to simulate user inputs
        const user = userEvent.setup();
        
        // Render the component in a router so navigation between pages works
        render(
            <MemoryRouter>
                <CourseForm/>
            </MemoryRouter>
        );

        // Confirm there are 3 elements with the text marker (the 2 forms and the add button)
        let markers = screen.getAllByText(/marker/i);
        expect(markers).toHaveLength(7);

        // Add a marker to the list
        const buttons = screen.getAllByRole("button");
        expect(buttons[0]).toBeInTheDocument();
        await user.click(buttons[0]);

        // Confirm there are now 4 elements with the text marker
        markers = screen.getAllByText(/marker/i);
        expect(markers).toHaveLength(10);
    });

    it("should not create a new course when not all form inputs have been filled in", async () => {
        // Used to simulate user inputs
        const user = userEvent.setup();

        // Mocked function for checking if the form can be submitted
        const addCourseClick = vi.fn();
        
        // Render the component in a router so navigation between pages works.
        // Pass the mocked function as the callback to see if it gets called
        render(
            <MemoryRouter>
                <CourseForm onAddCourse={addCourseClick}/>
            </MemoryRouter>
        );

        // Click the Create button without inputting any data
        const button = screen.getByRole("button", { name: /create/i });
        await user.click(button);

        // Make sure the form did not get submitted (no inputs -> not submission)
        expect(addCourseClick).toHaveBeenCalledTimes(0);
    });

    it("should create a new course when all form inputs have been filled in", async () => {
        // Used to simulate user inputs
        const user = userEvent.setup();

        // Mocked function for checking if the form can be submitted
        const addCourseClick = vi.fn();
        
        // Render the component in a router so navigation between pages works.
        // Pass the mocked function as the callback to see if it gets called
        render(
            <MemoryRouter>
                <CourseForm onAddCourse={addCourseClick}/>
            </MemoryRouter>
        );

        // Fill in each text input with "Hello"
        let inputs = screen.getAllByRole("textbox");
        expect(inputs).toHaveLength(6);
        inputs.forEach((input) => {
            user.click(input);
            user.keyboard("Hello");
        });

        // Make sure latitude and longitude inputs are tested
        const latLngInputs = screen.getAllByRole("spinbutton");
        expect(latLngInputs).toHaveLength(4);
        user.click(latLngInputs[0]);
        fireEvent.change(latLngInputs[0], {target: {value: "90"}});
        fireEvent.change(latLngInputs[1], {target: {value: "90"}});

        // Create a course with the inputted data
        const button = screen.getByRole("button", { name: /create/i });
        await user.click(button);

        // Make sure the form got submitted successfully by seeing if the callback was called
        expect(addCourseClick).toHaveBeenCalledOnce();
    });

    it("should create a new course when all form inputs have been filled in with special characters", async () => {
        // Used to simulate user inputs
        const user = userEvent.setup();

        // Mocked function for checking if the form can be submitted
        const addCourseClick = vi.fn();
        
        // Render the component in a router so navigation between pages works.
        // Pass the mocked function as the callback to see if it gets called
        render(
            <MemoryRouter>
                <CourseForm onAddCourse={addCourseClick}/>
            </MemoryRouter>
        );

        // Fill in each text input with special characters: "蟲誤,修正!"
        let inputs = screen.getAllByRole("textbox");
        expect(inputs).toHaveLength(6);
        inputs.forEach((input) => {
            user.click(input);
            user.keyboard("蟲誤,修正!");
        });

        // Create a course with the inputted data
        const button = screen.getByRole("button", { name: /create/i });
        await user.click(button);

        // Make sure the form got submitted
        expect(addCourseClick).toHaveBeenCalledOnce();
    });

    it("should create a new course when all form inputs have been filled in and there are extra markers", async () => {
        // Used to simulate user inputs
        const user = userEvent.setup();

        // Mocked function for checking if the form can be submitted
        const addCourseClick = vi.fn();
        
        // Render the component in a router so navigation between pages works.
        // Pass the mocked function as the callback to see if it gets called
        render(
            <MemoryRouter>
                <CourseForm onAddCourse={addCourseClick}/>
            </MemoryRouter>
        );

        // Add a marker to the list
        const buttons = screen.getAllByRole("button");
        expect(buttons[0]).toBeInTheDocument();
        await user.click(buttons[0]);
        await user.click(buttons[0]);

        // Fill in each text input with "Hello" up to the last marker's gate
        let inputs = screen.getAllByRole("textbox");
        expect(inputs).toHaveLength(10);
        inputs.forEach((input, index) => {
            user.click(input);
            // Different gates require different values, so this ensures the second gate (last 2 markers) have different values from the first gate
            if (index < 7) {
                user.keyboard("Hello");
            } else {
                user.keyboard("Howdy");
            }
        });

        // Create a course with the inputted data
        const button = screen.getByRole("button", { name: /create/i });
        await user.click(button);

        // Make sure the form got submitted
        expect(addCourseClick).toHaveBeenCalledOnce();
    });
});
