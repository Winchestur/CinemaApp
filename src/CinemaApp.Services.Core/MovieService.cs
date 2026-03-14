using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaApp.Data;
using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contract;
using CinemaApp.GCommon.Exceptions;
using CinemaApp.Services.Core.Contracts;
using CinemaApp.Web.ViewModels.Movie;
using Microsoft.EntityFrameworkCore;
using static CinemaApp.GCommon.ApplicationConstants;
using static CinemaApp.GCommon.Exceptions.DatabaseEntityCreatePersistFailure;

namespace CinemaApp.Services.Core
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public async Task CreateMovieAsync(MovieFormViewModel model)
        {
            Movie movie = new Movie()
            {
                Title = model.Title,
                Genre = model.Genre,
                ReleaseDate = DateTime.ParseExact(model.ReleaseDate, ReleaseDateFormat, CultureInfo.InvariantCulture),
                Duration = model.Duration,
                Director = model.Director,
                Description = model.Description,
                ImageUrl = model.ImageUrl
            };

            bool successAdd = await movieRepository.AddMovieAsync(movie);
            
            if (!successAdd)
            {
                throw new DatabaseEntityCreatePersistFailure();
            }

        }

        public async Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMoviesOrderedByTitleAsync()
        {
            IEnumerable<AllMoviesIndexViewModel> Movies = await movieRepository
                .GetAllMoviesAsNoTracking()
                .Select(m => new AllMoviesIndexViewModel()
                {
                    Id = m.Id.ToString(),
                    Title = m.Title,
                    Genre = m.Genre,
                    ReleaseDate = m.ReleaseDate.ToString(ReleaseDateFormat,CultureInfo.InvariantCulture),
                    Director = m.Director,
                    Duration = m.Duration,
                    ImageUrl = m.ImageUrl ?? DefaultImageUrl // default image url if the movie doesn't have one
                })
                .OrderBy(m => m.Title)
                .ThenBy(m => m.Director)
                .ToListAsync();

            return Movies;
        }
    }
}
