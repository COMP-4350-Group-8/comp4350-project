// This function is in its own file so it can be easily mocked in tests and re-used if needed

// Called by Home to get all of the courses from the backend
export default function getRegattas() {
    return [{id: 1, name: "Regatta1"}, {id: 2, name: "Regatta2"}, {id: 3, name: "This is the third regatta"}];
};