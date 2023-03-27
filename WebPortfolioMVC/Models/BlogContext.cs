using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace WebPortfolioMVC.Models
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PostTag>()
                .HasKey(pt => new { pt.PostId, pt.TagId });
        }
    }
    public class User
    {
        public User()
        {
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public ICollection<Post> Posts { get; set; }
    }

    public class Tag
    {
        public Tag()
        {
            PostTags = new HashSet<PostTag>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
    }
    public class PostTag
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int TagId { get; set; }
    }

    public class Post
    {
        public Post()
        {
            PostTags = new HashSet<PostTag>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Views { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
        public int UserId { get; set; }
    }

    public class PrePostDto
    {
        private int WordsInMinute = 90;
        private int IntroLenght = 150;
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Intro { get; set; }
        public int AuthorId { get; set; }
        public string CreatedAt { get; set; }
        public int Views { get; set; }
        public int Comments { get; set; }
        public int MinutsToRead { get; set; } = 1;
        public string[] PostTagsNames { get; set; }
        public string AuthorName { get; set; }

        public PrePostDto Init(Post source, BlogContext context)
        {
            var author = context.Users.Where(u => u.Id == source.UserId).FirstOrDefault();
            Title = source.Title;

            Id = source.Id;
            Content = source.Content;
            AuthorName = author?.FirstName + " " + author?.LastName;

            if ((source.CreatedAt - DateTime.Now).Days > 5)
            {
                CreatedAt = source.CreatedAt.ToString("MM/dd/yyyy");
            }
            else
            {
                CreatedAt = (source.CreatedAt - DateTime.Now).Days + " дней назад";
            }
            
            Views = source.Views;
            Comments = 6;

            if (Content.Length > IntroLenght)
            {
                Intro = Content.Substring(0, IntroLenght) + "...";
            }
            else
            {
                Intro = Content;
            }

            if (source.Content.Length < WordsInMinute)
            {
                MinutsToRead = Content.Length / WordsInMinute;
            }

            PostTagsNames = context.Tags.Where(t => source.PostTags.Select(pt => pt.TagId).Contains(t.Id)).Select(t => t.Name).ToArray();

            return this;
        }
    }
}
