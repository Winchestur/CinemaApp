using CinemaApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Data.Repository.Contract
{
    public interface IMovieRepository
    {
        IQueryable<Movie> GetAllMoviesAsNoTracking();

        Task<IEnumerable<Movie>> GetAllMovies();

        Task<bool> AddMovieAsync(Movie movie);
       // Task<int> SaveChangesAsync();
    }
}
