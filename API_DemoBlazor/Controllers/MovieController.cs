using API_DemoBlazor.Models;
using API_DemoBlazor.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_DemoBlazor.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieService _movieService;
        public MovieController(MovieService movieService)
        {
            _movieService = movieService;
        }

        [Authorize("connectedPolicy")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_movieService.GetAll());
        }

        [Authorize("adminPolicy")]
        [HttpPost]
        public IActionResult Post([FromBody] Movie m)
        {
            _movieService.Add(m);
            return Ok();
        }
    }
}
