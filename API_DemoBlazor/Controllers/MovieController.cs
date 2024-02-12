using API_DemoBlazor.Hubs;
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
        private readonly MovieHub _movieHub;
        public MovieController(MovieService movieService, MovieHub movieHub)
        {
            _movieService = movieService;
            _movieHub = movieHub;
        }

        //[Authorize("connectedPolicy")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_movieService.GetAll());
        }

        [Authorize("adminPolicy")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Movie m)
        {
            _movieService.Add(m);
            await _movieHub.NewMovie();
            return Ok();
        }
    }
}
