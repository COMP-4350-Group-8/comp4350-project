// This function is in its own file so it can be easily mocked in tests and re-used if needed

// Called by CreateCourse when a course form is submitted and sends the data to the backend
export default async function addCourseHandler(serverUrl, courseData, gateMarkerData) {
    try {
        // Post the new course and its markers
        await fetch(
            `${serverUrl}/course`,
            {
                method: 'POST',
                body: JSON.stringify(courseData),
                headers: {
                    'Content-Type': 'application/json'
                }
            }
        );

        // Update the marks that are gates so they are correctly linked together.
        // This needs to be done after the course (and marks) are created so the marks can be linked with foreign keys
        gateMarkerData.forEach(async (gateMarker) => {
            await fetch(
                `${serverUrl}/course/marks/${gateMarker.id}`,
                {
                    method: 'PUT',
                    body: JSON.stringify(gateMarker),
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }
            );
        });
    } catch (error) {
        console.error("Error posting data:", error);
    }
};