using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRaceSimulation
{
    internal class Car
    {
        public string Name { get; } // Car's name
        public double Position { get; set; } // Current position of the car in km
        public double Speed { get; set; } // Initial speed of the car in km/h
        public List<(string, double, int)> Events { get; } // List of possible events

        // Constructor to initialize the car
        public Car(string name)
        {
            Name = name;
            Position = 0;
            Speed = 120; // Initial speed in km/h
                         // List of events with their probabilities and effects
            Events = new List<(string, double, int)>
        {
            ("Out of Gas", 1.0 / 50, 30),
            ("Flat Tire", 2.0 / 50, 20),
            ("Bird on Windshield", 5.0 / 50, 10),
            ("Engine Failure", 10.0 / 50, 1)
        };
        }
    }
}
