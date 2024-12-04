// This function is in its own file so it can be easily mocked in tests and re-used if needed

// Called by CreateRegatta when a regatta form is submitted and sends the data to the backend
export default async function addRegattaHandler(serverUrl, regattaData, raceDataList) {
    try {
        await fetch(
            `${serverUrl}/regatta`,
            {
                method: 'POST',
                body: JSON.stringify(regattaData),
                headers: {
                    'Content-Type': 'application/json'
                }
            }
        );

        raceDataList.forEach((race) => {
            fetch(
                `${serverUrl}/race/${race.id}`,
                {
                    method: 'PUT',
                    body: JSON.stringify(race),
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