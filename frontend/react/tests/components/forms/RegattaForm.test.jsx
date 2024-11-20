import { it, expect, describe, vi } from "vitest";
import { fireEvent, render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { MemoryRouter } from "react-router-dom";
import React from "react";
import RegattaForm from "../../../src/components/forms/RegattaForm";

describe("RegattaForm", () => {
    it("should render the main form inputs", () => {
        // Render the component in a router so navigation between pages works
        render(
            <MemoryRouter>
                <RegattaForm/>
            </MemoryRouter>
        );

        // Confirm that the core regatta form elements are being rendered
        const regattaTitle = screen.getByText(/regatta title/i);
        expect(regattaTitle).toBeInTheDocument();

        const button = screen.getByRole("button");
        expect(button).toBeInTheDocument();
        expect(button).toHaveTextContent(/add race/i);
    });

    it("should not create a new regatta when not all the inputs have been filled in", async () => {
        // Used to simulate user inputs
        const user = userEvent.setup();

        // Mocked function for checking if the form can be submitted
        const addRegattaClick = vi.fn();
        
        // Render the component in a router so navigation between pages works
        render(
            <MemoryRouter>
                <RegattaForm onAddRegatta={addRegattaClick}/>
            </MemoryRouter>
        );

        // Test logic

        // Make sure the form did not get submitted (no inputs -> not submission)
        expect(addRegattaClick).toHaveBeenCalledTimes(0);
    });

    it("create a new regatta when all the inputs are filled in but the dropdown has not been touched", async () => {
        // Used to simulate user inputs
        const user = userEvent.setup();

        // Mocked function for checking if the form can be submitted
        const addRegattaClick = vi.fn();
        
        // Render the component in a router so navigation between pages works
        render(
            <MemoryRouter>
                <RegattaForm onAddRegatta={addRegattaClick}/>
            </MemoryRouter>
        );

        // Test logic

        // Make sure the form did not get submitted (no inputs -> not submission)
        expect(addRegattaClick).toHaveBeenCalledTimes(0);
    });

    it("create a new regatta when all the inputs are filled and the dropdown has been changed", async () => {
        // Used to simulate user inputs
        const user = userEvent.setup();

        // Mocked function for checking if the form can be submitted
        const addRegattaClick = vi.fn();
        
        // Render the component in a router so navigation between pages works
        render(
            <MemoryRouter>
                <RegattaForm onAddRegatta={addRegattaClick}/>
            </MemoryRouter>
        );

        // Test logic

        // Make sure the form did not get submitted (no inputs -> not submission)
        expect(addRegattaClick).toHaveBeenCalledTimes(0);
    });
});