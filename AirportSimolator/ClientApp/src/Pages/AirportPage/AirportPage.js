import React, { useEffect, useState } from 'react';
import "../AirportPage/AirportStyle.css";
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import Station from '../../components/Station';


const AirportPage =  () => {
    const [connection, setConnection] = useState(); //Connection to hub 
    const [stations, setStations] = useState([]);
    const [loaded, SetIsLoaded] = useState(false);
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

            connect.on("LandPlane", (plane) => {
                console.log(plane)
            });

            connect.on("DepartPlanes");

            connect.on("GetStations", (stations) => {
                console.log(stations);
                setStations(stations);
            });

            connect.on("LandPlanes");

            connect.on("sendPlaneUpdate", (msg) => {
                console.log(msg);
            });

            connect.keepAliveIntervalInMilliseconds = 5000;
            connect.serverTimeoutInMilliseconds = 120000;

            await connect.start();

            await connect.invoke("GetStations").then(() => {
                console.log(stations);
            });
            setConnection(connect);
        }
        catch (e) {
            console.log(e)
        }
    }

    useEffect(() => {
        const doIt = async () => {
            await connectToAirport();
        }
        doIt();
        SetIsLoaded(true);
    }, [])
  
    return (
        <div>
            <h1>Airport Simulator</h1>          
            <div className="myDiv">         
                <div className="wrapper">
                    {loaded ? stations.map((station) => {
                        return <Station key={station.stationId} station={station} />
                    }) : null}         
                </div>
            </div>
        </div>
    );
}
export default AirportPage;