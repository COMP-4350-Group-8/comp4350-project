import React from "react";
import PropTypes from "prop-types";

// Define the props that should be passed to this component
Gmap.propTypes = {
    latitude: PropTypes.number,
    longitude: PropTypes.number,
}

// Render a google maps iframe at the passed coordinates
export default  function Gmap({latitude, longitude})  {
    // Get the map
    const mapSrc = `https://maps.google.com/maps?q=${latitude},${longitude}&z=15&output=embed`;

    // Render the iframe using the map
    return (
        <div>
            <iframe
                title="Google Map"
                width="340"
                height="270"
                frameBorder="0"
                style={{border: 0}}
                src={mapSrc}
            ></iframe>
        </div>
    );
}
