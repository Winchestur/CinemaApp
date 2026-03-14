using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaApp.Data;
using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contract;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Data.Repository
{
    public class MovieRepository : IMovieRepository, IDisposable
    {
        private bool isDisposed = false;
        private readonly ApplicationDbContext dbContext;
        public MovieRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Movie> GetAllMoviesAsNoTracking()
        {
            return dbContext
                .Movies
                .AsNoTracking();
        }

        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            return await dbContext
                .Movies
                .AsNoTracking()
                .OrderBy(m => m.Title)
                .ToArrayAsync();
        }

        public async Task<bool> AddMovieAsync(Movie movie)
        {
            await dbContext.Movies.AddAsync(movie);
            int result = await SaveChangesAsync();
            return result == 1;
        }

        private async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            isDisposed = true;
        }

        public async Task<Movie?> GetMovieByIdAsync(Guid id)
        {
            return await dbContext.Movies
                .FindAsync(id);
        }
    }
}
