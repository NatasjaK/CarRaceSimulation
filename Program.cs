// Main program
using CarRaceSimulation;

class Program
{
    static async Task Main(string[] args)
    {
        Race race = new Race(10); // Create a race with a 10 km distance

        // Create car objects
        Car volvo = new Car("Volvo");
        Car ford = new Car("Ford");
        Car bmw = new Car("BMW");

        // Add cars to the race
        race.AddCar(volvo);
        race.AddCar(ford);
        race.AddCar(bmw);

        // Start the race and print status periodically
        var raceTask = race.StartRace();
        var statusTask = PrintRaceStatusPeriodically(race);

        await Task.WhenAll(raceTask, statusTask);

        Console.ReadLine(); // Allow the user to view race results
    }

    // Method to print race status periodically
    static async Task PrintRaceStatusPeriodically(Race race)
    {
        while (true)
        {
            race.PrintRaceStatus();
            await Task.Delay(5000); // Print every 5 seconds
        }
    }
}
