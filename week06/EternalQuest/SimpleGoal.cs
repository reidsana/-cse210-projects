using System;

public class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string title, string description, int points) : base(title, description, points)
    {
        _isComplete = false;
    }

    public override int RecordEvent()
    {
        if (_isComplete)
        {
            Console.WriteLine("This goal is already completed.");
            return 0;
        }

        _isComplete = true;
        Console.WriteLine($"Goal completed! You earned {GetPoints()} points.");
        return GetPoints();
    }

    public override string GetDetailsString()
    {
        return $"[{GetStatusString()}] {GetTitle()} ({GetDescription()})";
    }

    protected override string GetStatusString()
    {
        return _isComplete ? "X" : " ";
    }

    public override string GetSaveString()
    {
        
        return base.GetSaveString() + $"|{_isComplete}";
    }

    public static SimpleGoal LoadFromTokens(string[] tokens)
    {
        // tokens expected after splitting: [GoalType, Title, Description, Points, isComplete]
        string title = ParseToken(tokens[1]);
        string description = ParseToken(tokens[2]);
        int points = int.Parse(tokens[3]);
        bool isComplete = bool.Parse(tokens[4]);

        SimpleGoal g = new SimpleGoal(title, description, points);
        if (isComplete)
    {
            
            typeof(SimpleGoal).GetField("_isComplete", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .SetValue(g, true);
        }
        return g;
    }
}

