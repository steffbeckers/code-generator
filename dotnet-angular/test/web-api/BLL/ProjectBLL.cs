using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.BLL
{
	/// <summary>
	/// The business logic layer for Projects.
	/// </summary>
    public class ProjectBLL
    {
        private readonly ProjectRepository projectRepository;
        // TODO: private readonly Project...Repository project...Repository;

		/// <summary>
		/// The constructor of the Project business logic layer.
		/// </summary>
        public ProjectBLL(
			ProjectRepository projectRepository//,
			// TODO: Project...Repository project...Repository
		)
        {
            this.projectRepository = projectRepository;
            // TODO: this.Project...Repository = Project...Repository;
        }

		/// <summary>
		/// Retrieves all projects.
		/// </summary>
		public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await this.projectRepository.GetAsync();
        }

		/// <summary>
		/// Retrieves one project by Id.
		/// </summary>
		public async Task<Project> GetProjectByIdAsync(Guid id)
        {
            return await this.projectRepository.GetByIdAsync(id);
        }

		/// <summary>
		/// Creates a new project record.
		/// </summary>
        public async Task<Project> CreateProjectAsync(Project project)
        {
            return await this.projectRepository.InsertAsync(project);
        }

		/// <summary>
		/// Updates an existing project record by Id.
		/// </summary>
        public async Task<Project> UpdateProjectAsync(Guid id, Project projectUpdate)
        {
            // Retrieve existing
            Project project = await this.projectRepository.GetByIdAsync(id);
            if (project == null)
            {
                return null;
            }

            // Mapping
            project.Name = projectUpdate.Name;
            project.Description = projectUpdate.Description;

            return await this.projectRepository.UpdateAsync(project);
        }

		// TODO
        //public async Task<League> LinkPlayerToLeagueAsync(LeaguePlayer leaguePlayer)
        //{
        //    LeaguePlayer leaguePlayerLink = this.leaguePlayerRepository.GetByLeagueAndPlayerId(leaguePlayer.LeagueId, leaguePlayer.PlayerId);
		//
        //    if (leaguePlayerLink == null)
        //    {
        //        await this.leaguePlayerRepository.InsertAsync(leaguePlayer);
        //    }
        //    else
        //    {
        //        // Mapping
        //        leaguePlayerLink.Handicap = leaguePlayer.Handicap;
		//
        //        await this.leaguePlayerRepository.UpdateAsync(leaguePlayerLink);
        //    }
		//
        //    return this.leagueRepository.GetWithPlayersById(leaguePlayer.LeagueId);
        //}

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
		/// Deletes an existing project record by Id.
		/// </summary>
        public async Task<Project> DeleteProjectAsync(Project project)
        {
            await this.projectRepository.DeleteAsync(project);

            return project;
        }
    }
}
