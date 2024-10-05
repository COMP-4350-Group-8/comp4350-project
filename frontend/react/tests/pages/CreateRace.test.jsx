import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import React from "react";
import CreateRace from "../../src/pages/CreateRace.jsx";

// Some tests in this file share code, but the alternative was a mega test, so we chose to do it this way

describe("CreateRace", () => {
    it("should render the header", () => {
        render(<CreateRace/>);

        const heading = screen.getAllByRole("heading");
        expect(heading[0]).toBeInTheDocument();
        expect(heading[0]).toHaveTextContent(/create race/i);
    });

    it("should render the note form", () => {
        render(<CreateRace/>);

        expect(screen.getByText(/notes for race/i)).toBeInTheDocument();
        
        expect(screen.getByRole("textbox")).toBeInTheDocument();

        const button = screen.getByRole("button");
        expect(button).toBeInTheDocument();
        expect(button).toHaveTextContent(/add note/i);
    });

    it("should render an empty list of notes", () => {
        render(<CreateRace/>);

        expect(screen.getByText(/no notes/i)).toBeInTheDocument();
    });

    it("should add a note to the list when the user clicks the Add Note button", async () => {
        const user = userEvent.setup();

        render(<CreateRace/>);

        // Confirm the textbox exists, and write "hello" in it
        const textbox = screen.getByRole("textbox");
        expect(textbox).toBeInTheDocument();
        await user.click(textbox);
        await user.keyboard("hello");

        // Add the note to the list
        const button = screen.getByRole("button");
        expect(button).toBeInTheDocument();
        await user.click(button);

        // Confirm the note was added to the list
        expect(screen.getByText(/hello/i)).toBeInTheDocument();
        expect(screen.getByText(/delete/i)).toBeInTheDocument();
    });

    it("should delete a note from the list when the user clicks the note's Delete button", async () => {
        const user = userEvent.setup();

        render(<CreateRace/>);

        // Confirm the textbox exists, and write "howdy" in it
        const textbox = screen.getByRole("textbox");
        expect(textbox).toBeInTheDocument();
        await user.click(textbox);
        await user.keyboard("howdy");

        // Add the note to the list
        const button = screen.getByText(/add note/i);
        expect(button).toBeInTheDocument();
        await user.click(button);

        // Confirm the note was added to the list
        expect(screen.getByText(/howdy/i)).toBeInTheDocument();

        // Delete the note from the list
        const deleteButton = screen.getByText(/delete/i);
        expect(deleteButton).toBeInTheDocument();
        await user.click(deleteButton);

        // Confirm the note was deleted from the list
        expect(screen.queryByText(/howdy/i)).not.toBeInTheDocument();
        expect(screen.queryByText(/delete/i)).not.toBeInTheDocument();
    });

    it("should maintain proper note order while adding and deleting notes", async () => {
        const user = userEvent.setup();

        render(<CreateRace/>);

        // Add the first 3 notes to the list
        const notes = ["hello", "howdy", "hi there"]
        const textbox = screen.getByRole("textbox");
        expect(textbox).toBeInTheDocument();
        const button = screen.getByText(/add note/i);
        expect(button).toBeInTheDocument();
        for (let note of notes) {
            await user.click(textbox);
            await user.keyboard(note);
            await user.click(button);
            expect(screen.getByText(note)).toBeInTheDocument();
        }

        // Delete the second note from the list
        const deleteButtons = screen.getAllByText(/delete/i);
        expect(deleteButtons).toHaveLength(3);
        deleteButtons.forEach((deleteButton) => {
            expect(deleteButton).toBeInTheDocument();
        })
        await user.click(deleteButtons[1]);

        // Confirm the second note was deleted from the list
        expect(screen.queryByText(/howdy/i)).not.toBeInTheDocument();

        // Confirm the first and second note are still in the list
        let noteTexts = screen.getAllByText(/h/i);
        expect(noteTexts).toHaveLength(2);
        expect(noteTexts[0]).toHaveTextContent(/hello/i);
        expect(noteTexts[1]).toHaveTextContent(/hi there/i);

        // Add a fourth note
        await user.click(textbox);
        await user.keyboard("how're you doing");
        await user.click(button);

        // Confirm the fourth note is in the third position
        noteTexts = screen.getAllByText(/h/i);
        expect(noteTexts).toHaveLength(3);
        expect(noteTexts[2]).toHaveTextContent(/how're you doing/i);
    });
});