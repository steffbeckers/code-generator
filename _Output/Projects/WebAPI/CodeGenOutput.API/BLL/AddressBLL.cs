using CodeGenOutput.API.DAL;
using CodeGenOutput.API.DAL.Repositories;
using CodeGenOutput.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeGenOutput.API.BLL
{
    public interface IAddressBLL
    {
        Task<IEnumerable<Address>> GetAddressesAsync(string include);
        Task<Address> GetAddressByIdAsync(Guid id, string include);
        Task<IEnumerable<Address>> SearchAddressAsync(string term);
        Task<Address> CreateAddressAsync(Address address);
        Task<Address> UpdateAddressAsync(Address address);
        Task DeleteAddressAsync(Guid id);
    }

    public partial class BusinessLogicLayer : IAddressBLL
    {
        private readonly IRepository<Address> _addressRepository;

        public async Task<IEnumerable<Address>> GetAddressesAsync(string include = "")
        {
            return await _addressRepository.GetAsync(include: include);
        }

        public async Task<Address> GetAddressByIdAsync(Guid id, string include = "")
        {
            return await _addressRepository.GetByIdAsync(id, include: include);
        }

        public async Task<IEnumerable<Address>> SearchAddressAsync(string term)
        {
            return await _addressRepository.SearchAddressAsync(term);
        }

        public async Task<Address> CreateAddressAsync(Address address)
        {
            Address createdAddress = await _addressRepository.CreateAsync(address);
            await _unitOfWork.Commit();
            return createdAddress;
        }

        public async Task<Address> UpdateAddressAsync(Address address)
        {
            // Keep creating auditing details
            Address existingAddress = await GetAddressByIdAsync(address.Id);
            address.DateCreated = existingAddress.DateCreated;

            Address updatedAddress = await _addressRepository.UpdateAsync(address);
            await _unitOfWork.Commit();
            return updatedAddress;
        }

        public async Task DeleteAddressAsync(Guid id)
        {
            await _addressRepository.DeleteAsync(id);
            await _unitOfWork.Commit();
        }
    }
}
