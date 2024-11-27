// This function is in its own file so it can be easily mocked in tests and re-used if needed

// Called by ViewRegatta to get the data for the regatta with the matching id
export default function getRegatta(id) {
    return {name: "Regatta1", races: [{id: 1, name: "Race 1"}, {id: 2, name: "The second race"}]};
};