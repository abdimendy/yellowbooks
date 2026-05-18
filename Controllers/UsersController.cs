using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YellowBook.API.DTOs;
using YellowBook.API.Interfaces;

namespace YellowBook.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    // GET: api/users
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userRepository.GetAllAsync();
        var userDtos = users.Select(u => new UserDto
        {
            Id = u.Id,
            FullName = u.FullName,
            Username = u.Username,
            Email = u.Email,
            Role = u.Role,
            CreatedAt = u.CreatedAt
        });

        return Ok(userDtos);
    }

    // GET: api/users/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            return NotFound();

        var userDto = new UserDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role,
            CreatedAt = user.CreatedAt
        };

        return Ok(userDto);
    }

    // PUT: api/users/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] RegisterDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = new YellowBook.API.Models.User
        {
            FullName = updateDto.FullName,
            Username = updateDto.Username,
            Email = updateDto.Email,
            Role = updateDto.Role
        };

        var updatedUser = await _userRepository.UpdateAsync(id, user);
        if (updatedUser == null)
            return NotFound();

        var userDto = new UserDto
        {
            Id = updatedUser.Id,
            FullName = updatedUser.FullName,
            Username = updatedUser.Username,
            Email = updatedUser.Email,
            Role = updatedUser.Role,
            CreatedAt = updatedUser.CreatedAt
        };

        return Ok(userDto);
    }

    // DELETE: api/users/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _userRepository.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}