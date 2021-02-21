using CodeGen.API.DAL.Repositories;
using CodeGen.API.Hubs;
using CodeGen.API.Models;
using CodeGen.API.Validation;
using FluentValidation.Results;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ValidationException = CodeGen.API.Validation.ValidationException;

namespace CodeGen.API.BLL
{
    public interface IProjectBLL
    {
        Task<IEnumerable<Project>> GetProjectsAsync(string include = "");
        Task<Project> GetProjectByIdAsync(Guid id, string include = "");
        Task<Project> CreateProjectAsync(Project project);
        Task<Project> UpdateProjectAsync(Project project);
        Task DeleteProjectAsync(Guid id);
    }

    public partial class BusinessLogicLayer : IProjectBLL
    {
        public async Task<IEnumerable<Project>> GetProjectsAsync(string include = "")
        {
            return await _unitOfWork.GetRepository<Project>().GetAsync(include: include);
        }

        public async Task<Project> GetProjectByIdAsync(Guid id, string include = "")
        {
            return await _unitOfWork.GetRepository<Project>().GetByIdAsync(id, include: include);
        }

        public async Task<Project> CreateProjectAsync(Project project)
        {
            await ValidateProjectAsync(project);
            Project createdProject = await _unitOfWork.GetRepository<Project>().CreateAsync(project);
            await _unitOfWork.Commit();

            _realtimeHub.Clients.Group("code-generators").SendAsync("Generate", createdProject);

            return createdProject;
        }

        public async Task<Project> UpdateProjectAsync(Project project)
        {
            await ValidateProjectAsync(project);
            Project updatedProject = await _unitOfWork.GetRepository<Project>().UpdateAsync(project);
            await _unitOfWork.Commit();

            _realtimeHub.Clients.Group("code-generators").SendAsync("Generate", updatedProject);

            return updatedProject;
        }

        public async Task DeleteProjectAsync(Guid id)
        {
            await _unitOfWork.GetRepository<Project>().DeleteAsync(id);
            await _unitOfWork.Commit();
        }

        private async Task ValidateProjectAsync(Project project)
        {
            ValidationResult validationResult = await Validators.ProjectValidator.ValidateAsync(project);
            if (!validationResult.IsValid) { throw new ValidationException(validationResult.Errors); }
        }
    }
}
