using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRating.API.Models
{
    public class UserModel
    {
        public UserModel()
        {
            Ratings = new List<MasterModel>();
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public IList<MasterModel> Ratings { get; set; }
    }
}
