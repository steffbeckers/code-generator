using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.BLL
{
	/// <summary>
	/// The business logic layer for Todos.
	/// </summary>
    public class TodoBLL
    {
        private readonly TodoRepository todoRepository;

		/// <summary>
		/// The constructor of the Todo business logic layer.
		/// </summary>
        public TodoBLL(
			TodoRepository todoRepository
		)
        {
            this.todoRepository = todoRepository;
        }

		/// <summary>
		/// Retrieves all todos.
		/// </summary>
		public async Task<IEnumerable<Todo>> GetAllTodosAsync()
        {
			// #-#-# {83B8AA9F-713A-42FB-ADE1-8A4AA43886C8}
            // Before retrieval
            // #-#-#

            return await this.todoRepository.GetWithLinkedEntitiesAsync();
        }

		/// <summary>
		/// Retrieves one todo by Id.
		/// </summary>
		public async Task<Todo> GetTodoByIdAsync(Guid id)
        {
			// #-#-# {F838CE2A-D0FB-4F8A-A826-0D653DEECB2B}
            // Before retrieval
            // #-#-#

            return await this.todoRepository.GetWithLinkedEntitiesByIdAsync(id);
        }

		/// <summary>
		/// Creates a new todo record.
		/// </summary>
        public async Task<Todo> CreateTodoAsync(Todo todo)
        {
			// Trimming strings
            todo.Title = todo.Title.Trim();

			// #-#-# {D4775AF3-4BFA-496A-AA82-001028A22DD6}
            // Before creation
            // #-#-#

			todo = await this.todoRepository.InsertAsync(todo);

			// #-#-# {1972C619-D2F2-48FD-8474-3A69621B1F78}
            // After creation
            // #-#-#

            return todo;
        }

		/// <summary>
		/// Updates an existing todo record by Id.
		/// </summary>
        public async Task<Todo> UpdateTodoAsync(Todo todoUpdate)
        {
            // Retrieve existing
            Todo todo = await this.todoRepository.GetByIdAsync(todoUpdate.Id);
            if (todo == null)
            {
                return null;
            }

			// Trimming strings
            if (!string.IsNullOrEmpty(todoUpdate.Title))
                todoUpdate.Title = todoUpdate.Title.Trim();

            // Mapping
            todo.Title = todoUpdate.Title;
            todo.DueDate = todoUpdate.DueDate;

			// #-#-# {B5914243-E57E-41AE-A7C8-553F2F93267B}
            // Before update
            // #-#-#

			todo = await this.todoRepository.UpdateAsync(todo);

			// #-#-# {983B1B6C-14A7-4925-8571-D77447DF0ADA}
            // After update
            // #-#-#

            return todo;
        }

		/// <summary>
		/// Deletes an existing todo record by Id.
		/// </summary>
        public async Task<Todo> DeleteTodoAsync(Todo todo)
        {
			// #-#-# {FE1A99E0-482D-455B-A8C1-3C2C11FACA58}
            // Before deletion
            // #-#-#

            await this.todoRepository.DeleteAsync(todo);

			// #-#-# {F09857C0-44E7-4E6C-B3E6-883C0D28E1A6}
            // After deletion
            // #-#-#

            return todo;
        }
    }
}
