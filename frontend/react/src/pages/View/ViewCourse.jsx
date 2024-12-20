import React ,{useState, useEffect}from "react";
import {useParams} from "react-router-dom";
import PropTypes from 'prop-types';
import CourseView from "../../components/view/CourseView";
import getCourse from "../../utils/GetCourse";

// Define the props that should be passed to this component
ViewCourse.propTypes = {
    serverUrl: PropTypes.string
}

export default function ViewCourse({serverUrl}) {
    let {id} = useParams();
    id = parseInt(id);
    const [isLoading, setIsLoading] = useState(true);
    const [courseData, setCourseData] = useState(null);

    //get request to get course details
    useEffect(() => {
        setIsLoading(true);
        getCourse(serverUrl, id, setCourseData, setIsLoading);
    }, [serverUrl, id])

    if (isLoading) {
        return (
            <section>
                <p>Loading...</p>
            </section>
        );
    }

    return(
        <CourseView serverUrl={serverUrl} courseData={courseData}>
        </CourseView>
    )
}