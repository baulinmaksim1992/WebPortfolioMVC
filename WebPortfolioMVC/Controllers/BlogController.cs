using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public BlogController(BlogContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: Register new user
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email };
                // добавляем пользователя

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }

        // GET: Blog
        public async Task<IActionResult> Index()
        {
            return View();

            //var posts = await _context.Posts.ToListAsync();
            //var postsDto = new List<PrePostDto>();

            //if (posts.Count == 0)
            //{
            //    return View(new List<PrePostDto>());
            //}

            //foreach (var post in posts)
            //{
            //    postsDto.Add(new PrePostDto().Init(post, _context));
            //}

            //return View(postsDto);
        }

        // GET: Blog/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            return RedirectToAction(nameof(Index));
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var post = await _context.Posts
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (post == null)
            //{
            //    return NotFound();
            //}

            //return View(new PrePostDto().Init(post, _context));
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
            //if (ModelState.IsValid)
            //{
            //    var postForCreating = new Post { Title = post.Title, Content = post.Content, User.Identity. = 1, CreatedAt = DateTime.Now, Views = 0 };
            //    _context.Add(postForCreating);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}

            return RedirectToAction(nameof(Index));
        }

        // GET: Blog/Edit/5
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id)
        {
            return RedirectToAction(nameof(Index));
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var post = await _context.Posts.FindAsync(id);
            //if (post == null)
            //{
            //    return NotFound();
            //}
            //return View(post);
        }

        // POST: Blog/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,AuthorId")] Post post)
        {
            return RedirectToAction(nameof(Index));
            //if (id != post.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(post);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!PostExists(post.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(post);
        }

        // GET: Blog/Delete/5
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {

            return RedirectToAction(nameof(Index));
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var post = await _context.Posts
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (post == null)
            //{
            //    return NotFound();
            //}

            //return View(post);
        }

        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            return RedirectToAction(nameof(Index));
            //var post = await _context.Posts.FindAsync(id);
            //_context.Posts.Remove(post);
            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
        }
    }
}
