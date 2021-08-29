using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieRating.API.Models;
using MovieRating.API.Services;
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
    public class RatingsController : BaseController
    {
        public RatingsController(MovieRatingContext context) : base(context) { }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var ratings = (await Unit.Ratings.Get()).ToList();
                return Ok(ratings.Select(x => x.Create()).ToList());
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
                var rating = await Unit.Ratings.Get(id);
                return Ok(rating.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Rating rating)
        {
            try
            {
                if (rating.User.Id != CurrentUser.Id) return BadRequest("Invalid user ID");
                await Unit.Ratings.Insert(rating);
                await Unit.Save();
                return Ok(rating.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }


        [HttpPost("rate/{id}/{star}")]
        public async Task<IActionResult> Post(int id, int star)
        {
            try
            {
                Rating rating = await RatingService.Rate(Unit, id, star);
                await Unit.Ratings.Insert(rating);
                await Unit.Save();
                return Ok(rating.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Rating rating, int id)
        {
            try
            {
                if (rating.User.Id != CurrentUser.Id) return BadRequest("Invalid user ID");
                await Unit.Ratings.Update(rating, id);
                await Unit.Save();
                return Ok(rating.Create());
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
                Rating rating = await Unit.Ratings.Get(id);
                if (rating.User.Id != CurrentUser.Id) return BadRequest("Invalid user ID");
                await Unit.Ratings.Delete(id);
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
