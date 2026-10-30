using System;

//To exceed requirements, I added a streak feature that gives the user 500 bonus points for every 7 consecutive days they record an event. The streak is saved and loaded with the goals and score.
class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        manager.Start();
    }
}