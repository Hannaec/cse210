using System;
using System.IO;
using System.Collections.Generic;

// Owns the goals and the score, and runs the menu.
public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score;
    private int _streak;
    private DateTime _lastRecordedDate = DateTime.MinValue;

    public void Start()
    {
        int choice;
        do
        {
            DisplayPlayerInfo();

            Console.WriteLine("\nMenu Options:");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit");

            Console.Write("Select a choice from the menu: ");

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                choice = 0;
            }
            Console.WriteLine();

            if (choice == 1) //1. Create New Goal
            {
                CreateGoal();
            }

            else if (choice == 2) //2. List Goals
            {
                ListGoalDetails();
            }

            else if (choice == 3) //3. Save Goals
            {
                SaveGoals();
            }

            else if (choice == 4) //4. Load Goals
            {
                LoadGoals();
            }

            else if (choice == 5) //5. Record Event
            {
                RecordEvent();
            }

            else if (choice == 6) //6. Quit
            {
                Console.WriteLine("Thank you for using the program. Goodbye!");
            }

            else
            {
                Console.WriteLine("Invalid choice. Please select a valid option.");
            }
        } while (choice != 6);
    }

    public void DisplayPlayerInfo()
    {
        Console.WriteLine($"\nYou have {_score} points.");
    }

    public void ListGoalNames()
    {
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetName()}");
        }
    }

    public void ListGoalDetails()
    {
        Console.WriteLine("The goals are:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
    }

    public void CreateGoal()
    {
        int choice = 0;
        while (choice < 1 || choice > 3)
        {
            Console.WriteLine("The types of Goals are:");
            Console.WriteLine("1. Simple Goal");
            Console.WriteLine("2. Eternal Goal");
            Console.WriteLine("3. Checklist Goal");
            Console.Write("Which type of goal would you like to create? ");

            // TryParse sets choice to 0 when the input isn't a number
            if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
            {
                Console.WriteLine("Invalid choice. Please select 1, 2, or 3.\n");
                choice = 0;
            }
        }
        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine();
        Console.Write("What is a short description of it? ");
        string description = Console.ReadLine();
        Console.Write("What is the amount of points associated with this goal? ");
        int points = int.Parse(Console.ReadLine());
        if (choice == 1) // Simple Goal
        {
            _goals.Add(new SimpleGoal(name, description, points));
        }
        else if (choice == 2) // Eternal Goal
        {
            _goals.Add(new EternalGoal(name, description, points));
        }
        else if (choice == 3) // Checklist Goal
        {
            Console.Write("How many times does this goal need to be accomplished for a bonus? ");
            int target = int.Parse(Console.ReadLine());
            Console.Write("What is the bonus for accomplishing it that many times? ");
            int bonus = int.Parse(Console.ReadLine());

            _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
        }
    }

    public void RecordEvent()
    {
        Console.WriteLine("The goals are:");
        ListGoalNames();
        Console.Write("Which goal did you accomplish? ");
        int choice;
        if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > _goals.Count)
        {
            Console.WriteLine("Invalid choice. Please select an available goal.\n");
            choice = 0;
        }
        else
        {
            Goal selectedGoal = _goals[choice - 1];
            int pointsEarned = selectedGoal.RecordEvent();
            _score += pointsEarned;
            Console.WriteLine($"Congratulations! You have earned {pointsEarned} points!");
            _score += UpdateStreak();
            
        }

    }

    private int UpdateStreak()
    {
        DateTime today = DateTime.Today;

        if (_lastRecordedDate == today)
        {
            return 0; // already counted today
        }

        if (_lastRecordedDate == today.AddDays(-1))
        {
            _streak++;
        }
        else
        {
            _streak = 1; // missed a day, or first ever event
        }

        _lastRecordedDate = today;

        if (_streak % 7 == 0)
        {
            Console.WriteLine($"{_streak}-day streak! You get 500 bonus points!");
            return 500;
        }

        return 0;
    }

    public void SaveGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine();
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(filename))
        {
            file.WriteLine(_score);
            file.WriteLine($"{_streak}|{_lastRecordedDate:yyyy-MM-dd}");
            foreach (Goal goal in _goals)
            {
                file.WriteLine(goal.GetStringRepresentation());
            }
        }
    }

    public void LoadGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine();
        using (System.IO.StreamReader file = new System.IO.StreamReader(filename))
        {
            _score = int.Parse(file.ReadLine());
            string[] streakParts = file.ReadLine().Split('|');
            _streak = int.Parse(streakParts[0]);
            _lastRecordedDate = DateTime.Parse(streakParts[1]);
            _goals.Clear();
            string line;
            while ((line = file.ReadLine()) != null)
            {
                string[] parts = line.Split(':', 2);
                string type = parts[0];
                string[] details = parts[1].Split('|');

                if (type == "SimpleGoal")
                {
                    string name = details[0];
                    string description = details[1];
                    int points = int.Parse(details[2]);
                    bool isComplete = bool.Parse(details[3]);
                    _goals.Add(new SimpleGoal(name, description, points, isComplete));
                }
                else if (type == "EternalGoal")
                {
                    string name = details[0];
                    string description = details[1];
                    int points = int.Parse(details[2]);
                    _goals.Add(new EternalGoal(name, description, points));
                }
                else if (type == "ChecklistGoal")
                {
                    string name = details[0];
                    string description = details[1];
                    int points = int.Parse(details[2]);
                    int bonus = int.Parse(details[3]);
                    int target = int.Parse(details[4]);
                    int amountCompleted = int.Parse(details[5]);
                    _goals.Add(new ChecklistGoal(name, description, points, target, bonus, amountCompleted));
                }
            }
        }
    }
}