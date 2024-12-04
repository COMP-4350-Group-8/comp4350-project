// This function is in its own file so it can be easily mocked in tests and re-used if needed

// Called by Home to get all of the races from the backend
export default async function getRaces(serverUrl, setRaceData) {
    try {
        const response = await fetch(`${serverUrl}/race`);

        if (!response.ok) {
            throw new Error(`HTTP error: ${response.status}`);
        }

        const data = await response.json();
        setRaceData(data);
    } catch (error) {
        console.error("Error fetching data:", error);
    }
};