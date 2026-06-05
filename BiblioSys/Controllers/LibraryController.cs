using Bibliosys;
using Bibliosys.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

        [Authorize]
        public async Task<IActionResult> MyReservations()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var reservations = await db.Reservations
                .Where(r => r.UserId == userId)
                .Include(r => r.Book)
                .ThenInclude(b => b.Author)
                .ToListAsync();
            return View(reservations);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Reservations()
        {
            ViewBag.BookId = new SelectList(db.Books.Where(b => b.IsFree), "Id", "Title");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View(new Reservation());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Reservations(Reservation reservation)
        {
            reservation.ReservationDate = DateTime.Now;
            reservation.Status = ReservationStatus.Aktywna;
            ModelState.Remove("Book");
            ModelState.Remove("User");

            if (ModelState.IsValid)
            {
                var book = await db.Books.FindAsync(reservation.BookId);
                if (book == null || !book.IsFree)
                {
                    ModelState.AddModelError("", "Książka jest niedostępna.");
                    ViewBag.BookId = new SelectList(db.Books.Where(b => b.IsFree), "Id", "Title");
                    ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
                    return View(reservation);
                }
                book.IsFree = false;
                db.Reservations.Add(reservation);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BookId = new SelectList(db.Books.Where(b => b.IsFree), "Id", "Title");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View(reservation);
        }

        public IActionResult AddUser() => View();

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

        [Authorize(Roles = "Admin")]
        public IActionResult AddBook()
        {
            ViewBag.Authors = db.Authors.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddBook(Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Authors = await db.Authors.ToListAsync();
            return View(book);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddAuthor() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
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

        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user == null)
            {
                ViewBag.Error = "Nieprawidłowy adres e-mail lub hasło.";
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Email, user.Email),
            };

            if (user.IsAdmin)
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = false,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminPanel()
        {
            var reservations = await db.Reservations
                .Include(r => r.Book)
                    .ThenInclude(b => b.Author)
                .Include(r => r.User)
                // don't show reservations that already have a return date
                .Where(r => r.ReturnDate == null)
                .ToListAsync();

            var users = await db.Users.ToListAsync();
            ViewBag.Users = users;
            return View(reservations);
        }
    }
}