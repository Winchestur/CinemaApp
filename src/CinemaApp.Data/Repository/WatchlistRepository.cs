using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;
using CinemaApp.Web.ViewModels.Watchlist;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Data.Repository
{
    public class WatchlistRepository : IWatchlistRepository
    {
        private readonly ApplicationDbContext dbContext;
        public WatchlistRepository(ApplicationDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        public async Task AddMovieToWatchlistAsync(string userId, Guid movieId)
        {
            var exists = await dbContext.Movies
                .IgnoreQueryFilters()
                .AnyAsync(m => m.Id == movieId);

            if (!exists)
                throw new Exception("Movie not found.");

            var alreadyAdded = await dbContext.UserMovies
                .AnyAsync(um => um.UserId == userId && um.MovieId == movieId);

            if (!alreadyAdded)
            {
                await dbContext.UserMovies.AddAsync(new UserMovie
                {
                    UserId = userId,
                    MovieId = movieId
                });
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<WatchlistViewModel>> GetAllWatchList(string userId)
        {
            return await dbContext.UserMovies
             .Where(um => um.UserId == userId)
             .Include(um => um.Movie) // зареждаме навигацията
             .IgnoreQueryFilters()    // игнорираме IsDeleted филтъра
             .Select(um => new WatchlistViewModel
             {
                 MovieId = um.Movie!.Id.ToString(),
                 Title = um.Movie.Title,
                 Genre = um.Movie.Genre,
                 ImageUrl = um.Movie.ImageUrl,
                 ReleaseDate = um.Movie.ReleaseDate.ToString("yyyy-MM-dd")
             })
             .ToListAsync();
        }

        public async Task<bool> GetMovieInWatchlistAsync(string userId, Guid movieId)
        {
            return await dbContext
                .UserMovies
                .AnyAsync(um => um.UserId == userId && um.MovieId == movieId);
        }

        public async Task RemoveMovieFromWatchlistAsync(string userId, Guid movieId)
        {
            var userMovie = await dbContext.UserMovies
                .FirstOrDefaultAsync(um => um.UserId == userId && um.MovieId == movieId);

            if (userMovie != null)
            {
                dbContext.UserMovies.Remove(userMovie);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
