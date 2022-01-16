using AirportSimolator.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Timers;

namespace Airport.Simulator
{
    public class Logic
    {
        private Connection _simulatorConnection;

        Timer _landPlaneTimer;
        Timer _departPlaneTimer;

        Random _random;
        string[] _planesCompanies;
        bool _autoLand, _autoDepart;

        ConsoleColor _commandColor = ConsoleColor.Yellow;
        ConsoleColor _statesColor = ConsoleColor.Green;
        ConsoleColor _enterCommandColor = ConsoleColor.Magenta;
        ConsoleColor _messageColor = ConsoleColor.Blue;
        ConsoleColor _disconnectedColor = ConsoleColor.Red;

        public string Message { get; set; }
        public int PlanesCount { get; set; }

        public Logic()
        {
            _simulatorConnection = new Connection(this);
            Message = "no messages";

            // timers that lands a plane
            _landPlaneTimer = new Timer();
            _landPlaneTimer.Interval = 2000;
            _landPlaneTimer.Enabled = true;

            // timers that departs a plane
            _departPlaneTimer = new Timer();
            _departPlaneTimer.Interval = 2000;
            _departPlaneTimer.Enabled = true;

            _planesCompanies = new string[] { "IsraAir", "El-Al", "Boeing", "Airbus", "Lockheed Martin", "safran", "Leonardo", "DeltaAir" };

            _random = new Random();
        }

        public void WriteMenuCommands()
        {
            Console.Clear();
            Console.ResetColor();

            Console.WriteLine("welcome to the simulator");
            Console.ForegroundColor = _statesColor;
            Console.WriteLine($"-  autoLand = {_autoLand}");
            Console.WriteLine($"-  autodepart =  {_autoDepart}");
            if (_simulatorConnection.ConnectionInstance.State == HubConnectionState.Disconnected)
                Console.ForegroundColor = _disconnectedColor;
            Console.WriteLine($"-  connection = {_simulatorConnection.ConnectionInstance.State}\n");
            Console.ResetColor();

            Console.WriteLine("Airport commands - type the action you want to perform\n ");
            Console.ForegroundColor = _commandColor;
            Console.WriteLine("-   connect    -  connects to the server");
            Console.WriteLine("-  disconnect  - disconnects from the server");
            Console.WriteLine("-  autoland   -  start/stop landing planes automaticlly");
            Console.WriteLine("-  autodepart  -  start/stop landing planes automaticlly");
            Console.WriteLine("-  landplane   -  land plane");
            Console.WriteLine("-  departplane - depart plane");
            Console.WriteLine("-     exit     -  stop and exit's the application  \n");

            Console.WriteLine($"Current number of planes in airport {PlanesCount}");
            Console.ResetColor();
            Console.WriteLine("------------------------------Messages------------------------------");
            Console.ForegroundColor = _messageColor;
            Console.WriteLine(Message + "\n");
            Console.ResetColor();
            Console.WriteLine("--------------------------------------------------------------------");

            Console.ForegroundColor = _enterCommandColor;
            Console.WriteLine("Please Enter your command here:\n");
            Console.ResetColor();
        }

        public bool checkCommand(string command)
        {
            switch (command)
            {
                case "connect":
                    this.Connect();
                    break;
                case "disconnect":
                    this.Disconnect();
                    break;
                case "exit":
                    return false;
            }
            if (_simulatorConnection.ConnectionInstance.State != HubConnectionState.Connected)
            {
                Message = "the simulator disconnected from the server";
                return true;
            }


            switch (command)
            {
                case "landplane":
                    this.LandPlane();
                    break;
                case "departplane":
                    this.DepartPlane();
                    break;
                case "autoland":
                    this.LandPlaneAuto();
                    break;
                case "autodepart":
                    this.DepartPlaneAuto();
                    break;
                default:
                    break;
            }
            return true;

        }

        private void LandPlane()
        {
            var planeName = "";
            while (true)
            {
                Console.WriteLine("Landing Process");
                Console.WriteLine("---------------------------------");
                Console.WriteLine("Enter plane name:");
                planeName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(planeName))
                    break;
            }
            _simulatorConnection.ConnectionInstance.InvokeAsync("LandPlane", planeName);
        }

        private void DepartPlane()
        {
            var planeName = "";
            while (true)
            {
                Console.WriteLine("Departing Process");
                Console.WriteLine("---------------------------------");
                Console.WriteLine("Enter plane name:");
                planeName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(planeName))
                    break;
            }
            _simulatorConnection.ConnectionInstance.InvokeAsync("DepartPlane", planeName);
        }

        public void LandPlaneAuto()
        {
            _autoLand = !_autoLand;
            if (_autoLand)
            {
                _landPlaneTimer.Elapsed += LandPlaneTimer_Elapsed;
                _landPlaneTimer.Start();
            }
            else
            {
                _landPlaneTimer.Elapsed -= LandPlaneTimer_Elapsed;
                _landPlaneTimer.Stop();
            }
        }

        public void DepartPlaneAuto()
        {
            _autoDepart = !_autoDepart;
            if (_autoDepart)
            {
                _departPlaneTimer.Elapsed += DepartPlaneTimer_Elapsed; ;
                _departPlaneTimer.Start();
            }
            else
            {
                _departPlaneTimer.Elapsed -= DepartPlaneTimer_Elapsed;
                _departPlaneTimer.Stop();
            }
        }

        public void Connect()
        {
            if (_simulatorConnection.ConnectionInstance.State != HubConnectionState.Connected)
            {
                _simulatorConnection.ConnectionInstance.StartAsync().Wait();
                _simulatorConnection.ConnectionInstance.InvokeAsync("GetPlanes");
                Message = "server connected succesfully";
            }
        }

        public void Disconnect()
        {
            if (_simulatorConnection.ConnectionInstance.State != HubConnectionState.Disconnected)
            {
                _simulatorConnection.ConnectionInstance.StopAsync().Wait();
                _autoDepart = false;
                _autoLand = false;
                Message = "server disconnected succesfully";
            }
        }

        private void LandPlaneTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_simulatorConnection.ConnectionInstance.State == HubConnectionState.Connected)
            {
                var planeName = $"{_planesCompanies[_random.Next(0, _planesCompanies.Length)]} {_random.Next(100)}";
                _simulatorConnection.ConnectionInstance.InvokeAsync("LandPlane", planeName);
            }
        }

        private void DepartPlaneTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_simulatorConnection.ConnectionInstance.State == HubConnectionState.Connected)
            {             
                _simulatorConnection.ConnectionInstance.InvokeAsync("DepartPlanesAuto");
            }
        }
    }
}
