using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicroServiceSetup.Models;
using MicroServiceSetup.Services;

namespace MicroServiceSetup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase // use Controller if want to use MVC like Razor
    {
        private readonly TodoContext _todoContext;
        private readonly ITodoService _todoService;
        public TodoController(TodoContext todoContext, ITodoService todoService)
        {
            _todoContext = todoContext;
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTodos()
        {
            try
            {
                // usually controller handles http, service does functionality + retrieval
                var result = await _todoContext.ToDoItems.ToListAsync(); // ToDoITems is table name
                return new OkObjectResult(result);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Trouble retrieving all todos: {e}");
                return StatusCode(500, e);
            }
        }

        [HttpGet("{id}")]         // [Authorize(Roles="Consumer, ServiceAccount")]
        public async Task<IActionResult> GetTodo(int id) // [FromRoute]
        {
            try
            {
                var todo = await _todoContext.ToDoItems.FindAsync(id);
                if (todo == null)
                    return StatusCode(404); // NotFound();
                return new OkObjectResult(todo);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostTodo([FromBody] Todo newTodo)
        {
            if (newTodo.Name == null) 
                return StatusCode(400, "Must provide todo name.");

            try
            {
                _todoContext.ToDoItems.Add(newTodo);
                await _todoContext.SaveChangesAsync();
                return new OkObjectResult("Succesfully saved todo");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e);
            }
        }
        
        //could also do a put, and assign whole new item?
        [HttpPatch] //should have an id in url?
        public async Task<IActionResult> UpdateTodo([FromBody] Todo updatedTodo)
        {
            if ( String.IsNullOrEmpty(updatedTodo.Id.ToString()) )
                return StatusCode(400, "Must provide id");
            try
            {
                var todo = await _todoContext.ToDoItems.FindAsync(updatedTodo.Id);
                if (todo == null) 
                    return StatusCode(404);

                if (!String.IsNullOrEmpty(updatedTodo.Name)) //prob a check above to ensure no empty
                    todo.Name = updatedTodo.Name;
                if (updatedTodo.IsComplete)
                    todo.IsComplete = updatedTodo.IsComplete;
            
                await _todoContext.SaveChangesAsync();

                return new OkObjectResult($"Succesfully updated: {todo.Name}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            try
            {
                var todo = await _todoContext.ToDoItems.FindAsync(id);
                if (todo == null) 
                    return StatusCode(404);
                
                _todoContext.ToDoItems.Remove(todo);
                await _todoContext.SaveChangesAsync();
                
                return new OkObjectResult($"Deleted {todo.Name}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e);
            }
        }
        //todo play w/ query params. Could of done: Authentication/roles, service token?, appsettings
    }
}