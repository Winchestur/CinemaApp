using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaApp.Data;
using CinemaApp.Services.Core.Contracts;
using CinemaApp.Web.ViewModels.Movie;
using Microsoft.EntityFrameworkCore;
using static CinemaApp.GCommon.ApplicationConstants;

namespace CinemaApp.Services.Core
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext dbContext;
        public MovieService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMoviesOrderedByTitleAsync()
        {
            IEnumerable<AllMoviesIndexViewModel> Movies = await dbContext.Movies
                .AsNoTracking()
                .Select(m => new AllMoviesIndexViewModel()
                {
                    Id = m.Id.ToString(),
                    Title = m.Title,
                    Genre = m.Genre,
                    ReleaseDate = m.ReleaseDate.ToString(DefaultDateTimeFormat,CultureInfo.InvariantCulture),
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
