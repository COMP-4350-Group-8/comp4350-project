import "./CreateRace.css"
import RaceForm from "../components/forms/RaceForm.jsx";

export default  function CreateRace()  {

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
    }

    return (
        <RaceForm  onAddCourse ={addCourseHandler}/>
    );

}
