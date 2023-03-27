using Microsoft.EntityFrameworkCore;
using System.Text;
using WebPortfolioMVC.Models;

namespace WebPortfolioMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<BlogContext>(options => 
            options.UseSqlite(builder.Configuration.GetConnectionString("BlogContext")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            //SeedDataToDb(app);

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        /// <summary>
        /// Заполнить БД данными для теста
        /// </summary>
        /// <param name="app"></param>
        private static void SeedDataToDb(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<BlogContext>();

                context.Database.Migrate();
                SeedData(context);
            }
        }

        private static void SeedData(BlogContext context)
        {
            // Add some sample users
            var users = new List<User>
            {
            new User { FirstName = "John", LastName = "Doe", Email = "john@example.com", PasswordHash = "password", PasswordSalt = Encoding.UTF8.GetBytes("salt"), Posts = new List<Post>() },
            new User { FirstName = "Jane", LastName = "Doening", Email = "jane@example.com", PasswordHash = "password", PasswordSalt = Encoding.UTF8.GetBytes("salt"), Posts = new List<Post>() }
            };
            context.Users.AddRange(users);
            context.SaveChanges();

            // Add some sample tags

            var tag1 = new Tag { Name = "Technology", PostTags = new List<PostTag>() };
            var tag2 = new Tag { Name = "Sports", PostTags = new List<PostTag>() };
            var tag3 = new Tag { Name = "Entertainment", PostTags = new List<PostTag>() };


            var tags = new List<Tag> {};
            tags.Add(tag1);
            tags.Add(tag2);
            tags.Add(tag3);

            context.Tags.AddRange(tags);
            context.SaveChanges();

            // Add some sample posts

            var janeId = context.Users.Where(u => u.FirstName == "Jane").FirstOrDefault().Id;
            var jhonId = context.Users.Where(u => u.FirstName == "John").FirstOrDefault().Id;

            var tagsDb = context.Tags.ToList();

            var posts = new List<Post>
            {
                new Post { Title = "My First Post", Content = "This is my first blog post.", UserId = jhonId, CreatedAt = DateTime.Now, Views = 0, PostTags = new List<PostTag> { new PostTag { TagId = tagsDb.First(t => t.Id == 1).Id } } },
                new Post { Title = "My Second Post", Content = "This is my second blog post.", UserId = janeId, CreatedAt = DateTime.Now.AddDays(-1), Views = 0, PostTags = new List<PostTag> { new PostTag { TagId = tagsDb.First(t => t.Id == 2).Id }, new PostTag { TagId = tagsDb.First(t => t.Id == 3).Id } } },
                new Post { Title = "My Third Post", Content = "This is my third blog post.", UserId = jhonId, CreatedAt = DateTime.Now.AddDays(-2), Views = 0, PostTags = new List<PostTag> { new PostTag { TagId = tagsDb.First(t => t.Id == 1).Id }, new PostTag { TagId = tagsDb.First(t => t.Id == 3).Id } } }
            };
            context.Posts.AddRange(posts);
            context.SaveChanges();
        }
    }
}