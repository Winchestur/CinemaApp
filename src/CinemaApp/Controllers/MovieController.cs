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
    [AllowAnonymous]
    public class MovieController : BaseController
    {
        private readonly IMovieService movieService;
        private readonly ILogger<MovieController> logger;
        public MovieController(IMovieService movieService, ILogger<MovieController> logger)
        {
            this.movieService = movieService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AllMoviesIndexViewModel> movies =
                await movieService.GetAllMoviesOrderedByTitleAsync();

            return View(movies);
        }

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

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            MovieDetailsViewModel? model = await movieService.GetMovieDetailsByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            MovieFormViewModel? model = await movieService.GetMovieForEditByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] Guid id, MovieFormViewModel model)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await movieService.EditMovieAsync(id, model);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, CreateMovieFailureMessage);
                ModelState.AddModelError(string.Empty, CreateMovieFailureMessage);
                return View(model);
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            MovieDetailsViewModel movieDetailsVm = await movieService.GetMovieDetailsByIdAsync(id);

            if (movieDetailsVm == null)
            {
                return NotFound();
            }

            return View(movieDetailsVm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] Guid id, MovieDetailsViewModel deleteDetails)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                await movieService.SoftDeleteMovieAsyAsync(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (DatabaseEntityCreatePersistFailure ex)
            {
                logger.LogError(ex, CreateMovieFailureMessage);
                ModelState.AddModelError(string.Empty, CreateMovieFailureMessage);
                return View(deleteDetails);
            }


            return RedirectToAction(nameof(Index));
        }
    }
}
