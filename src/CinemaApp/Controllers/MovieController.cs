using CinemaApp.Data;
using Microsoft.AspNetCore.Mvc;
using CinemaApp.Web.ViewModels.Movie;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using CinemaApp.Services.Core.Contracts;
using CinemaApp.Services.Core;

namespace CinemaApp.Web.Controllers
{
    public class MovieController : BaseController
    {
        private readonly IMovieService movieService;
        public MovieController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AllMoviesIndexViewModel> movies = 
                await movieService.GetAllMoviesOrderedByTitleAsync();

            return View(movies);
        }
    }
}
