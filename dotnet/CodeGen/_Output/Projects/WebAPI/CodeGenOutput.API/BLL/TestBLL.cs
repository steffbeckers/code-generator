using CodeGenOutput.API.DAL;
using CodeGenOutput.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeGenOutput.API.BLL
{
    public interface ITestBLL
    {
        Task<IEnumerable<Test>> GetTestsAsync();
        Task<Test> GetTestByIdAsync(Guid id);
        Task<IEnumerable<Test>> SearchTestAsync(string term);
        Task<Test> CreateTestAsync(Test test);
        Task<Test> UpdateTestAsync(Test test);
        Task DeleteTestAsync(Test test);
    }

    public partial class BusinessLogicLayer : ITestBLL
    {
        private readonly IRepository<Test> _testRepository;

        public async Task<IEnumerable<Test>> GetTestsAsync()
        {
            return await _testRepository.GetAsync();
        }

        public async Task<Test> GetTestByIdAsync(Guid id)
        {
            return await _testRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Test>> SearchTestAsync(string term)
        {
            return await _testRepository.SearchTest(term);
        }

        public async Task<Test> CreateTestAsync(Test test)
        {
            Test createdTest = await _testRepository.CreateAsync(test);
            await _unitOfWork.Commit();
            return createdTest;
        }

        public async Task<Test> UpdateTestAsync(Test test)
        {
            Test updatedTest = await _testRepository.UpdateAsync(test);
            await _unitOfWork.Commit();
            return updatedTest;
        }

        public async Task DeleteTestAsync(Test test)
        {
            await _testRepository.DeleteAsync(test);
            await _unitOfWork.Commit();
        }
    }
}
