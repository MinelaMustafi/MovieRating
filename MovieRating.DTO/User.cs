using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MovieRating.DTO
{
    public class User : BaseClass
    {
        public User()
        {
            Ratings = new List<Rating>();
        }

        [Description("Required")]
        public string Username { get; set; }
        [Description("Required")]
        public string Password { get; set; }
        public virtual IList<Rating> Ratings { get; set; }
    }
}
