using CodeGenOutput.API.DAL;
using CodeGenOutput.API.DAL.Repositories;
using CodeGenOutput.API.Models;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ValidationException = CodeGenOutput.API.Validation.ValidationException;

namespace CodeGenOutput.API.BLL
{
    public interface IAddressBLL
    {
        Task<IEnumerable<Address>> GetAddressesAsync(string include = "");
        Task<Address> GetAddressByIdAsync(Guid id, string include = "");
        Task<Address> CreateAddressAsync(Address address);
        Task<Address> UpdateAddressAsync(Address address);
        Task DeleteAddressAsync(Guid id);
    }

    public partial class BusinessLogicLayer : IAddressBLL
    {
        public async Task<IEnumerable<Address>> GetAddressesAsync(string include = "")
        {
            return await _unitOfWork.GetRepository<Address>().GetAsync(include: include);
        }

        public async Task<Address> GetAddressByIdAsync(Guid id, string include = "")
        {
            return await _unitOfWork.GetRepository<Address>().GetByIdAsync(id, include: include);
        }

        public async Task<Address> CreateAddressAsync(Address address)
        {
            await ValidateAddressAsync(address);
            Address createdAddress = await _unitOfWork.GetRepository<Address>().CreateAsync(address);
            await _unitOfWork.Commit();
            
            return createdAddress;
        }

        public async Task<Address> UpdateAddressAsync(Address address)
        {
            await ValidateAddressAsync(address);
            Address updatedAddress = await _unitOfWork.GetRepository<Address>().UpdateAsync(address);
            await _unitOfWork.Commit();
            
            return updatedAddress;
        }

        public async Task DeleteAddressAsync(Guid id)
        {
            await _unitOfWork.GetRepository<Address>().DeleteAsync(id);
            await _unitOfWork.Commit();
        }

        private async Task ValidateAddressAsync(Address address)
        {
            AddressValidator validator = new AddressValidator();
            ValidationResult validationResult = await validator.ValidateAsync(address);
            if (!validationResult.IsValid) { throw new ValidationException(validationResult.Errors); }
        }
    }
}
