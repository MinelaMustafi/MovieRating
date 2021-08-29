using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRating.DTO
{
    public static class CurrentUser
    {
        public static int Id { get; set; }
        public static string Username { get; set; }
        public static string Token { get; set; }
    }
}
