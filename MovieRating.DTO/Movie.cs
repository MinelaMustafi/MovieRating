using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MovieRating.DTO
{
    public class Movie : BaseClass
    {
        public Movie()
        {
            Ratings = new List<Rating>();
        }
        public string PhotoUrl { get; set; }

        [Description("Required")]
        public string Title { get; set; }
        [Description("Required")]
        public string Description { get; set; }
        [Description("Required")]
        public Type Type { get; set; }
        [Description("Required")]
        public DateTime Released { get; set; }
        [Description("Required")]
        public string Cast { get; set; }
        public virtual IList<Rating> Ratings { get; set; }
    }
}
