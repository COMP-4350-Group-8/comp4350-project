// This function is in its own file so it can be easily mocked in tests and re-used if needed

// Called by ViewRace to get the data for the race with the matching id
export default async function getRace(id, setRaceData) {
    try {
        const response = await fetch(`http://localhost:5000/race/${id}`);

        if (!response.ok) {
            throw new Error(`HTTP error: ${response.status}`);
        }

        const data = await response.json();
        setRaceData(data);
    } catch (error) {
        console.error("Error fetching data:", error);
    }
};