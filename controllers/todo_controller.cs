using Microsoft.AspNetCore.Mvc;
using TODOAPI.Data;
using TODOAPI.Models;

namespace TODOAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TodoController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/todo -> hanya todo milik user yang login
        [HttpGet]
        public IActionResult GetTodosForCurrentUser()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return Unauthorized("Silakan login dulu");

            var todos = _context.TodoItems.Where(t => t.UserId == userId.Value).ToList();
            return Ok(todos);
        }

        // POST api/todo -> buat todo baru untuk user login
        [HttpPost]
        public IActionResult AddTodo([FromBody] TodoItem todo)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return Unauthorized("Silakan login dulu");

            todo.UserId = userId.Value;

            _context.TodoItems.Add(todo);
            _context.SaveChanges();
            return Ok(todo);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTodo(int id, TodoItem updatedTodo)
        {
            var todo = _context.TodoItems.Find(id);
            if (todo == null) return NotFound();

            todo.Title = updatedTodo.Title;
            todo.IsCompleted = updatedTodo.IsCompleted;
            _context.SaveChanges();
            return Ok(todo);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodo(int id)
        {
            var todo = _context.TodoItems.Find(id);
            if (todo == null) return NotFound();

            _context.TodoItems.Remove(todo);
            _context.SaveChanges();
            return Ok("Deleted");
        }
    }
}