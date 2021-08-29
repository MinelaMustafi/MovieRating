using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieRating.API.Authentication;
using MovieRating.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRating.API.Controllers
{
    [Authorize]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected UnitOfWork Unit;
        protected AccessHandler Access;

        public BaseController(MovieRatingContext context)
        {
            Unit = new UnitOfWork(context);
            Access = new AccessHandler(Unit);
        }


        [NonAction]
        public IActionResult HandleException(Exception ex)
        {
            if (ex is ArgumentException)
            {
                return NotFound(ex.Message);
            }
            return BadRequest(ex.Message);
        }
    }
}
