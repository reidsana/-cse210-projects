using System;

public class ChecklistGoal : Goal
{
    private int _targetCount;
    private int _currentCount;
    private int _bonusPoints;

    public ChecklistGoal(string title, string description, int pointsPerEvent, int targetCount, int bonusPoints)
        : base(title, description, pointsPerEvent)
    {
        _targetCount = targetCount;
        _currentCount = 0;
        _bonusPoints = bonusPoints;
    }

    public override int RecordEvent()
    {
        if (_currentCount >= _targetCount)
        {
            Console.WriteLine("Checklist already completed.");
            return 0;
        }

        _currentCount++;
        int award = GetPoints();
        if (_currentCount == _targetCount)
        {
            // Completed - give bonus
            award += _bonusPoints;
            Console.WriteLine($"Checklist goal complete! You earned {GetPoints()} + bonus {_bonusPoints} = {award} points.");
        }
        else
        {
            Console.WriteLine($"Progress recorded: {_currentCount}/{_targetCount}. You earned {GetPoints()} points.");
        }

        return award;
    }

    public override string GetDetailsString()
    {
        return $"[{GetStatusString()}] {GetTitle()} ({GetDescription()}) Completed {_currentCount}/{_targetCount}";
    }

    protected override string GetStatusString()
    {
        return _currentCount >= _targetCount ? "X" : " ";
    }

    public override string GetSaveString()
    {
        
        return base.GetSaveString() + $"|{_targetCount}|{_currentCount}|{_bonusPoints}";
    }

    public static ChecklistGoal LoadFromTokens(string[] tokens)
    {
        
        string title = ParseToken(tokens[1]);
        string description = ParseToken(tokens[2]);
        int points = int.Parse(tokens[3]);
        int target = int.Parse(tokens[4]);
        int current = int.Parse(tokens[5]);
        int bonus = int.Parse(tokens[6]);

        ChecklistGoal g = new ChecklistGoal(title, description, points, target, bonus);

      
        typeof(ChecklistGoal).GetField("_currentCount", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(g, current);

        return g;
    }
}
