using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranscribeVideo.Core.Data;
using TranscribeVideo.Core.DTOs.User;
using TranscribeVideo.Core.Interfaces.Repository;
using TranscribeVideo.Core.Interfaces.Services;

namespace TranscribeVideo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService; 

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto dto)
        {
            return Ok(await _userService.CreateAsync(dto));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(UpdateUserDto dto, int id)
        {
            return Ok(await _userService.UpdateAsync(dto, id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.DeleteAsync(id);

            return result ? Ok() : NotFound();
        }
    }
}
