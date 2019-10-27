using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.BLL
{
    /// <summary>
    /// The business logic layer for Todoes.
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
        /// Retrieves all todoes.
        /// </summary>
        public async Task<IEnumerable<Todo>> GetAllTodoesAsync()
        {
            return await this.todoRepository.GetAsync();
        }

        /// <summary>
        /// Retrieves one todo by Id.
        /// </summary>
        public async Task<Todo> GetTodoByIdAsync(Guid id)
        {
            return await this.todoRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Creates a new todo record.
        /// </summary>
        public async Task<Todo> CreateTodoAsync(Todo todo)
        {
            return await this.todoRepository.InsertAsync(todo);
        }

        /// <summary>
        /// Updates an existing todo record by Id.
        /// </summary>
        public async Task<Todo> UpdateTodoAsync(Guid id, Todo todoUpdate)
        {
            // Retrieve existing
            Todo todo = await this.todoRepository.GetByIdAsync(id);
            if (todo == null)
            {
                return null;
            }

            // Mapping
            todo.Title = todoUpdate.Title;
            todo.Body = todoUpdate.Body;
            todo.ProjectId = todoUpdate.ProjectId;

            return await this.todoRepository.UpdateAsync(todo);
        }

        // TODO
        //public async Task<League> UnlinkPlayerFromLeagueAsync(LeaguePlayer leaguePlayer)
        //{
        //    LeaguePlayer leaguePlayerLink = this.leaguePlayerRepository.GetByLeagueAndPlayerId(leaguePlayer.LeagueId, leaguePlayer.PlayerId);
        //
        //    if (leaguePlayerLink != null)
        //    {
        //        await this.leaguePlayerRepository.DeleteAsync(leaguePlayerLink);
        //    }

        //    return this.leagueRepository.GetWithPlayersById(leaguePlayer.LeagueId);
        //}

        /// <summary>
        /// Deletes an existing todo record by Id.
        /// </summary>
        public async Task<Todo> DeleteTodoAsync(Todo todo)
        {
            await this.todoRepository.DeleteAsync(todo);

            return todo;
        }
    }
}
