using Bibliosys;
using Bibliosys.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BiblioSys.Controllers
{
    public class LibraryController : Controller
    {
        public readonly DatabaseContext db;

        public LibraryController(DatabaseContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            var books = await db.Books.Include(b => b.Author).ToListAsync();
            return View(books);
        }
        public IActionResult AddUser() 
        {      
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(user);
        }
    }
}