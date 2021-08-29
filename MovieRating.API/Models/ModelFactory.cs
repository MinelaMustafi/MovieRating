using MovieRating.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRating.API.Models
{
    public static class ModelFactory
    {
        public static MovieModel Create(this Movie movie)
        {
            return new MovieModel
            {
                Id = movie.Id,
                PhotoUrl = movie.PhotoUrl,
                Title = movie.Title,
                Description = movie.Description,
                AverageRating = movie.Ratings.ToList().CalculateAverageRating(),
                Type = movie.Type.ToString(),
                Released = movie.Released,
                Cast = movie.Cast.CastToList(),
                Ratings = movie.Ratings.Select(x => x.Master()).ToList()
            };
        }


        public static RatingModel Create(this Rating rating)
        {
            return new RatingModel
            {
                Id = rating.Id,
                StarNumber = rating.StarNumber,
                Movie = rating.Movie.Master(),
                User = rating.User.Master()
            };
        }


        public static UserModel Create(this User user)
        {
            return new UserModel
            {
                Id = user.Id,
                Username = user.Username,
                Password = "",
                Ratings = user.Ratings.Select(x => x.Master()).ToList()
            };
        }
    }
}
