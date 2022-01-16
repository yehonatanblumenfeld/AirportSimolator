using AirportSimolator.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;

namespace Airport.Simulator
{
    public class Connection
    {
        static HubConnection connection;

        public Connection(Logic logic)
        {
            if (connection == null)
            {
                connection = new HubConnectionBuilder()
                    .WithUrl("https://localhost:44369/airportserverhub")
                    .Build();

                connection.On("LandPlane", (string msg , int count) =>
                {
                    logic.Message = msg;
                    logic.PlanesCount = count;
                    logic.WriteMenuCommands();
                });

                connection.On("DepartPlane", (string msg) =>
                {
                    logic.Message = msg;
                    logic.WriteMenuCommands();
                });  
                
                connection.KeepAliveInterval = TimeSpan.FromSeconds(5);
                connection.ServerTimeout = TimeSpan.FromMinutes(2);

                connection.StartAsync().Wait();
            }
        }

        public HubConnection ConnectionInstance { get { return connection; } }
    }
}
