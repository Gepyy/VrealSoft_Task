using Microsoft.AspNetCore.Mvc;
using VrealSoft_Task.Models;
using Microsoft.EntityFrameworkCore;
using VrealSoft_Task.DTO;
using Microsoft.AspNetCore.Authorization;
namespace VrealSoft_Task.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RoomsController : ControllerBase
{
    private readonly AppDbContext _context;
    public RoomsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoom([FromBody] RoomDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name) || dto.Capacity <= 0)
        {
            return BadRequest("Invalid room data.");   
        }
        var room = new Rooms { Name = dto.Name, Capacity = dto.Capacity };
        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetRooms), new { id = room.RoomID }, room);
    }

    [HttpGet]
    public async Task<IActionResult> GetRooms()
    {
        var rooms = await _context.Rooms.ToListAsync();
        return Ok(rooms);
    }
}