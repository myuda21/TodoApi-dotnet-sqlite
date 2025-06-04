using Microsoft.AspNetCore.Mvc;
using TODOAPI.Data;
using TODOAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace TODOAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TodoController(AppDbContext context) => _context = context;

        [HttpGet("{userId}")]
        public IActionResult GetTodos(int userId)
        {
            var todos = _context.TodoItems.Where(t => t.UserId == userId).ToList();
            return Ok(todos);
        }

        [HttpPost]
        public IActionResult AddTodo(TodoItem todo)
        {
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