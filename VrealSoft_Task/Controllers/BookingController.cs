using Microsoft.AspNetCore.Mvc;
using VrealSoft_Task.Models;
using Microsoft.EntityFrameworkCore;
using VrealSoft_Task.DTO;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;
using System.Security.Claims;

namespace VrealSoft_Task.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly AppDbContext _context;
    public BookingController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBooking([FromBody] BookingDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);   
        }

        if (!DateTime.TryParseExact(dto.StartTime, "HH:mm dd/MM/yyyy", CultureInfo.InvariantCulture,DateTimeStyles.None, out var startTime))
        {
            return BadRequest("StartTime must be in format 'HH:mm dd/MM/yyyy'.");   
        }

        if (!DateTime.TryParseExact(dto.EndTime, "HH:mm dd/MM/yyyy", CultureInfo.InvariantCulture,DateTimeStyles.None, out var endTime))
        {
            return BadRequest("EndTime must be in format 'HH:mm dd/MM/yyyy'.");   
        }

        if (startTime >= endTime)
        {
            return BadRequest("EndTime must be after StartTime.");   
        }
        var roomExists = await _context.Rooms.AnyAsync(r => r.RoomID == dto.RoomId);
        if (!roomExists)
        {
            return BadRequest("RoomId does not exist.");   
        }
        if (await _context.Booking.AnyAsync(b => b.RoomId == dto.RoomId && ((startTime < b.EndTime) && (endTime > b.StartTime))))
        {
            return Conflict("Room is already booked for the selected time.");   
        }

        var bookedBy = User.FindFirstValue(ClaimTypes.Name);
        if (string.IsNullOrEmpty(bookedBy))
        {
            return Unauthorized("User identity not found.");   
        }

        var booking = new Booking
        {
            RoomId = dto.RoomId,
            StartTime = startTime,
            EndTime = endTime,
            BookedBy = bookedBy
        };
        _context.Booking.Add(booking);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetBookings), new { id = booking.BookingId }, booking);
    }

    [HttpGet]
    public async Task<IActionResult> GetBookings()
    {
        var bookings = await _context.Booking.Include(b => b.Room).ToListAsync();
        var result = bookings.Select(b => new
        {
            RoomId = b.RoomId,
            StartTime = b.StartTime.ToString("HH:mm dd/MM/yyyy"),
            EndTime = b.EndTime.ToString("HH:mm dd/MM/yyyy"),
            BookedBy = b.BookedBy
        });
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBooking(int id)
    {
        var booking = await _context.Booking.FindAsync(id);
        if (booking == null)
        {
            return NotFound();   
        }
        _context.Booking.Remove(booking);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}