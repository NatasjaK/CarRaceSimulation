namespace CarRaceSimulation;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Car Race!");

        // Create two car objects
        Car car1 = new Car("Car 1", 120);
        Car car2 = new Car("Car 2", 120);

        // Create threads for each car
        Thread thread1 = new Thread(() => Race(car1));
        Thread thread2 = new Thread(() => Race(car2));

        // Start the race
        thread1.Start();
        thread2.Start();

        static void Race(Car car)
        {
            while (car.Position < 10) // Sträckan att tävla på (10 km)
            {
                int randomEvent = new Random().Next(1, 51); // Slumpa en siffra mellan 1 och 50

                if (randomEvent == 1) // 1/50 chans för slut på bensin
                {
                    Console.WriteLine($"{car.Name} ran out of fuel and needs to refuel. Stopping for 30 seconds.");
                    Thread.Sleep(30000); // Stoppa i 30 sekunder
                }
                else if (randomEvent <= 3) // 2/50 chans för punktering
                {
                    Console.WriteLine($"{car.Name} got a flat tire and needs to change it. Stopping for 20 seconds.");
                    Thread.Sleep(20000); // Stoppa i 20 sekunder
                }
                else if (randomEvent <= 8) // 5/50 chans för fågel på vindrutan
                {
                    Console.WriteLine($"{car.Name} has a bird on the windshield and needs to clean it. Stopping for 10 seconds.");
                    Thread.Sleep(10000); // Stoppa i 10 sekunder
                }
                else if (randomEvent <= 18) // 10/50 chans för motorfel
                {
                    Console.WriteLine($"{car.Name} has an engine problem. Speed reduced by 1 km/h.");
                    car.Speed -= 1; // Minska hastigheten med 1 km/h
                }

                car.Position += car.Speed; // Flytta bilen framåt
                Thread.Sleep(1000); // Vänta 1 sekund för att simulera ett steg framåt
            }

            Console.WriteLine($"{car.Name} finished the race!");
        }

        Console.WriteLine("Race is over!");
    }
}
