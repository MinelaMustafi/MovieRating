using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRating.API.Models
{
    public class RatingModel
    {
        public int Id { get; set; }
        public int StarNumber { get; set; }
        public MasterModel Movie { get; set; }
        public MasterModel User { get; set; }
    }
}
