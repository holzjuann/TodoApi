using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Context;
using TodoApi.Entities;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoDbContext _todoContext;

        public TodoController(TodoDbContext todoContext) 
        {
            _todoContext = todoContext;
        }

        [HttpGet]
        [Route("/List")]
        public async Task<IActionResult> List() 
        {
            var todos = await _todoContext.Todos.ToListAsync();
            return Ok(todos);
        }
        [HttpPost]
        [Route("/Add")]
        public async Task<IActionResult> Add(Todo todo) 
        {
            if(!ModelState.IsValid) 
            {
                return BadRequest();
            } else 
            {
                await _todoContext.Todos.AddAsync(todo);
                await _todoContext.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpPut]
        [Route("/Edit")]
        public async Task<IActionResult> Edit(int id, Todo todo) {
            var todoDb = await _todoContext.Todos.FindAsync(id);

            if(!ModelState.IsValid) 
            {
                return BadRequest();
            } else
            {
                todoDb.Title = todo.Title;
                todoDb.Description = todo.Description;
                todoDb.Status = todo.Status;
                todoDb.Finished = todo.Finished;

                _todoContext.Todos.Update(todoDb);
                await _todoContext.SaveChangesAsync();
            }

            return Ok(todoDb);
        }

        [HttpDelete]
        [Route("/Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var todo = await _todoContext.Todos.FindAsync(id);

            if(!ModelState.IsValid) 
            {
                return BadRequest();
            } else
            {
                _todoContext.Todos.Remove(todo);
                await _todoContext.SaveChangesAsync();
            }

            return Ok(todo);
        }

        [HttpGet]
        [Route("/SearchById")]
        public async Task<IActionResult> SearchById(int id)
        {
            var tododb = await _todoContext.Todos.FindAsync(id);
           
            return Ok(tododb);
        }

        [HttpGet]
        [Route("/SearchByTitle")]
        public async Task<IActionResult> SearchByTitle(string title)
        {
            var todo = from t in _todoContext.Todos
                select t;
            if(!String.IsNullOrEmpty(title))
            {
                todo = todo.Where(t => t.Title.Contains(title));
            }
           
            return Ok(todo);
        }
    }
}