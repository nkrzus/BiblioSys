using Bibliosys;
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
            var books = await db.Books.ToListAsync();
            return View(books);
        }
    }
}
