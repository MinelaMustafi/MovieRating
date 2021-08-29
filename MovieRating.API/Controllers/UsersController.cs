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
    public class UsersController : BaseController
    {
        public UsersController(MovieRatingContext context) : base(context) { }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var users = (await Unit.Users.Get()).ToList();
                return Ok(users.Select(x => x.Create()).ToList());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var user = await Unit.Users.Get(id);
                return Ok(user.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            try
            {
                if(user.Username.UsernameExists(Unit)) 
                    throw new ArgumentException("User with that username already exists.");

                await Unit.Users.Insert(user);
                await Unit.Save();
                return Ok(user.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] User user, int id)
        {
            try
            {
                if (user.Username.UsernameExists(Unit))
                    throw new ArgumentException("User with that username already exists.");

                await Unit.Users.Update(user, id);
                await Unit.Save();
                return Ok(user.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }


        [Route("password/{id}")]
        [HttpPut]
        public async Task<IActionResult> SetPassword([FromBody] PasswordModel password, int id)
        {
            if (CurrentUser.Id != id) return BadRequest("Invalid user ID");
            try
            {
                User user = await Unit.Users.Get(id);
                if (user.Username.HashWith(password.OldPassword) != user.Password) return BadRequest("Invalid credentials!");
                if (password.NewPassword != password.Confirmation) return BadRequest("Password and confirmation are not the same!");
                await Unit.Users.SetPassword(id, password.NewPassword);
                await Unit.Save();
                return Ok(user.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Unit.Users.Delete(id);
                await Unit.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}
