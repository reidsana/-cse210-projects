using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        // Video 1
        Video video1 = new Video("How to Tie a Fly Fishing Knot", "TroutMaster", 320);
        video1.AddComment(new Comment("Oksana", "This helped so much, thank you!"));
        video1.AddComment(new Comment("Jake", "Great tutorial!"));
        video1.AddComment(new Comment("Lily", "So clear and easy to follow."));
        videos.Add(video1);

        // Video 2
        Video video2 = new Video("Mountain Biking Tips for Beginners", "RideSmith", 415);
        video2.AddComment(new Comment("Sam", "Awesome video!"));
        video2.AddComment(new Comment("Mia", "Can’t wait to try these tips."));
        video2.AddComment(new Comment("Chris", "Very motivating!"));
        videos.Add(video2);

        // Video 3
        Video video3 = new Video("Best Sunglasses for Fly Fishing", "OutdoorOptics", 210);
        video3.AddComment(new Comment("Angela", "ChromaPop lenses are amazing!"));
        video3.AddComment(new Comment("Ben", "Thanks for the comparison."));
        video3.AddComment(new Comment("Owen", "Exactly what I needed!"));
        videos.Add(video3);

        // Display all videos
        foreach (Video video in videos)
        {
            video.DisplayInfo();
        }
    }
}

