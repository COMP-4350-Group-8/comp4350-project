// This function is in its own file so it can be easily mocked in tests and re-used if needed

// Called by CreateRace when a race form is submitted and sends the data to the backend
export default function addRaceHandler(raceData) {
    fetch(
        'http://localhost:5000/race',
        {
            method: 'POST',
            body: JSON.stringify(raceData),
            headers: {
                'Content-Type': 'application/json'
            }
        }
    );
};