// This function is in its own file so it can be easily mocked in tests and re-used if needed

// Called by Home to get all of the courses from the backend
export default function getRegattas() {
    return [{name: "Regatta1"}, {name: "Regatta2"}, {name: "This is the third regatta"}];
};