// This function is in its own file so it can be easily mocked in tests and re-used if needed

// Called by CreateRegatta when a regatta form is submitted and sends the data to the backend
export default function addRegattaHandler(regattaData) {
    fetch(
        'http://localhost:5000/regatta',
        {
            method: 'POST',
            body: JSON.stringify(regattaData),
            headers: {
                'Content-Type': 'application/json'
            }
        }
    );
};