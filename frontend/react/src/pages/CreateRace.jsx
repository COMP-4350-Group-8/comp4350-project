import {useNavigate} from "react-router-dom";
import React, {useState} from "react";
import "./CreateRace.css"

export default  function CreateRace()  {
    const [newNote, setNewNote] = useState("");
    const [notes, setNotes] = useState([]);

    function handleSubmit(e){
        e.preventDefault();

        setNotes((currentNotes) => {
                return[
                    ...currentNotes, {id: crypto.randomUUID(), title: newNote}
                ]
            }
        )

        setNewNote("")
    }

    function deleteNote(id){
        setNotes((currentNotes) => {
            return currentNotes.filter(note => note.id !== id)
        })
    }


    return (
        <>
            <div className="create-race-header">
                <h1> Create Race </h1>
            </div>
            <form onSubmit={handleSubmit} className="create-race-form">
                <div className="form-row">
                    <label htmlFor = "note" className= "new-note-header">Notes For Race</label>
                    <input
                        className="new-note-input"
                        value = {newNote}
                        onChange={(e) => setNewNote(e.target.value)}
                        type="text"
                        id="note"
                        placeholder="Enter new note"/>
                </div>
                <button className= "submit-button" >Add Note</button>
            </form>
            <h2 className= "notes-header">NOTES</h2>
            <ul className= "notes-list">
                {notes.length === 0 && "No Notes"}
                {notes.map(note =>{
                    return <li key = {note.id}>
                        <label>
                            {note.title}
                        </label>
                        <button onClick={() => deleteNote(note.id)}
                                className="delete-note">
                            Delete
                        </button>
                    </li>
                })}
            </ul>
        </>
    );
}
