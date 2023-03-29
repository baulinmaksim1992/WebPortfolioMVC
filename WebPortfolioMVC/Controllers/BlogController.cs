using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPortfolioMVC.Models;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogContext _context;

        public BlogController(BlogContext context)
        {
            _context = context;
        }

        // GET: Create User register page

        public async Task<IActionResult> CreateUser()
        {
            return View();
        }

        // GET: Blog
        public async Task<IActionResult> Index()
        {
            var posts = await _context.Posts.ToListAsync();
            var postsDto = new List<PrePostDto>();

            foreach (var post in posts)
            {
                postsDto.Add(new PrePostDto().Init(post, _context));
            }

            return View(postsDto);
        }

        // GET: Blog/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(new PrePostDto().Init(post, _context));
        }

        // GET: Blog/Create
        //[ValidateAntiForgeryToken]
        public IActionResult CreatePost()
        {
            return View();
        }

        // POST: Blog/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost([Bind("Title,Content,AuthorId")] Post post)
        {
            if (ModelState.IsValid)
            {
                var postForCreating = new Post { Title = post.Title, Content = post.Content, UserId = 1, CreatedAt = DateTime.Now, Views = 0, PostTags = new List<PostTag> { new PostTag { TagId = _context.Tags.First(t => t.Id == 1).Id } } };
                _context.Add(postForCreating);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Blog/Edit/5
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Blog/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,AuthorId")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Blog/Delete/5
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
