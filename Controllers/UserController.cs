using Personalized_Library_System.Models;
using Personalized_Library_System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Personalized_Library_System.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class UserController : ControllerBase
{
    private readonly AppDbContext context;

    public UserController(AppDbContext context)
    {
        this.context = context;
    }

    [HttpGet]
    // This Returns the list of all users in the database
    public async Task<ActionResult<IEnumerable<User>>> Get() =>
        await context.User.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> Get(int id)
    {
        // This Returns a User with the specified ID {id}
        var user = await context.User.FindAsync(id);

        if (user is null)
            return NotFound();

        return user;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] User user)
    {
        // this validates the user request to avoid duplicate entries
        // before creating a new user in the database
        if (user.Password == null)
            return BadRequest("Password Cannot Be Empty");
        
        var userEmail = await context.User.FirstOrDefaultAsync(u => u.Email == user.Email);
        if (user.Email == userEmail?.Email)
            return BadRequest("User Already Exists");

        user.Password = HashPassword(user.Password); 
        context.User.Add(user);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromForm] int id)
    {
        // This Updates a specific User object using the {id}
        // to retrieve the User and update the user type.
        if (id == 0)
            return BadRequest();

        var existingUser = await context.User.FindAsync(id);
        if (existingUser is null)
            return NotFound();
        
        context.Entry(existingUser).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (existingUser is null)
            {
                return NotFound();
            }
            else
                throw;
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await context.User.FindAsync(id);

        if (user == null)
            return NotFound();

        context.User.Remove(user);
        await context.SaveChangesAsync();

        return Ok();
    }

    private static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

}