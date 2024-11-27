// This function is in its own file so it can be easily mocked in tests and re-used if needed

// Called by Home to get all of the courses from the backend
export default function getRegatta(id) {
    return {name: "Regatta1", races: [{name: "Race 1"}, {name: "The second race"}]};
};