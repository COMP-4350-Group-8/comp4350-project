import { it, expect, describe, vi } from "vitest";
import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import React from 'react';
import { MemoryRouter } from 'react-router-dom';
import App from "../src/App";

// Mock for the addCourseHandler method from AddCourse that is used in CreateCourse
vi.mock("../src/utils/AddCourse",  () => ({
    default: vi.fn()
}))

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
});