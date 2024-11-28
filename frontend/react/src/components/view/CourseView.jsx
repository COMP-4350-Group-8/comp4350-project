import React from "react";
import Card from '../ui/Card';
import ViewMap from '../map/ViewMap.jsx'
import classes from './CourseView.module.css'
import PropTypes from "prop-types";

CourseView.propTypes = {
    courseData: PropTypes.shape({
        id: PropTypes.number,
        name: PropTypes.string,
        description: PropTypes.string,
        courseMarks: PropTypes.arrayOf(PropTypes.shape({
            id: PropTypes.number,
            latitude: PropTypes.string,
            longitude: PropTypes.string,
            description: PropTypes.string,
            rounding: PropTypes.bool,
            isStartLine: PropTypes.bool,
            gate: PropTypes.string,
            courseId: PropTypes.number,
        })),
    })
}

export default function CourseView({courseData}) {
    const points = [];

    for (let i = 0; i < courseData.courseMarks.length; i++) {
        points.push(
            <div className={classes.mapbox}>
                <div className={classes.boxhead}> Marker {i + 1}</div>
                <div className={classes.details}>Description : {courseData.courseMarks[i].description}</div>
                <div className={classes.details}>Latitude : {courseData.courseMarks[i].latitude}</div>
                <div className={classes.details}>Longitude : {courseData.courseMarks[i].longitude}</div>
            </div>
        )
    }
    return (
        <Card>
            <div>
                <div className={classes.title}> {courseData.name}</div>
                <div className={classes.mapbox}><ViewMap markers={courseData.courseMarks}/></div>
                <div className={classes.mapbox}>
                    <div className={classes.boxhead}> DESCRIPTION</div>
                    <div className={classes.para}>{courseData.description}</div>
                </div>
                {points}
            </div>
        </Card>
    )
}