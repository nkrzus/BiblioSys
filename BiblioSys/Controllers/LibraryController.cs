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

        public IActionResult AddBook()
        {
            var authors = db.Authors.ToList();
            ViewBag.Authors = authors;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBook(Book book)
        {

            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            var authors = await db.Authors.ToListAsync();
            ViewBag.Authors = authors;
            return View(book);
        }

        public IActionResult AddAuthor()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAuthor(Author author)
        {
            if (ModelState.IsValid)
            {
                db.Authors.Add(author);
                await db.SaveChangesAsync();
                return RedirectToAction("AddBook");
            }
            return View(author);
        }
    }
}