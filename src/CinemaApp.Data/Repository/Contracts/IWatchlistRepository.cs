using CinemaApp.Web.ViewModels.Watchlist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaApp.Data.Models;

namespace CinemaApp.Data.Repository.Contracts
{
    public interface IWatchlistRepository
    {
        Task<IEnumerable<WatchlistViewModel>> GetAllWatchList(string userId);

        Task<bool> GetMovieInWatchlistAsync(string userId, Guid movieId);

        Task AddMovieToWatchlistAsync(string userId, Guid movieId);
        Task RemoveMovieFromWatchlistAsync(string userId, Guid movieId);
    }
}
