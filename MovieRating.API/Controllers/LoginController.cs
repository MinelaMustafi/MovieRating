using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieRating.API.Models;
using MovieRating.DAL;
using MovieRating.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRating.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {
        public LoginController(MovieRatingContext context) : base(context) { }

        [AllowAnonymous]
        [HttpPost]
        [Route("/api/login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                User user = await Access.Check(model.Username, model.Password);
                if (user != null)
                {
                    CurrentUser.Id = user.Id;
                    CurrentUser.Username = user.Username;
                    CurrentUser.Token = Access.GetToken(user, model.Remember);
                    return Ok(new { CurrentUser.Id, CurrentUser.Username, CurrentUser.Token });
                }
                throw new ArgumentException($"Error! Bad credentials!");
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("/api/signup")]
        public async Task<IActionResult> Signup([FromBody] User user)
        {
            try
            {
                await Unit.Users.Insert(user);
                await Unit.Save();
                return Ok("User je prijavljen");
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}
