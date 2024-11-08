import React from "react";
import PropTypes from "prop-types";
import classes from './Card.module.css';

// Define the props that should be passed to this component
Card.propTypes = {
    children: PropTypes.element,
};

function Card({children}) {
    return <div className={classes.card}>{children}</div>;
}

export default Card;