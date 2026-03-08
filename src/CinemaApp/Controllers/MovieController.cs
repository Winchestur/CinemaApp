using CinemaApp.Data;
using Microsoft.AspNetCore.Mvc;
using CinemaApp.Web.ViewModels.Movie;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Web.Controllers
{
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public MovieController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            ICollection<AllMoviesIndexViewModel> movies = await dbContext.Movies.Select(m => new AllMoviesIndexViewModel
            {
                Id = m.Id.ToString(),
                Title = m.Title,
                Genre = m.Genre,
                ReleaseDate = m.ReleaseDate.ToString("dd/MM/yyyy"),
                Director = m.Director,
                ImageUrl = m.ImageUrl
            }).ToListAsync();
            
            return View(movies);
        }
    }
}
