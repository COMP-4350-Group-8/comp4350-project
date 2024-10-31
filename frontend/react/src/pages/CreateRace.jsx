import RaceForm from "../components/forms/RaceForm.jsx";
import "./CreateRace.css";

export default function CreateRace()  {

    //Enter the URL in the '' fo the fetch function
    function addCourseHandler(courseData) {
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

    return (
        <RaceForm  onAddCourse ={addCourseHandler}/>
    );

}
