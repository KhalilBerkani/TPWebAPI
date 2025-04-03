using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentification.JWT.Controllers
{
    /// <summary>
    /// Contrôleur de test permettant d'accéder à des endpoints publics ou protégés par JWT.
    /// </summary>
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// Endpoint protégé accessible uniquement aux utilisateurs authentifiés via JWT.
        /// </summary>
        /// <returns>Message de confirmation d'accès autorisé.</returns>
        [Authorize]
        [HttpGet("protected")]
        public IActionResult ProtectedEndpoint()
        {
            return Ok("Tu as accédé à un endpoint protégé !");
        }

        /// <summary>
        /// Endpoint public accessible sans authentification.
        /// </summary>
        /// <returns>Message de confirmation d'accès public.</returns>
        [HttpGet("public")]
        public IActionResult PublicEndpoint()
        {
            return Ok("Ceci est un endpoint public.");
        }
    }
}
