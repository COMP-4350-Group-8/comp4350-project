import { it, expect, describe, vi } from "vitest";
import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { MemoryRouter } from "react-router-dom";
import React from "react";
import RegattaForm from "../../../src/components/forms/RegattaForm";

// Mock for the getRaces method from GetRaces that is used in RegattaForm
vi.mock("../../../src/utils/GetRaces",  () => ({
    default: vi.fn(() => {
        return [{id: 1, name: "Race1"}, {id: 2, name: "Race2"}];
    })
}));

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

        // Get the buttons (should just be the Add Race button)
        let buttons = screen.getAllByRole("button");
        expect(buttons).toHaveLength(1);

        // Add a race to the regatta
        expect(buttons[0]).toHaveTextContent(/add race/i);
        await user.click(buttons[0]);

        // Confirm the Create button now exists
        buttons = screen.getAllByRole("button");
        expect(buttons).toHaveLength(2);

        // Click the Create button without filling in the title field
        expect(buttons[1]).toHaveTextContent(/create/i);
        await user.click(buttons[1]);

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

        // Get the buttons (should just be the Add Race button)
        let buttons = screen.getAllByRole("button");
        expect(buttons).toHaveLength(1);

        // Add a race to the regatta
        expect(buttons[0]).toHaveTextContent(/add race/i);
        await user.click(buttons[0]);

        // Fill in the race title
        const input = screen.getByRole("textbox");
        expect(input).toBeInTheDocument();
        user.click(input);
        user.keyboard("Hello");

        // Confirm the Create button now exists
        buttons = screen.getAllByRole("button");
        expect(buttons).toHaveLength(2);

        // Click the Create button without filling in the title field
        expect(buttons[1]).toHaveTextContent(/create/i);
        await user.click(buttons[1]);

        // Make sure the form got submitted
        expect(addRegattaClick).toHaveBeenCalledTimes(1);
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

        // Get the buttons (should just be the Add Race button)
        let buttons = screen.getAllByRole("button");
        expect(buttons).toHaveLength(1);

        // Add a race to the regatta
        expect(buttons[0]).toHaveTextContent(/add race/i);
        await user.click(buttons[0]);

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

        // Confirm the Create button now exists
        buttons = screen.getAllByRole("button");
        expect(buttons).toHaveLength(2);

        // Click the Create button without filling in the title field
        expect(buttons[1]).toHaveTextContent(/create/i);
        await user.click(buttons[1]);

        // Make sure the form got submitted
        expect(addRegattaClick).toHaveBeenCalledTimes(1);
    });

    it("create a new regatta after adding a second race", async () => {
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

        // Fill in the race title
        const input = screen.getByRole("textbox");
        expect(input).toBeInTheDocument();
        user.click(input);
        user.keyboard("Hello");

        // Get the buttons (should just be the Add Race button)
        let buttons = screen.getAllByRole("button");
        expect(buttons).toHaveLength(1);

        // Add a race to the regatta
        expect(buttons[0]).toHaveTextContent(/add race/i);
        await user.click(buttons[0]);

        // Select the second race from the dropdown
        let dropdown = screen.getByRole("combobox");
        expect(dropdown).toBeInTheDocument();
        user.click(dropdown);
        let options = screen.getAllByRole("option");
        expect(options).toHaveLength(2);
        user.click(options[1]);

        // Add a second race to the regatta
        expect(buttons[0]).toHaveTextContent(/add race/i);
        await user.click(buttons[0]);

        // Select the second race from the second dropdown
        const dropdowns = screen.getAllByRole("combobox");
        expect(dropdowns).toHaveLength(2);
        user.click(dropdowns[1]);
        options = screen.getAllByRole("option");
        expect(options).toHaveLength(4);
        user.click(options[3]);

        // Confirm the Create button now exists
        buttons = screen.getAllByRole("button");
        expect(buttons).toHaveLength(2);

        // Click the Create button without filling in the title field
        expect(buttons[1]).toHaveTextContent(/create/i);
        await user.click(buttons[1]);

        // Make sure the form got submitted
        expect(addRegattaClick).toHaveBeenCalledTimes(1);
    });
});