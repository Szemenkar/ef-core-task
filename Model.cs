namespace entity_framework_task;
using Microsoft.EntityFrameworkCore;

public class BloggingContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    public string DbPath { get; }

    public BloggingContext()
    {
        var folder = Environment.CurrentDirectory;
        DbPath = System.IO.Path.Join(folder, "cs.forts-Johannes-Szemenkar.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

public class User
{
    public int UserId { get; set; }
    public string? Username { get; set; }
    public string? Password { private get; set; }

    public List<Post> Posts { get; } = new();
}

public class Blog
{
    public int BlogId { get; set; }
    public string? Url { get; set; }
    public string? Name { get; set; }

    public List<Post> Posts { get; } = new();
}

public class Post
{
    public int PostId { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public DateOnly PublishedOn { get; set; }

    public int BlogId { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public Blog? Blog { get; set; }
}