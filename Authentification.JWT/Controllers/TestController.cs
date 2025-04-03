using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentification.JWT.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [Authorize]
        [HttpGet("protected")]
        public IActionResult ProtectedEndpoint()
        {
            return Ok("✅ Tu as accédé à un endpoint protégé !");
        }

        [HttpGet("public")]
        public IActionResult PublicEndpoint()
        {
            return Ok("🔓 Ceci est un endpoint public.");
        }
    }
}
