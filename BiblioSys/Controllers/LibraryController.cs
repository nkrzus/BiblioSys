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

        public async Task<IActionResult> MyReservations()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }
            var reservations = await db.Reservations
                .Where(r => r.UserId == userId)
                .Include(r => r.Book)
                .ThenInclude(b => b.Author)
                .ToListAsync();
            return View(reservations);
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
        public ActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            var user = db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user == null)
            {
                ViewBag.Error = "Nieprawidłowy adres e-mail lub hasło.";
                return View();
            }
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetInt32("IsAdmin", user.IsAdmin ? 1 : 0);
            return RedirectToAction("Index");
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