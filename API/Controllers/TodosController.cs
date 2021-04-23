using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Todos;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TodosController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Todo>>> GetTodos()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]  //todos/id
        public async Task<ActionResult<Todo>> GetTodo(Guid id)
        {
            return await Mediator.Send(new Details.Query{Id = id});
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodo(Todo todo)
        {
            return Ok(await Mediator.Send(new Create.Command {Todo = todo}));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditTodo(Guid id, Todo todo)
        {
            todo.Id = id;
            return Ok(await Mediator.Send(new Edit.Command{Todo = todo}));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(Guid id)
        {
            return Ok(await Mediator.Send(new Delete.Command{Id = id}));
        }

    }
}