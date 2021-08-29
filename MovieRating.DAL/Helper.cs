using MovieRating.DTO;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MovieRating.DAL
{
    public static class Helper
    {
        public static async Task Create<T>(this T ent, MovieRatingContext context)
        {
            if (typeof(T) == typeof(Rating)) await Build(ent as Rating, context);
        }

        public static async Task Build(Rating rating, MovieRatingContext context)
        {
            rating.Movie = await context.Movies.FindAsync(rating.Movie.Id);
            rating.User = await context.Users.FindAsync(rating.User.Id);
        }


        public static void Update<T>(this T oldEnt, T newEnt)
        {
            if (typeof(T) == typeof(Rating)) UpdateLink(oldEnt as Rating, newEnt as Rating);
        }

        public static void UpdateLink(Rating oldRating, Rating newRating)
        {
            oldRating.Movie = newRating.Movie;
            oldRating.User = newRating.User;
        }


        public static bool CanDelete<T>(this T ent)
        {
            if (typeof(T) == typeof(Movie)) return HasNoChildren(ent as Movie);
            return true;
        }

        public static bool HasNoChildren(Movie movie)
        {
            return movie.Ratings.Count == 0;
        }


        public static string HashWith(this string username, string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(username + password));
            string hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            return hash;
        }
    }
}
