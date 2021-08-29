using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRating.DTO
{
    public class BaseClass
    {
        public BaseClass()
        {
            Creator = CurrentUser.Id;
            Deleted = false;
            Created = DateTime.Now;
        }
        public int Id { get; set; }
        public int Creator { get; set; }
        public DateTime Created { get; set; }
        public bool Deleted { get; set; }
    }
}
