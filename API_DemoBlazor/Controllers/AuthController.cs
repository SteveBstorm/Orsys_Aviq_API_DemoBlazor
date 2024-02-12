using API_DemoBlazor.Models;
using API_DemoBlazor.Services;
using API_DemoBlazor.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_DemoBlazor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _service;
        private readonly JwtGenerator _jwt;

        public AuthController(UserService service, JwtGenerator jwt)
        {
            _service = service;
            _jwt = jwt;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User u)
        {
            _service.Register(u);
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginForm form)
        {
            if(!ModelState.IsValid) 
                return BadRequest();

            User? connectedUser = _service.Login(form.Email, form.Password);
            if (connectedUser == null) return BadRequest("Connexion pas bien");
            return Ok(_jwt.GenerateToken(connectedUser));
        }

        [Authorize("connectedPolicy")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetUsers());
        }
    }
}
