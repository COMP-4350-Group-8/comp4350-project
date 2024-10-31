import React from "react";

export default  function Gmap(props)  {

    // eslint-disable-next-line react/prop-types
    const mapSrc = `https://maps.google.com/maps?q=${props.latitude},${props.longitude}&z=15&output=embed`;
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
