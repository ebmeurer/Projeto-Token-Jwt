using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using WebApplication1.Entities;
using WebApplication1.Services;
using WebApplication1.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private TokenService _TokenService;

        private DataContext _dataContext;



        public AuthController(TokenService TokenService, DataContext context)
        {
          _TokenService = TokenService;
          this._dataContext = context;
        }

        [HttpPost]

        public ActionResult<string> getToken([FromBody] User request)
        {

            if (_TokenService.ValidateLogin(request, _dataContext)){
                var Token = _TokenService.GenerateToken(request);

                string json = JsonConvert.SerializeObject(new
                {
                    data = new JwtTokenReturn {Token = Token}
                });

                var jsonElement = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(json);

                return Ok(jsonElement);   
            } else {
                return NotFound();
            }

        }

        [HttpGet]
        [Authorize]

        public ActionResult Privateinfos()
        {
            return Ok("infos privadas");
        }
    }   
}
