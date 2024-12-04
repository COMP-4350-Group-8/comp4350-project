import { it, expect, describe, vi } from "vitest";
import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import React from 'react';
import { MemoryRouter } from 'react-router-dom';
import SetServerURL from  '../../src/pages/SetServerURL/SetServerURL';

describe("Home", () => {
    it("should render the Set Server URL page", () => {
        // Render the component in a router so navigation between pages works
        render(<MemoryRouter>
            (<SetServerURL/>);
        </MemoryRouter>)

        // Verify the header exists
        const heading = screen.getAllByRole("heading");
        expect(heading[0]).toBeInTheDocument();
        expect(heading[0]).toHaveTextContent(/set custom server url/i);

        // Verify the url input field exists
        let input = screen.getByRole("textbox");
        expect(input).toBeInTheDocument();

        // Verify the set button exists
        const button = screen.getByRole("button", {name: /set/i});
        expect(button).toBeInTheDocument();
        expect(button).toHaveTextContent(/set/i);
    });

    it("should call the mocked function when the url gets set but is unchanged", async () => {
        // Used to simulate user inputs
        const user = userEvent.setup();

        // Mocked function for checking if the url can be updated
        const setServerUrl = vi.fn((url) => {
            expect(url).toBe("testurl.com");
        });

        // Render the component in a router so navigation between pages works
        render(<MemoryRouter>
            (<SetServerURL serverUrl={"testurl.com"} setServerUrl={setServerUrl}/>);
        </MemoryRouter>)

        // Verify the passed url is the placeholder url
        let input = screen.getByRole("textbox");
        expect(input).toBeInTheDocument();
        expect(input).toHaveDisplayValue("testurl.com");

        // Press the set button
        const button = screen.getByRole("button", {name: /set/i});
        expect(button).toBeInTheDocument();
        expect(button).toHaveTextContent(/set/i);
        await user.click(button);
    });

    it("should call the mocked function when the url gets set after being changed", async () => {
        // Used to simulate user inputs
        const user = userEvent.setup();

        // Mocked function for checking if the url can be updated
        const setServerUrl = vi.fn((url) => {
            expect(url).toBe("testurl2.com");
        });

        // Render the component in a router so navigation between pages works
        render(<MemoryRouter>
            (<SetServerURL serverUrl={"testurl1.com"} setServerUrl={setServerUrl}/>);
        </MemoryRouter>)

        // Update the url to the new value
        let input = screen.getByRole("textbox");
        expect(input).toBeInTheDocument();
        expect(input).toHaveDisplayValue("testurl1.com");
        await user.clear(input);
        await user.type(input, "testurl2.com");

        // Press the set button
        const button = screen.getByRole("button", {name: /set/i});
        expect(button).toBeInTheDocument();
        expect(button).toHaveTextContent(/set/i);
        await user.click(button);
    });
});