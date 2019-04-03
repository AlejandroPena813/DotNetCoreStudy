using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicroServiceSetup.Models;
using MicroServiceSetup.Services;

namespace MicroServiceSetup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly TodoContext _context; //Entity Context
        private readonly ITodoService _todoService;

        public ValuesController(TodoContext context, ITodoService todoService)
        {
            _context = context;
            _todoService = todoService;
        }

        [HttpPost]
        public async Task<ActionResult<Todo>> PostTodoItem(Todo item)
        {
            try
            {
                _context.ToDoItems.Add(item);
                _context.SaveChanges();

                _todoService.GenMessage(item);
            }
            catch (Exception e)
            {
                //normally a log
                Console.WriteLine(e);
            }
        
            return Ok();
//            return CreatedAtAction(nameof(GetToDoItem), new {id = item.Id}, item);
        }
        
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] {"value1", "value2"};
        }

//        // GET api/values/5
//        [HttpGet("{id}")]
//        public ActionResult<string> Get(int id)
//        {
//            return "value";
//        }
//
//        // POST api/values
//        [HttpPost]
//        public void Post([FromBody] string value)
//        {
//        }
//
//        // PUT api/values/5
//        [HttpPut("{id}")]
//        public void Put(int id, [FromBody] string value)
//        {
//        }
//
//        // DELETE api/values/5
//        [HttpDelete("{id}")]
//        public void Delete(int id)
//        {
//        }
    }
}