using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.API.BLL;
using Test.API.Models;
using Test.API.ViewModels;

namespace Test.API.Controllers
{
	/// <summary>
	/// The Todos controller.
	/// </summary>
    [Route("api/[controller]")]
    [ApiController]
	[Produces("application/json")]
    public class TodosController : ControllerBase
    {
        private readonly ILogger<TodosController> logger;
        private readonly IMapper mapper;
        private readonly TodoBLL bll;

		/// <summary>
		/// The constructor of the Todos controller.
		/// </summary>
        public TodosController(
            ILogger<TodosController> logger,
			IMapper mapper,
            TodoBLL bll
        )
        {
            this.logger = logger;
			this.mapper = mapper;
            this.bll = bll;
        }

        // GET: api/Todos
		/// <summary>
		/// Retrieves all todos.
		/// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoVM>>> GetTodos()
        {
            IEnumerable<Todo> todos = await this.bll.GetAllTodosAsync();

            return this.mapper.Map<IEnumerable<Todo>, List<TodoVM>>(todos);
        }

        // GET: api/Todos/{id}
		/// <summary>
		/// Retrieves a specific todo.
		/// </summary>
		/// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoVM>> GetTodo([FromRoute] Guid id)
        {
            Todo todo = await this.bll.GetTodoByIdAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            return this.mapper.Map<Todo, TodoVM>(todo);
        }

        // POST: api/Todos
		/// <summary>
		/// Creates a new todo.
		/// </summary>
		/// <param name="todoVM"></param>
        [HttpPost]
        public async Task<ActionResult<TodoVM>> CreateTodo([FromBody] TodoVM todoVM)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapping
            Todo todo = this.mapper.Map<TodoVM, Todo>(todoVM);

            todo = await this.bll.CreateTodoAsync(todo);

            return CreatedAtAction(
				"GetTodo",
				new { id = todo.Id },
				this.mapper.Map<Todo, TodoVM>(todo)
			);
        }

		// PUT: api/Todos/{id}
		/// <summary>
		/// Updates a specific todo.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="todoVM"></param>
        [HttpPut("{id}")]
        public async Task<ActionResult<TodoVM>> UpdateTodo([FromRoute] Guid id, [FromBody] TodoVM todoVM)
        {
			// Validation
            if (!ModelState.IsValid || id != todoVM.Id)
            {
                return BadRequest(ModelState);
            }

            // Retrieve existing todo
            Todo todo = await this.bll.GetTodoByIdAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

			// Mapping
            Todo todoUpdate = this.mapper.Map<TodoVM, Todo>(todoVM);

			// Update fields
            todo.Title = todoUpdate.Title;
            todo.DueDate = todoUpdate.DueDate;
			
            todo = await this.bll.UpdateTodoAsync(id, todo);

			return this.mapper.Map<Todo, TodoVM>(todo);
        }

        // DELETE: api/Todos/{id}
		/// <summary>
		/// Deletes a specific todo.
		/// </summary>
		/// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoVM>> DeleteTodo([FromRoute] Guid id)
        {
            // Retrieve existing todo
            Todo todo = await this.bll.GetTodoByIdAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            await this.bll.DeleteTodoAsync(todo);

            return this.mapper.Map<Todo, TodoVM>(todo);
        }
    }
}
