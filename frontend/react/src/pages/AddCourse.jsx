export default function addCourseHandler(courseData) {
    // Will create a 404 error until a backend can be used
    fetch(
        '',
        {
            method: 'POST',
            body: JSON.stringify(courseData),
            headers: {
                'Content-Type': 'application/json'
            }
        }
    );

    // For testing until a local backend can be used instead
    console.log(`Added course ${JSON.stringify(courseData, null, 4)}`);
};