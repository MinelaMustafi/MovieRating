using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MovieRating.DTO
{
    public class Rating : BaseClass
    {
        [Description("Required")]
        public int StarNumber { get; set; }
        [Description("Required")]
        public virtual Movie Movie { get; set; }
        [Description("Required")]
        public virtual User User { get; set; }
    }
}
