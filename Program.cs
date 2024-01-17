using entity_framework_task;
using Microsoft.EntityFrameworkCore;
using System;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Xml.Linq;

using var db = new BloggingContext();
db.Database.EnsureDeleted();
db.SaveChanges();
db.Database.EnsureCreated();
db.SaveChanges();

Console.WriteLine($"\nDatabase path: {db.DbPath}.");

var readUserCsv = File.ReadAllLines("user.csv");
foreach (var user in readUserCsv)
{
    string[] lineSplit = user.Split(",");
    var existingUser = db.Users.Find(int.Parse(lineSplit[0]));
    if (existingUser != null)
    {
        continue;
    }
    else
    {
        db.Users.Add(new User { UserId = int.Parse(lineSplit[0]), Username = lineSplit[1], Password = lineSplit[2] });
    }
}

db.SaveChanges();

var readBlogCsv = File.ReadAllLines("blog.csv");
foreach (var blog in readBlogCsv)
{
    string[] lineSplit = blog.Split(",");
    var existingBlog = db.Blogs.Find(int.Parse(lineSplit[0]));
    if (existingBlog != null)
    {
        continue;
    }
    else
    {
        db.Blogs.Add(new Blog { BlogId = int.Parse(lineSplit[0]), Url = lineSplit[1], Name = lineSplit[2] });
    }
}

db.SaveChanges();

var readPostCsv = File.ReadAllLines("post.csv");
foreach (var post in readPostCsv)
{
    string[] lineSplit = post.Split(",");
    db.Posts.Add(new Post { PostId = int.Parse(lineSplit[0]), Title = lineSplit[1], Content = lineSplit[2], PublishedOn = DateOnly.Parse(lineSplit[3]), BlogId = int.Parse(lineSplit[4]), UserId = int.Parse(lineSplit[5]) });
}

db.SaveChanges();

Console.WriteLine("\nContent:");
foreach (User user in db.Users.OrderBy(e => e.Username))
{
    Console.WriteLine($"\nUsername: {user.Username}");

    foreach (Post post in user.Posts.OrderBy(e => e.Title))
    {
        Console.WriteLine($"    Title: {post.Title}, Content: {post.Content}, Published: {post.PublishedOn}");
        Console.WriteLine($"        Blog Name: {post.Blog?.Name}, URL: {post.Blog?.Url}");
    }
}