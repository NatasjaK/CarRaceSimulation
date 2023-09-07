public class Car
{
    public int Id { get; set; }
    public string CarName { get; set; }
    public double Speed { get; set; }
    public double Distance { get; set; }
    public TimeSpan? RaceDuration { get; set; }
    public int IsWinner { get; set; }
    public int RaceCompleted { get; set; }

    public Car(int id, string carName, decimal distance, TimeSpan? raceDuration, int speed, int isWinner, int raceCompleted)
    {
        Id = id;
        CarName = carName;
        Distance = Distance;
        Speed = speed;
        RaceDuration = raceDuration;
        IsWinner = isWinner;
        RaceCompleted = raceCompleted;
    }
}