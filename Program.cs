using System.Diagnostics;

namespace carSim
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await CarRace();
        }
        
        public static async Task CarRace()
        {
            Console.ForegroundColor = ConsoleColor.Cyan; // Set header color
            Console.WriteLine("************************************************************");
            Console.WriteLine("*                      Car Race Simulation                  *");
            Console.WriteLine("*                                                            *");
            Console.ForegroundColor = ConsoleColor.White; // Reset text color
            Console.WriteLine("* Hello! Please press any key to begin the car race.         *");
            Console.WriteLine("* While the race is in progress, you can press the [Enter]    *");
            Console.WriteLine("* key to receive real-time updates on the status of the cars. *");
            Console.ForegroundColor = ConsoleColor.Cyan; // Set footer color
            Console.WriteLine("************************************************************");
            Console.ForegroundColor = ConsoleColor.White; // Reset text color

            Console.ReadKey();

            Console.Clear();
            //Create car objects
            Car volvoCar = new Car(1, "Volvo", 0, TimeSpan.Zero, 120, 0, 0);
            Car fordCar = new Car(2, "Ford", 0, TimeSpan.Zero, 120, 0, 0);
            Car bmwCar = new Car(3, "BMW", 0, TimeSpan.Zero, 120, 0, 0);

            // Runs the race simulation for each car
            var volvoRace = Race(volvoCar);
            var fordRace = Race(fordCar);
            var bmwRace = Race(bmwCar);


            // List to check on the updates of each car
            var carRaceStatus = MonitorRaceStatus(new List<Car> { volvoCar, fordCar, bmwCar });
            // List to check if car is a winner or sore loser
            var carList = new List<Task<Car>> { volvoRace, fordRace, bmwRace };
            var finishedCars = new List<Car>();

            Car isWinner = null;
            bool isWinnerFound = false;

            // A while loop to determine who crosses the finish line first
            while (carList.Count > 0)
            {
                var finishedRace = await Task.WhenAny(carList);
                finishedCars.Add(finishedRace.Result);
                carList.Remove(finishedRace);
            }

            foreach (var car in finishedCars)
            {
                if (car != null && car.Distance >= 10 && !isWinnerFound)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    isWinner = car;
                    car.IsWinner++;
                    car.RaceCompleted++;
                    isWinnerFound = true;
                    Console.WriteLine("*********************************************************");
                    Console.WriteLine($"*** {isWinner.CarName} is the WINNER! Congratulations! ***");
                    Console.WriteLine("*********************************************************");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (car.IsWinner == 0 && car.RaceCompleted == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    car.RaceCompleted++;
                    Console.WriteLine($"*** {car.CarName} lost the race. Better luck next time! ***");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("------------------------------------------------------------------------");
            Console.WriteLine($"   ---The simulation has ended and {isWinner.CarName} is the WINNER! Horaaay!---");
            Console.WriteLine("------------------------------------------------------------------------");
        }
        // Method to simulate the race, using a stopwatch to keep track
        public static async Task<Car> Race(Car car)
        {
            // Initialize a stopwatch to measure race time
            Stopwatch raceTimer = new Stopwatch();
            raceTimer.Start();

            // Set the race parameters
            bool isRaceActive = true;
            int raceGoalDistanceInKilometers = 10;

            while (isRaceActive)
            {
                // Calculate the current race duration in tenths of a second
                car.RaceDuration = raceTimer.Elapsed * 10;

                // Calculate the car's speed in meters per second
                double speedInMetersPerSecond = car.Speed / 3600.0;

                // Calculate the distance the car would travel in the next 30 seconds
                double distanceTraveledInKilometers = speedInMetersPerSecond * 30;

                // Calculate the remaining distance to the goal in kilometers
                double remainingDistanceToGoal = (raceGoalDistanceInKilometers - car.Distance) / speedInMetersPerSecond;

                // Check if the car is still racing
                if (car.Distance < raceGoalDistanceInKilometers)
                {
                    // Check if the car will reach or exceed the goal distance in the next 30 seconds
                    if (car.Distance + distanceTraveledInKilometers >= raceGoalDistanceInKilometers)
                    {
                        // Calculate the time needed to cover the remaining distance
                        double timeToFinish = remainingDistanceToGoal;

                        // Wait for the remaining time to simulate the car crossing the finish line
                        await Wait(timeToFinish);

                        // Stop the race stopwatch
                        raceTimer.Stop();

                        // Update race statistics
                        car.RaceDuration = raceTimer.Elapsed * 10;
                        car.Distance += remainingDistanceToGoal * speedInMetersPerSecond;
                        UpdateRaceResults(car);

                        // Mark the race as completed
                        isRaceActive = false;
                    }
                    else
                    {
                        // Simulate a random event during the race
                        await ApplyRandomEvent(car);
                    }
                }
            }
            return car;
        }

        // Method to apply random events happening during the race
        public static async Task ApplyRandomEvent(Car car)
        {
            int randomEventNumber = random.Next(50);

            // Simulate a random event wait time
            await Wait(30);

            switch (randomEventNumber)
            {
                case 0:
                    Console.WriteLine($"{car.CarName} ran out of gas! Refueling...");
                    await Wait(30);
                    break;

                case int n when (n > 0 && n <= 2):
                    Console.WriteLine($"{car.CarName} got a flat tire and needs to change it.");
                    await Wait(20);
                    break;

                case int n when (n > 2 && n <= 7):
                    Console.WriteLine($"{car.CarName} hit a bird, and it splattered on the windshield.");
                    await Wait(10);
                    break;

                case int n when (n > 7 && n <= 17):
                    Console.WriteLine($"{car.CarName} experienced engine problems, and its speed decreased by 1 km/h.");
                    car.Speed--;
                    double distanceTraveledWithEngineProblem = CarSpeedUpdate(car);
                    car.Distance += distanceTraveledWithEngineProblem;
                    break;

                default:
                    double distanceTraveled = CarSpeedUpdate(car);
                    car.Distance += distanceTraveled;
                    break;
            }
        }
        // Method to update race results
        public static void UpdateRaceResults(Car car)
        {
            string raceTimeFormatted = string.Format(@"{0:mm\:ss\:ff}", car.RaceDuration);
            Console.WriteLine($"\n{car.CarName} successfully crossed the finish line at a speed of {car.Speed} km/h. {car.CarName} completed the race in {raceTimeFormatted}.");
        }


        // Method to monitor and display race status
        public static async Task MonitorRaceStatus(List<Car> cars)
        {
            while (true)
            {
                await Wait(10);

                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    DisplayRaceStatus(cars);
                }

                var totalCompletedRaces = cars.Sum(car => car.RaceCompleted);
                if (totalCompletedRaces >= 3)
                {
                    break;
                }
            }
        }
        // Method to display race status
        public static void DisplayRaceStatus(List<Car> cars)
        {

            foreach (var car in cars)
            {
                ConsoleColor statusColor = car.RaceCompleted > 0 ? ConsoleColor.Red : ConsoleColor.Yellow;
                ConsoleColor infoColor = ConsoleColor.Blue;

                Console.ForegroundColor = statusColor;
                Console.WriteLine($"--Status of {car.CarName} ");
                Console.ForegroundColor = infoColor;
                Console.WriteLine($"Distance traveled: {Math.Truncate(car.Distance * 100) / 100} km Speed: {car.Speed} km/h --");
                Console.WriteLine("---------------------------------------------");
            }


            Console.ForegroundColor = ConsoleColor.White; // Reset text color to white.
        }

        private static Random random = new Random();

        // Method to simulate a delay
        public async static Task Wait(double delay = 30)
        {
            await Task.Delay(TimeSpan.FromSeconds(delay / 10));
        }

        // Method to update car speed during the race
        public static double CarSpeedUpdate(Car car)
        {
            double distancePerSecond = car.Speed / 3600.0;
            double distanceTraveld = distancePerSecond * 30;
            return distanceTraveld;
        }
    }
}