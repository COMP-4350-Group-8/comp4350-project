// This function is in its own file so it can be easily mocked in tests

// Called by Home to get all of the courses from the backend
export default function getRaces() {
    return [{name: "Race1"}, {name: "Race2"}];
};