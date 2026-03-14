using CinemaApp.Data;
using Microsoft.AspNetCore.Mvc;
using CinemaApp.Web.ViewModels.Movie;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using CinemaApp.Services.Core.Contracts;
using CinemaApp.Services.Core;
using CinemaApp.GCommon.Exceptions;
using static CinemaApp.GCommon.OutPutMessages;

namespace CinemaApp.Web.Controllers
{
    [Authorize]
    public class MovieController : BaseController
    {
        private readonly IMovieService movieService;
        private readonly ILogger<MovieController> logger;
        public MovieController(IMovieService movieService, ILogger<MovieController> logger)
        {
            this.movieService = movieService;
            this.logger = logger;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AllMoviesIndexViewModel> movies =
                await movieService.GetAllMoviesOrderedByTitleAsync();

            return View(movies);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieFormViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await movieService.CreateMovieAsync(model);
            }
            catch (DatabaseEntityCreatePersistFailure ex)
            {
                logger.LogError(ex, CreateMovieFailureMessage);
                ModelState.AddModelError(string.Empty, CreateMovieFailureMessage);
                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, UnexpectedErrorMessage);
                ModelState.AddModelError(string.Empty, UnexpectedErrorMessage);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            MovieDetailsViewModel? model = await movieService.GetMovieDetailsByIdAsync(id);
                
                if (model == null)
                {
                    return NotFound();
                }

            return View(model);
        }
    }
}
