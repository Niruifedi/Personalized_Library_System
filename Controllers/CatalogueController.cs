using Personalized_Library_System.Models;
using Personalized_Library_System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Personalized_Library_System.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class CatalogueController: ControllerBase
{
    private readonly  AppDbContext _context;

    public CatalogueController(AppDbContext context)
    {
        this._context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User_Catalogue>>> Get() =>
    await _context.Catalogues.ToListAsync();

    [HttpPost]
    public async Task<IActionResult> create([FromForm]User_Catalogue catalogue)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _context.Add(catalogue);
        await _context.SaveChangesAsync();
        if (catalogue.UserId != 0)
        {
            // var userId = catalogue.UserId;
            var user = await _context.User.FindAsync(catalogue.UserId);
            if (user != null)
            {
                user.Catalogues.Add(catalogue);
                await _context.SaveChangesAsync();
            } 
            else
            {
                return NotFound($"No User Found with id ");
            }

        }

        return Created("Catalogue Created Successfully", catalogue);    

    }

}