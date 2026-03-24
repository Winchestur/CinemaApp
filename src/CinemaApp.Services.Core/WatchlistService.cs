using CinemaApp.Data.Repository.Contracts;
using CinemaApp.Services.Core.Contracts;
using CinemaApp.Web.ViewModels.Watchlist;

namespace CinemaApp.Services.Core
{
    public class WatchlistService : IWatchlistService
    {
        private readonly IWatchlistRepository watchListRepository;

        public WatchlistService(IWatchlistRepository watchListRepository) 
        {
            this.watchListRepository = watchListRepository;
        }

        public async Task AddToWatchListAsync(string userId, Guid movieId)
        {
            await watchListRepository.AddMovieToWatchlistAsync(userId, movieId);
        }

        public  async Task<IEnumerable<WatchlistViewModel>> GetUserWatchListAsync(string userId)
        {
            return await watchListRepository.GetAllWatchList(userId);
        }

        public async Task<bool> IsMovieInWatchlistAsync(string userId, Guid movieId)
        {
            return await watchListRepository.GetMovieInWatchlistAsync(userId, movieId);
        }

        public async Task RemoveFromWatchListAsync(string userId, Guid movieId)
        {
            await watchListRepository.RemoveMovieFromWatchlistAsync(userId, movieId);
        }
    }
}
