import React ,{useState, useEffect}from "react";
import CourseView from "../components/view/CourseView";
import {useParams} from "react-router-dom";

export default function ViewCourse() {
    let {id} = useParams();
    id = parseInt(id);
    const [isLoading, setIsLoading] = useState(true);
    const [courseData, setCourseData] = useState(null);

    useEffect(() => {
        setIsLoading(true);
        fetch(
            'https://react-getting-started-48dec-default-rtdb.firebaseio.com/meetups.json'
        )
            .then((response) => {
                return response.json();
            })
            .then((data) => {
                setIsLoading(false);
                setCourseData(data);
            });
    }, [])

    if (isLoading) {
        return (
            <section>
                <p>Loading...</p>
            </section>
        );
    }

    return(
        <CourseView courseData={courseData}></CourseView>
    )
}