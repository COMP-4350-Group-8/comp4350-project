// This function is in its own file so it can be easily mocked in tests and re-used if needed

// Called by ViewCourse to get the data for the course with the matching id
export default async function getCourse(serverUrl, id, setCourseData, setIsLoading) {
    try {
        await fetch(`${serverUrl}/course/${id}`)
            .then((response) => {
                if (!response.ok) {
                    throw new Error(`HTTP error: ${response.status}`);
                }
                
                return response.json();
            })
            .then((data) => {
                setIsLoading(false);
                setCourseData(data);
            });
    } catch (error) {
        console.error("Error fetching data:", error);
    }
};