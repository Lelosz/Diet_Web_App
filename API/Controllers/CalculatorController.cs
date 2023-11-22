using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly CalculatorContext _context;

        public CalculatorController(CalculatorContext context) => _context = context;
        [HttpGet]
        public async Task<IEnumerable<Calculator>> Get()
            => await _context.Calculators.ToListAsync();

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var issue = await _context.Calculators.FindAsync(id);
            return issue == null ? NotFound() : Ok(issue);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Calculator calculator)
        {
            await _context.Calculators.AddAsync(calculator);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = calculator.Id }, calculator);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Calculator calculator) 
        {
            if (id != calculator.Id) return BadRequest();

            _context.Entry(calculator).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var calculatorToDelete = await _context.Calculators.FindAsync(id);
            if (calculatorToDelete == null) return NotFound();

            _context.Calculators.Remove(calculatorToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
