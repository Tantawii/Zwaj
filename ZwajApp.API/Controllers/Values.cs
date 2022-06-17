using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZwajApp.API.Data;

namespace ZwajApp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ValuesController : ControllerBase
{
        private readonly DataContext _context;
        
    public ValuesController(DataContext context)
    {
            _context = context;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var values = await _context.Values.ToListAsync();
        return Ok(values);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var value = await _context.Values.FirstOrDefaultAsync(x=> x.id == id);
        return Ok(value);
    }
}
