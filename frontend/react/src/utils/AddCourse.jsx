// This function is in its own file so it can be easily mocked in tests and re-used if needed

// Called by CreateCourse when a course form is submitted and sends the data to the backend
export default function addCourseHandler(serverUrl, courseData) {
    fetch(
        `${serverUrl}/course`,
        {
            method: 'POST',
            body: JSON.stringify(courseData),
            headers: {
                'Content-Type': 'application/json'
            }
        }
    );
};