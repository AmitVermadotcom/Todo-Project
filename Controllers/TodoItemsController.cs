using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todoonboard_api.Models;

namespace todoonboard_api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly Context _context;

        public TodoItemsController(Context context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }
        // GET: api/Board/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(int id)
        {
            var todoitem = await _context.TodoItems.FindAsync(id);

            if (todoitem == null)
            {
                return NotFound();
            }

            return todoitem;
        }

        // GET: api/TodoItems/allTodosInBoard/{boardId}
        [HttpGet("allTodosInBoard/{id}")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItemsInBoard(int id)
        {
            var todoItem = _context.TodoItems.Where(r => r.board.Id == id);

            if (todoItem == null)
            {
                return NotFound();
            }
            return await todoItem.ToListAsync();
        }

        // GET: api/TodoItems/allIncompleteTodos
        [HttpGet("allIncompleteTodos")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetIncompletedItems()
        {
            var todoItem = _context.TodoItems.Where(r => r.isDone == false);

            if (todoItem == null)
            {
                return NotFound();
            }

            return await todoItem.ToListAsync();
        }

        // PATCH: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchTodoItem(int id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }
            var item = await _context.TodoItems.FirstOrDefaultAsync(item => item.Id == id);
            if (item == null) return BadRequest();
            item.title = todoItem.title == null ? item.title : todoItem.title;
            item.updated = DateTime.UtcNow;
            item.isDone = todoItem.isDone;
            // item.board_id = todoItem.board_id == 0 ? item.board_id : todoItem.board_id;
            await _context.SaveChangesAsync();
            return Ok(item);
        }

        // PUT: api/TodoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }
            _context.Entry(todoItem).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

            }
            return NoContent();
        }
        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            var board = _context.Boards.Find(todoItem.board.Id);
            todoItem.board = board;
            todoItem.created = DateTime.UtcNow;
            todoItem.updated = DateTime.UtcNow;

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return Ok(CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem).Value);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(int id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
