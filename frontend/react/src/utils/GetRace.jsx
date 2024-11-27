// This function is in its own file so it can be easily mocked in tests and re-used if needed

// Called by ViewRace to get the data for the race with the matching id
export default function getRace(id) {
    return {id: 1, name: "Race1", courses: [{id: 1, name: "Course 1"}, {id: 1, name: "The second course"}]};
};