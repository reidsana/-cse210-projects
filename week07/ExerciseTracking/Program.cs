using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Activity> activities = new List<Activity>();

        Running run = new Running("03 Nov 2022", 30, 3.0);
        Cycling cycle = new Cycling("04 Nov 2022", 45, 15.0);
        Swimming swim = new Swimming("05 Nov 2022", 25, 20);

        activities.Add(run);
        activities.Add(cycle);
        activities.Add(swim);

        foreach (Activity a in activities)
        {
            Console.WriteLine(a.GetSummary());
        }
    }
}

