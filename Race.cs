using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRaceSimulation
{
    internal class Race
    {
        int distance; // Race distance in km
        List<Car> cars = new List<Car>(); // List of participating cars

        // Constructor to initialize the race with a given distance
        public Race(int distance)
        {
            this.distance = distance;
        }

        // Method to add a car to the race
        public void AddCar(Car car)
        {
            cars.Add(car);
        }

        // Method to simulate events for a car
        public async Task SimulateEvents(Car car)
        {
            Random random = new Random();
            while (car.Position < distance)
            {
                await Task.Delay(30000); // Simulate time passing (30 seconds)
                double randomEventProbability = random.NextDouble();
                foreach (var (eventName, eventProbability, eventEffect) in car.Events)
                {
                    if (randomEventProbability < eventProbability) // Compare with eventProbability
                    {
                        car.Speed -= eventEffect;
                        Console.WriteLine($"{car.Name} has a {eventName} and its speed is reduced to {car.Speed} km/h.");
                        break; // Only one event can happen at a time
                    }
                }
            }
        }

        // Method to start the race
        public async Task StartRace()
        {
            List<Task> carTasks = new List<Task>();
            Console.WriteLine("The race begins!");

            foreach (var car in cars)
            {
                carTasks.Add(SimulateEvents(car));
            }

            await Task.WhenAll(carTasks);

            Car winner = cars.OrderByDescending(c => c.Position).First();
            Console.WriteLine($"{winner.Name} wins the race!");
        }

        // Method to print the status of the race
        public void PrintRaceStatus()
        {
            Console.WriteLine("Race Status:");
            foreach (var car in cars)
            {
                Console.WriteLine($"{car.Name}: Position = {car.Position} km, Speed = {car.Speed} km/h");
            }
        }

    }
}
