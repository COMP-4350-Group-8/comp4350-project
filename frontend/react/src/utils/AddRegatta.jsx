// This function is in its own file so it can be easily mocked in tests and re-used if needed

// Called by CreateRace when a race form is submitted and sends the data to the backend
export default function addRegattaHandler(regattaData) {
    // fetch(
    //     'http://localhost:5000/course',
    //     {
    //         method: 'POST',
    //         body: JSON.stringify(courseData),
    //         headers: {
    //             'Content-Type': 'application/json'
    //         }
    //     }
    // );

    console.log(`Added regatta ${JSON.stringify(regattaData, null, 4)}`);
};