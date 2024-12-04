// This function is in its own file so it can be easily mocked in tests and re-used if needed

// Called by Home to get all of the courses from the backend
export default async function getCourses(serverUrl, setCourseData) {
    try {
        const response = await fetch(`${serverUrl}/course`);

        if (!response.ok) {
            throw new Error(`HTTP error: ${response.status}`);
        }

        const data = await response.json();
        setCourseData(data);
    } catch (error) {
        console.error("Error fetching data:", error);
    }
};