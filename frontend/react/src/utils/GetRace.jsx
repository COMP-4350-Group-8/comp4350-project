// This function is in its own file so it can be easily mocked in tests and re-used if needed

// Called by ViewRace to get the data for the race with the matching id
export default async function getRace(serverUrl, id, setRaceData) {
    try {
        const response = await fetch(`${serverUrl}/race/${id}`);

        if (!response.ok) {
            throw new Error(`HTTP error: ${response.status}`);
        }

        const data = await response.json();

        // Ensure the race has a course before continuing
        if (data.courseId) {
            const courseResponse = await fetch(`${serverUrl}/course/${data.courseId}`);

            if (!courseResponse.ok) {
                throw new Error(`HTTP error: ${courseResponse.status}`);
            }
            
            const courseData = await courseResponse.json();
    
            // Add the course data into the race data
            data.courses = [];
            data.courses.push(courseData);
        }
        
        setRaceData(data);
    } catch (error) {
        console.error("Error fetching data:", error);
    }
};