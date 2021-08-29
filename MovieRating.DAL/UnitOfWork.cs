using MovieRating.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieRating.DAL
{
    public class UnitOfWork : IDisposable
    {
        public MovieRatingContext Context { get; }
        public UnitOfWork(MovieRatingContext context) => Context = context;

        private IRepository<Movie> _movies;
        private IRepository<Rating> _ratings;
        private UserRepository _users;

        public IRepository<Movie> Movies => _movies ?? (_movies = new Repository<Movie>(Context));
        public IRepository<Rating> Ratings => _ratings ?? (_ratings = new Repository<Rating>(Context));
        public UserRepository Users => _users ?? (_users = new UserRepository(Context));


        public async Task<int> Save()
        {
            int x = await Context.SaveChangesAsync();
            return x;
        }

        public void Dispose() => Context.Dispose();
    }
}
