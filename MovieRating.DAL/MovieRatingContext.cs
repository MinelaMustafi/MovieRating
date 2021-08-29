using Microsoft.EntityFrameworkCore;
using MovieRating.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MovieRating.DAL
{
    public class MovieRatingContext : DbContext
    {
        protected string _conStr;

        public MovieRatingContext() : base()
        {
            _conStr = "Server=localhost;Database=movierating;Trusted_Connection=True;";
        }

        public MovieRatingContext(DbContextOptions<MovieRatingContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (_conStr != null)
            {
                builder.UseSqlServer(_conStr);
            }
            builder.UseLazyLoadingProxies(true);
            base.OnConfiguring(builder);
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Movie>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<Rating>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<User>().HasQueryFilter(x => !x.Deleted);
        }


        public override async Task<int> SaveChangesAsync(CancellationToken token = default)
        {
            try
            {
                foreach (var entry in ChangeTracker.Entries().Where(
                    x => x.State == EntityState.Deleted && x.Entity is BaseClass))
                {
                    entry.State = EntityState.Modified;
                    entry.CurrentValues["Deleted"] = true;
                }
                return await base.SaveChangesAsync(token);
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}
