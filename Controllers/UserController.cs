using Microsoft.AspNetCore.Mvc;
using todoonboard_api.InfoModels;
using todoonboard_api.Services;

namespace todoonboard_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("create")]
        public IActionResult createUser(UserRequest userRequest){
            var res = _userService.CreateUser(userRequest);
            if(res == null) return BadRequest(res);
            return Ok(res);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}