using MovieRating.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRating.API.Models
{
    public static class MasterFactory
    {
        public static MasterModel Master(this Movie movie)
        {
            return new MasterModel
            {
                Id = movie.Id,
                Name = movie.Title
            };
        }


        public static MasterModel Master(this Rating rating)
        {
            return new MasterModel
            {
                Id = rating.Id,
                Name = rating.StarNumber.ToString()
            };
        }


        public static MasterModel Master(this User user)
        {
            return new MasterModel
            {
                Id = user.Id,
                Name = user.Username
            };
        }
    }
}
