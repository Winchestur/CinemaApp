using CinemaApp.Web.ViewModels.Watchlist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Services.Core.Contracts
{
    public interface IWatchlistService
    {
        Task<IEnumerable<WatchlistViewModel>> GetUserWatchListAsync(string userId);
        Task<bool> IsMovieInWatchlistAsync(string userId, Guid movieId);

        Task AddToWatchListAsync(string userId, Guid movieId);

        Task RemoveFromWatchListAsync (string userId, Guid movieId);
    }
}
