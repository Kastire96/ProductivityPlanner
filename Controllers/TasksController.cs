using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductivityPlanner.API.Data;
using ProductivityPlanner.API.Models;

namespace ProductivityPlanner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<TodoTask>>> GetTasksByUser(int userId)
        {
            return await _context.Tasks
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoTask>> GetTask(int id)
        {
            var todoTask = await _context.Tasks.FindAsync(id);
            if (todoTask == null) return NotFound();
            return todoTask;
        }

        [HttpPost]
        public async Task<ActionResult<TodoTask>> PostTask(TodoTask todoTask)
        {
            _context.Tasks.Add(todoTask);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTask), new { id = todoTask.Id }, todoTask);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, TodoTask todoTask)
        {
            if (id != todoTask.Id) return BadRequest();

            _context.Entry(todoTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Tasks.Any(e => e.Id == id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var todoTask = await _context.Tasks.FindAsync(id);
            if (todoTask == null) return NotFound();

            _context.Tasks.Remove(todoTask);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
