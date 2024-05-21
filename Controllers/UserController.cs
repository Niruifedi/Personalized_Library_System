using Personalized_Library_System.Models;
using Personalized_Library_System.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Personalized_Library_System.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    public UserController()
    {
    }

    [HttpGet]
    // This Returns the list of all users in the database
    public ActionResult<List<User>> Get() =>
        UserService.GetAll();

    [HttpGet("{id}")]
    public ActionResult<User> Get(int id)
    {
        // This Returns a User with the specified ID {id}
        var user = UserService.Get(id);

        if (user is null)
            return NotFound();

        return user;
    }

    [HttpPost]
    public IActionResult Create([FromForm] User user)
    {
        // this validates the user request to avoid duplicate entries
        // before creating a new user in the database        
        if (user.email is not null)
        {
            var userServiceUser = UserService.GetByEmail(user.email);
            if (userServiceUser != null && user.email == userServiceUser.email)
                return BadRequest();
        }

        UserService.Add(user);
        return CreatedAtAction(nameof(Get), new { id = user.id }, user);
        
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromForm] int id,[FromForm] User user)
    {
        // This Updates a specific User object using the {id}
        // to retrieve the User and update the user type.
        if (id != user.id)
            return BadRequest();

        var existingUser = UserService.Get(id);
        if (existingUser is null)
            return NotFound();
        
        UserService.Update(user);

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var user = UserService.Get(id);

        if (user is null)
            return NotFound();

        UserService.Delete(id);

        return Ok();
    }
}