import React, {useRef} from "react";
import { useNavigate } from 'react-router-dom';
import PropTypes from 'prop-types';
import Card from "../../components/ui/Card";
import "./SetServerURL.css"; 

// Define the props that should be passed to this component
SetServerURL.propTypes = {
    serverUrl: PropTypes.string,
    setServerUrl: PropTypes.func
}

// 
export default function SetServerURL({serverUrl, setServerUrl})  {
    // Used to navigate back to the homepage after submitting the new server url
    const navigate = useNavigate();

    const urlInputRef = useRef();

    function submitHandler(event) {
        event.preventDefault();

        setServerUrl(urlInputRef.current.value);

        navigate("/");
    }

    return (
        <>
            <h1 className="set-header">Set Custom Server URL</h1>
            <Card>
                <form className="set-form" onSubmit={submitHandler}>
                    <div className="set-control">
                        <label htmlFor='title'>Enter the URL for your server</label>
                        <input defaultValue={serverUrl} type='text' required id='serverUrl' ref={urlInputRef}/>
                    </div>
                    <div>
                        <button className="set-button">Set</button>
                    </div>
                </form>
            </Card>
        </>
    );
}