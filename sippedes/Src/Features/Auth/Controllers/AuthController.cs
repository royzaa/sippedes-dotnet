using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sippedes.Cores.Controller;
using sippedes.Features.Auth.Dto;
using sippedes.Features.Auth.Services;

namespace sippedes.Features.Auth.Controllers
{
    [Route("api/auth")]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register-admin")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterRequest request)
        {
            var user = await _authService.RegisterAdmin(request);

            return Success(user);
        }

        [HttpPost("register-civilian")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterCivilian([FromBody] RegisterCivilianRequest request)
        {
            var user = await _authService.RegisterCivilian(request);

            return Success(user);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var login = await _authService.Login(request);
            return Success(login);
        }
    }
}
