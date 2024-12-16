using InveonWeek2.Models.Entities;
using InveonWeek2.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InveonWeek2.Controllers
{
    [Authorize]
    [Route("Book")]
    public class BookController(AppDbContext appDbContext) : Controller
    {
        [Route("")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var BooKList = await appDbContext.Books.ToListAsync();


            return View("Index", BooKList);
        }

        [Route("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {

            var book = await appDbContext.Books.FindAsync(id);


            return View("Details", book);
        }


        [Authorize("Admin")]
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var book = new Book
            {
                Title = "Best Practice",
                Author = "Fatih Çakıroğlu",
                PublicationYear = "2024",
                ISBN = "000000",
                Genre = "Code",
                Publisher = "Fatih Çakıroğlu",
                PageCount = 400,
                Language = "English",
                Summary = "Inveon",
                AvailableCopies = 100
            };

            await appDbContext.AddAsync(book);
            await appDbContext.SaveChangesAsync();

            var bookList = await appDbContext.Books.ToListAsync();
            return RedirectToAction("Index", bookList);

        }

    }
}
