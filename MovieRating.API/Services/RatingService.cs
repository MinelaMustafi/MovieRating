using MovieRating.DAL;
using MovieRating.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRating.API.Services
{
    public static class RatingService
    {
        public static async Task<Rating> Rate(UnitOfWork unit, int movieId, int starNumber)
        {
            return new Rating
            {
                StarNumber = starNumber,
                Movie = await unit.Movies.Get(movieId),
                User = await unit.Users.Get(CurrentUser.Id)
            };
        }
    }
}
