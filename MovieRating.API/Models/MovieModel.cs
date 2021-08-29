using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRating.API.Models
{
    public class MovieModel
    {
        public MovieModel()
        {
            Ratings = new List<MasterModel>();
        }
        public int Id { get; set; }
        public string PhotoUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal AverageRating { get; set; }
        public string Type { get; set; }
        public DateTime Released { get; set; }
        public List<string> Cast { get; set; }
        public IList<MasterModel> Ratings { get; set; }
    }
}
