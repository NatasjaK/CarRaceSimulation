using System;
using System.Threading;

namespace CarRaceSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Car Race!");

            // Create three car objects
            Car Volvo = new Car("Volvo", 120);
            Car Tesla = new Car("Tesla", 120);
            Car Ford = new Car("Ford", 120);

            // Create threads for each car
            Thread volvoThread = new Thread(() => Race(Volvo));
            Thread teslaThread = new Thread(() => Race(Tesla));
            Thread fordThread = new Thread(() => Race(Ford));


            // Start the race for each car
            volvoThread.Start();
            teslaThread.Start();
            fordThread.Start();

            Console.WriteLine("Race is over!");
        }

        static void Race(Car car)
        {
            while (car.Position < 10) // The race distance (10 km)
            {
                int randomEvent = new Random().Next(1, 51); // Generate a random number between 1 and 50

                if (randomEvent == 1) // 1 in 50 chance of running out of fuel
                {
                    Console.WriteLine($"{car.Name} ran out of fuel and needs to refuel. Stopping for 30 seconds.");
                    Thread.Sleep(30000); // Pause for 30 seconds
                }
                else if (randomEvent <= 3) // 2 in 50 chance of a flat tire
                {
                    Console.WriteLine($"{car.Name} got a flat tire and needs to change it. Stopping for 20 seconds.");
                    Thread.Sleep(20000); // Pause for 20 seconds
                }
                else if (randomEvent <= 8) // 5 in 50 chance of a bird on the windshield
                {
                    Console.WriteLine($"{car.Name} has a bird on the windshield and needs to clean it. Stopping for 10 seconds.");
                    Thread.Sleep(10000); // Pause for 10 seconds
                }
                else if (randomEvent <= 18) // 10 in 50 chance of engine trouble
                {
                    Console.WriteLine($"{car.Name} has an engine problem. Speed reduced by 1 km/h.");
                    car.Speed -= 1; // Reduce speed by 1 km/h
                }

                car.Position += car.Speed; // Move the car forward
                Thread.Sleep(1000); // Wait for 1 second to simulate one step forward
            }

            Console.WriteLine($"{car.Name} finished the race!");
        }
    }
}
