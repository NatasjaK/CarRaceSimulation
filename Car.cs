using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRaceSimulation
{
    internal class Car
    {
        public string Name { get; set; }
        public double Position { get; set; }
        public int Speed { get; set; }

        public Car(string name, int speed)
        {
            Name = name;
            Position = 0;
            Speed = 120; // Grundhastighet (120 km/h)
        }
    }
}
