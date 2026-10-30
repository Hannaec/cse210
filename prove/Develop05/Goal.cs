using System;

// Base class: holds what every goal shares.
public abstract class Goal
{
    private string _name;
    private string _description;
    private int _points;

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    // Protected getters so derived classes can read, but outside code can't change them.
    public string GetName() => _name;
    protected string GetDescription() => _description;
    protected int GetPoints() => _points;

    // Records one event for this goal and returns the points earned.
    public abstract int RecordEvent();

    // True once the goal can't earn more (eternal goals never are).
    public abstract bool IsComplete();

    // Text for the goal list, e.g. "[ ] Read scriptures (Daily study)".
    public virtual string GetDetailsString()
    {
        string checkbox = IsComplete() ? "[X]" : "[ ]";
        return $"{checkbox} {_name} ({_description})";
    }

    // One line for the save file, starting with the type, e.g. "SimpleGoal:name|desc|points|false".
    public abstract string GetStringRepresentation();
}