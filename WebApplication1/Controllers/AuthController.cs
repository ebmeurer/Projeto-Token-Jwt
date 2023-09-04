using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Entities;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private TokenService _TokenService;

        public AuthController(TokenService TokenService)
        {
          _TokenService = TokenService;
        }

        [HttpPost]

        public ActionResult<string> getToken(User request)
        {

            var Token = _TokenService.GenerateToken(request);

            return Ok(Token);
        }

        [HttpGet]
        [Authorize]

        public ActionResult Privateinfos()
        {
            return Ok("infos privadas");
        }


    }   
}
