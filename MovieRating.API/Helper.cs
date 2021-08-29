using MovieRating.DAL;
using MovieRating.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRating.API
{
    public static class Helper
    {
        public static List<string> CastToList(this string cast)
        {
            return cast.Split(',').ToList();
        }


        public static decimal CalculateAverageRating(this List<Rating> ratings)
        {
            if (ratings.Count == 0) return 0M;
            else return Convert.ToDecimal(ratings.Sum(x => x.StarNumber)) / ratings.Count();
        }


        public static bool UsernameExists(this string username, UnitOfWork unit)
        {
            User existingUser = unit.Users.Get(x => x.Username == username).Result.FirstOrDefault();
            if (existingUser != null) return true;
            else return false;
        }
    }
}
