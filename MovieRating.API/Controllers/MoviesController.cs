using Microsoft.AspNetCore.Authorization;
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
    public class MoviesController : BaseController
    {
        public MoviesController(MovieRatingContext context) : base(context) { }


        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationService paginationService, [FromQuery(Name = "filter")] string filter = "")
        {
            try
            {
                var pagination = new PaginationService(paginationService.PageNumber, paginationService.PageSize);
                if (String.IsNullOrEmpty(filter))
                {
                    var query = (await Unit.Movies.Get()).ToList();
                    return Ok(query.Select(x => x.Create()).OrderByDescending(x => x.AverageRating)
                                .Skip((pagination.PageNumber-1)*pagination.PageSize)
                                .Take(pagination.PageSize)
                                .ToList());
                }
                else
                {
                    var query = (await Unit.Movies.Get(x => x.Title.Contains(filter) 
                        || x.Description.Contains(filter) || x.Cast.Contains(filter))).ToList();
                    return Ok(query.Select(x => x.Create()).OrderByDescending(x => x.AverageRating)
                                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                                .Take(pagination.PageSize)
                                .ToList());
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }


        [AllowAnonymous]
        [HttpGet("topmovies")]
        public async Task<IActionResult> GetTopMovies([FromQuery] PaginationService paginationService, [FromQuery(Name = "filter")] string filter = "")
        {
            try
            {
                var pagination = new PaginationService(paginationService.PageNumber, paginationService.PageSize);
                if (String.IsNullOrEmpty(filter))
                {
                    var movies = (await Unit.Movies.Get()).Where(x => x.Type == DTO.Type.Movie).ToList();
                    return Ok(movies.Select(x => x.Create()).OrderByDescending(x => x.AverageRating)
                                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                                .Take(pagination.PageSize)
                                .ToList());
                }
                else
                {
                    var movies = (await Unit.Movies.Get(x => x.Title.Contains(filter)
                        || x.Description.Contains(filter) || x.Cast.Contains(filter)))
                        .Where(x => x.Type == DTO.Type.Movie).ToList();
                    return Ok(movies.Select(x => x.Create()).OrderByDescending(x => x.AverageRating)
                                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                                .Take(pagination.PageSize)
                                .ToList());
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }


        [AllowAnonymous]
        [HttpGet("topshows")]
        public async Task<IActionResult> GetTopShows([FromQuery] PaginationService paginationService, [FromQuery(Name = "filter")] string filter = "")
        {
            try
            {
                var pagination = new PaginationService(paginationService.PageNumber, paginationService.PageSize);
                if (String.IsNullOrEmpty(filter))
                {
                    var shows = (await Unit.Movies.Get()).Where(x => x.Type == DTO.Type.Show).ToList();
                    return Ok(shows.Select(x => x.Create()).OrderByDescending(x => x.AverageRating)
                                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                                .Take(pagination.PageSize)
                                .ToList());
                }
                else
                {
                    var shows = (await Unit.Movies.Get(x => x.Title.Contains(filter)
                        || x.Description.Contains(filter) || x.Cast.Contains(filter)))
                        .Where(x => x.Type == DTO.Type.Show).ToList();
                    return Ok(shows.Select(x => x.Create()).OrderByDescending(x => x.AverageRating)
                                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                                .Take(pagination.PageSize)
                                .ToList());
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }


        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var movie = await Unit.Movies.Get(id);
                return Ok(movie.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Movie movie)
        {
            try
            {
                await Unit.Movies.Insert(movie);
                await Unit.Save();
                return Ok(movie.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Movie movie, int id)
        {
            try
            {
                await Unit.Movies.Update(movie, id);
                await Unit.Save();
                return Ok(movie.Create());
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
                await Unit.Movies.Delete(id);
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
