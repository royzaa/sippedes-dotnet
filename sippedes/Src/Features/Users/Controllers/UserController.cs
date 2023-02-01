using Microsoft.AspNetCore.Mvc;
using sippedes.Cores.Controller;
using sippedes.Cores.Dto;
using sippedes.Cores.Entities;
using sippedes.Src.Features.Users.Services;

namespace sippedes.Src.Features.Users.Controllers
{
    [Route("api/users")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewUser([FromBody] User request)
        {
            var userResponse = await _userService.Create(request);
            return Created("/api/users", Success(userResponse));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUser([FromQuery] string? name, [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var result = await _userService.GetAll(name, page, size);
            return Success(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetById(id);
            return Success(user);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            var userUpdate = await _userService.Update(user);
            return Success(userUpdate);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteById(string id)
        {
            var  result = _userService.DeleteById(id);
            return Success(result);
            
        }
        
    }
}
