// This function is in its own file so it can be easily mocked in tests and re-used if needed

// Called by ViewRegatta to get the data for the regatta with the matching id
export default async function getRegatta(id, setRegattaData) {
    try {
        const response = await fetch(`http://localhost:5000/regatta/${id}`);

        if (!response.ok) {
            throw new Error(`HTTP error: ${response.status}`);
        }

        const data = await response.json();
        setRegattaData(data);
    } catch (error) {
        console.error("Error fetching data:", error);
    }
};