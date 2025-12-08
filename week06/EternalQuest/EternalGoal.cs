using System;

public class EternalGoal : Goal
{
    public EternalGoal(string title, string description, int points) : base(title, description, points)
    {
    }

    // Eternal goals are never completed; each record awards points.
    public override int RecordEvent()
    {
        Console.WriteLine($"Recorded eternal goal. You earned {GetPoints()} points.");
        return GetPoints();
    }

    public override string GetSaveString()
    {
        // Format: EternalGoal|Title|Description|Points
        return base.GetSaveString();
    }

    public static EternalGoal LoadFromTokens(string[] tokens)
    {
        string title = ParseToken(tokens[1]);
        string description = ParseToken(tokens[2]);
        int points = int.Parse(tokens[3]);
        return new EternalGoal(title, description, points);
    }
}
