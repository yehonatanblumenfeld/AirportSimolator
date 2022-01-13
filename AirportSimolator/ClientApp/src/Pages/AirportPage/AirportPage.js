import React, { useState } from 'react';
import "../AirportPage/AirportStyle.css";
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';

const AirportPage = () => {
    //const [planeName, setPlaneName] = useState();
    const [connection, setConnection] = useState(); //Connection to hub 
    const [stations, setStations] = useState();
    const connectToAirport = async () => {
        if (connection != null) return;
        try {
            const connect = new HubConnectionBuilder()
                .withUrl("https://localhost:44369/airportclienthub")
                .configureLogging(LogLevel.Information)              
                .build();

            connect.on("SendUpdatedStations", (stations) => {
                console.log(stations);
                setStations(stations);
            });
            
            //connect.on("LandPlane", (plane) => {
            //    console.log(plane)
            //});

            connect.on("DepartPlanes");

            connect.on("GetStations");
                  
            connect.on("LandPlanes");

            connect.on("sendPlaneUpdate", (msg) => {
                console.log(msg);
            });            

            connect.keepAliveIntervalInMilliseconds = 5000;
            connect.serverTimeoutInMilliseconds = 120000;
            await connect.start();         
            setConnection(connect);
        }
        catch (e) {
            console.log(e)
        }
    }
    connectToAirport();
   
    const handleLandPlane = () => {
        connection.invoke("LandPlane");
    }
    const handleDepartPlanes = () => {
        connection.invoke("DepartPlanes");
    }   

    return (
        <div className="myDiv">
            Hello And welcome to the AirPort !     
            <div className="wrapper">
                <div className="box landing">1</div>
                <div className="box landing">2</div>
                <div className="box landing">3</div>
                <div className="box landingAndTakeoff">4</div>
                <div className="box landing">5</div>\
                <div></div>
                <div className="box parking">6</div>
                <div className="box parking">7</div>
                <div className="box takeOff">8</div>
            </div>
            <br/>
            <div >
                <p className="green"><strong>Landing</strong></p>
                <p className="red"><strong>Landing & TakeOff</strong></p>
                <p className="blue"><strong>Parking</strong></p>
                <p className="orange"><strong>TakeOff</strong></p>
            </div>
        </div>
    );
}
export default AirportPage;