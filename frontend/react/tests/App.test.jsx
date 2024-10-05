import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import React from 'react';
import { MemoryRouter } from 'react-router-dom';
import App from "../src/App";

describe("App", () => {
    it("should navigate to the Create Race page when the Start Race button is clicked", async () => {
        const user = userEvent.setup();

        render(<MemoryRouter>
            (<App/>);
        </MemoryRouter>)

        // Check that we're on the Home page initially
        expect(screen.getByText(/sail mapper/i)).toBeInTheDocument();

        // Find the Start Race button and click it (and wait for the click to finish)
        const button = screen.getByRole("button", { name: /start race/i });
        expect(button).toBeInTheDocument();
        await user.click(button);

        // Assert that the page changed to the Create Race page
        expect(screen.getByText(/create race/i)).toBeInTheDocument();
    });
});