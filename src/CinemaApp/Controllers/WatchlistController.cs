using CinemaApp.Services.Core.Contracts;
using CinemaApp.Web.ViewModels.Watchlist;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Controllers
{
    public class WatchlistController : BaseController
    {
        private readonly IWatchlistService watchListService;

        public WatchlistController(IWatchlistService watchListService)
        {
            this.watchListService = watchListService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!isUserAuthenticated())
                return RedirectToAction("Index", "Home");

            string? userId = GetUserId();
            var model = await watchListService.GetUserWatchListAsync(userId!);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(string? movieId)
        {
            if (!isUserAuthenticated())
                return RedirectToAction("Index");

            if (string.IsNullOrWhiteSpace(movieId) || !Guid.TryParse(movieId, out Guid parsedMovieId))
                return BadRequest("Invalid movie ID.");

            string? userId = GetUserId();

            bool isWatchlisted = await watchListService.IsMovieInWatchlistAsync(userId!, parsedMovieId);

            if (!isWatchlisted)
                await watchListService.AddToWatchListAsync(userId!, parsedMovieId);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Remove(string? movieId)
        {
            if (!isUserAuthenticated())
                return RedirectToAction("Index");

            if (string.IsNullOrWhiteSpace(movieId) || !Guid.TryParse(movieId, out Guid parsedMovieId))
                return BadRequest("Invalid movie ID.");

            string? userId = GetUserId();

            bool isWatchlisted = await watchListService.IsMovieInWatchlistAsync(userId!, parsedMovieId);

            if (isWatchlisted)
                await watchListService.RemoveFromWatchListAsync(userId!, parsedMovieId);

            return RedirectToAction(nameof(Index));
        }
    }
}