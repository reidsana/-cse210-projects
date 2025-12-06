using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create individual shapes (optional testing)
        Square s = new Square("Red", 4);
        Rectangle r = new Rectangle("Blue", 5, 3);
        Circle c = new Circle("Green", 2);

        Console.WriteLine(s.GetColor());
        Console.WriteLine(s.GetArea());
        Console.WriteLine();

        Console.WriteLine(r.GetColor());
        Console.WriteLine(r.GetArea());
        Console.WriteLine();

        Console.WriteLine(c.GetColor());
        Console.WriteLine(c.GetArea());
        Console.WriteLine();

        // Build a list of shapes
        List<Shape> shapes = new List<Shape>();
        shapes.Add(s);
        shapes.Add(r);
        shapes.Add(c);

        Console.WriteLine("List of shapes:");
        foreach (Shape shape in shapes)
        {
            Console.WriteLine($"Color: {shape.GetColor()}  Area: {shape.GetArea()}");
        }
    }
}
