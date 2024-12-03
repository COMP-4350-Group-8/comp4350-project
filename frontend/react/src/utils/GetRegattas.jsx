// This function is in its own file so it can be easily mocked in tests and re-used if needed

// Called by Home to get all of the regattas from the backend
export default async function getRegattas(serverUrl, setRegattaData) {
    try {
        const response = await fetch(`${serverUrl}/regatta`);

        if (!response.ok) {
            throw new Error(`HTTP error: ${response.status}`);
        }

        const data = await response.json();
        setRegattaData(data);
    } catch (error) {
        console.error("Error fetching data:", error);
    }
};