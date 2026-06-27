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

        public async Task<IActionResult> Index(string search, int page = 1)
        {
            int pageSize = 9;
            var query = db.Books.Include(b => b.Author).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.ToLower();
                query = query.Where(b =>
                    b.Title.ToLower().Contains(s) ||
                    (b.Author != null && (b.Author.FirstName + " " + b.Author.LastName).ToLower().Contains(s)));
            }

            int total = await query.CountAsync();
            var books = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            ViewBag.Search = search;
            ViewBag.Page = page;
            ViewBag.TotalPages = (int)Math.Ceiling(total / (double)pageSize);

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ReturnBook(int id)
        {
            var reservation = await db.Reservations.Include(r => r.Book).FirstOrDefaultAsync(r => r.Id == id);
            if (reservation == null)
                return NotFound();

            reservation.ReturnDate = DateTime.Now;
            reservation.Status = ReservationStatus.Zakonczona;
            if (reservation.Book != null)
                reservation.Book.IsFree = true;

            await db.SaveChangesAsync();
            return RedirectToAction("AdminPanel");
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
            reservation.ReturnDate = DateTime.Now.AddMonths(1);
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
                return RedirectToAction("AdminPanel");
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
        public async Task<IActionResult> AdminPanel(string searchR, string searchU, int pageR = 1, int pageU = 1)
        {
            int pageSize = 10;

            var rQuery = db.Reservations
                .Include(r => r.Book).ThenInclude(b => b.Author)
                .Include(r => r.User)
                .Where(r => r.Status == ReservationStatus.Aktywna)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchR))
            {
                var s = searchR.ToLower();
                rQuery = rQuery.Where(r =>
                    (r.Book != null && r.Book.Title.ToLower().Contains(s)) ||
                    (r.User != null && (r.User.FirstName + " " + r.User.LastName + " " + r.User.Email).ToLower().Contains(s)));
            }

            int totalR = await rQuery.CountAsync();
            var reservations = await rQuery.Skip((pageR - 1) * pageSize).Take(pageSize).ToListAsync();

            var uQuery = db.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchU))
            {
                var s = searchU.ToLower();
                uQuery = uQuery.Where(u =>
                    (u.FirstName + " " + u.LastName).ToLower().Contains(s) ||
                    u.Email.ToLower().Contains(s));
            }

            int totalU = await uQuery.CountAsync();
            var users = await uQuery.Skip((pageU - 1) * pageSize).Take(pageSize).ToListAsync();

            ViewBag.Users = users;
            ViewBag.SearchR = searchR;
            ViewBag.SearchU = searchU;
            ViewBag.PageR = pageR;
            ViewBag.PageU = pageU;
            ViewBag.TotalPagesR = (int)Math.Ceiling(totalR / (double)pageSize);
            ViewBag.TotalPagesU = (int)Math.Ceiling(totalU / (double)pageSize);

            return View(reservations);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ToggleAdminRole(int id)
        {
            var user = await db.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            user.IsAdmin = !user.IsAdmin;
            await db.SaveChangesAsync();
            return RedirectToAction("AdminPanel");
        }
    }
}