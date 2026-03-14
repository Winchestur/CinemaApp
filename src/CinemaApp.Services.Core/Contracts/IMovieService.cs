using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaApp.Web.ViewModels.Movie;

namespace CinemaApp.Services.Core.Contracts
{
    public interface IMovieService
    {
        Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMoviesOrderedByTitleAsync();
        Task CreateMovieAsync(MovieFormViewModel model);
        Task<MovieDetailsViewModel> GetMovieDetailsByIdAsync(Guid id);

        Task<MovieFormViewModel> GetMovieForEditByIdAsync(Guid id);

        Task<bool> ExistsByIdAsync(Guid id);

        Task EditMovieAsync(Guid id, MovieFormViewModel model);
    }
}
