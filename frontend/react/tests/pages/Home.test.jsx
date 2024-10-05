import { render, screen } from "@testing-library/react";
import React from 'react';
import { MemoryRouter } from 'react-router-dom';
import Home from  '../../src/pages/Home';

describe("Home", () => {
    it("should render the header", () => {
        render(<MemoryRouter>
            (<Home/>);
        </MemoryRouter>)

        const heading = screen.getAllByRole("heading");
        expect(heading[0]).toBeInTheDocument();
        expect(heading[0]).toHaveTextContent(/sail mapper/i);
    });

    it("should render the Start Race button", () => {
        render(<MemoryRouter>
            (<Home/>);
        </MemoryRouter>)

        const button = screen.getByRole("button");
        expect(button).toBeInTheDocument();
        expect(button).toHaveTextContent(/start race/i);
    });
});
