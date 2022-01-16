import React from 'react';
import '../components/StationStyle.css';
import { FaPlane } from 'react-icons/fa';

const Station = ({ station }) => {

    return (
        <div className={`box ${station.state}`}>
            <h4>{station.state} {station.stationId}</h4>
            {station.currectPlaneName ? <FaPlane size={140} text="s" color="#4F8EF7" /> : null}
            <h2>{station.currectPlaneName}</h2>
        </div>
    )
}
export default Station;
