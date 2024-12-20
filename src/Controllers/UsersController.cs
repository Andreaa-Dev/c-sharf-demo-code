using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using user.src.Services.user;
using user.src.Utils;
using static user.src.DTO.UserDTO;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;



namespace user.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        protected readonly IUserService _userService;

        public UsersController(IUserService service)
        {
            _userService = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetAllAsync()
        {
            var userList = await _userService.GetAllAsync();
            return Ok(userList);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserReadDto>> GetByIdAsync([FromRoute] Guid id)
        {
            var User = await _userService.GetByIdAsync(id);
            return Ok(User);
        }

        [HttpPatch("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<UserReadDto>> UpdateOneAsync([FromRoute] Guid id, UserUpdateDto updateDto)
        {
            var userUpdated = await _userService.UpdateOneAsync(id, updateDto);
            return Ok(userUpdated);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> DeleteOneAsync([FromRoute] Guid id)
        {
            var isDeleted = await _userService.DeleteOneASync(id);
            return Ok(isDeleted);
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserReadDto>> RegisterUser([FromBody] UserCreateDto userCreateDto)
        {
            var user = await _userService.CreateOneAsync(userCreateDto);
            return Ok(user);
        }


        [HttpPost("signIn")]
        public async Task<ActionResult<string>> SignInUser([FromBody] UserSignInDto userSignInDto)
        {
            var token = await _userService.SignInAsync(userSignInDto);
            System.Console.WriteLine($"token:{token}");
            return Ok(token);
        }

        [HttpPost("create-admin")]
        public async Task<ActionResult<UserReadDto>> CreateAdminAsync([FromBody] UserCreateDto userCreateDto)
        {
            var user = await _userService.CreateAdminAsync(userCreateDto);
            return Ok(user);
        }

        [HttpGet("auth")]
        [Authorize]
        public async Task<ActionResult<UserReadDto>> CheckAuthAsync()
        {
            var authenticatedClaims = HttpContext.User;
            var userId = authenticatedClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var userGuid = new Guid(userId);
            var user = await _userService.GetByIdAsync(userGuid);
            return Ok(user);
        }

        [Authorize]
        [HttpPatch("make-admin/{id:guid}")]
        public async Task<ActionResult<UserReadDto>> UpdateAdminAsync([FromRoute] Guid id)
        {
            var user = await _userService.UpdateAdminAsync(id);
            return Ok(user);
        }


    }


}