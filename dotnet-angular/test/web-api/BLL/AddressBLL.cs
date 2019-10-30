using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.BLL
{
	/// <summary>
	/// The business logic layer for Addresses.
	/// </summary>
    public class AddressBLL
    {
        private readonly AddressRepository addressRepository;

		/// <summary>
		/// The constructor of the Address business logic layer.
		/// </summary>
        public AddressBLL(
			AddressRepository addressRepository
		)
        {
            this.addressRepository = addressRepository;
        }

		/// <summary>
		/// Retrieves all addresses.
		/// </summary>
		public async Task<IEnumerable<Address>> GetAllAddressesAsync()
        {
            return await this.addressRepository.GetAsync();
        }

		/// <summary>
		/// Retrieves one address by Id.
		/// </summary>
		public async Task<Address> GetAddressByIdAsync(Guid id)
        {
            return await this.addressRepository.GetWithLinkedEntitiesByIdAsync(id);
        }

		/// <summary>
		/// Creates a new address record.
		/// </summary>
        public async Task<Address> CreateAddressAsync(Address address)
        {
			// #-#-#  
			// Before creation
			// #-#-#

			address = await this.addressRepository.InsertAsync(address);

			// #-#-#  
			// After creation
			// #-#-#

            return address;
        }

		/// <summary>
		/// Updates an existing address record by Id.
		/// </summary>
        public async Task<Address> UpdateAddressAsync(Guid id, Address addressUpdate)
        {
            // Retrieve existing
            Address address = await this.addressRepository.GetByIdAsync(id);
            if (address == null)
            {
                return null;
            }

            // Mapping
            address.Street = addressUpdate.Street;
            address.Number = addressUpdate.Number;
            address.PostalCode = addressUpdate.PostalCode;
            address.City = addressUpdate.City;
            address.Country = addressUpdate.Country;
            address.AccountId = addressUpdate.AccountId;

            return await this.addressRepository.UpdateAsync(address);
        }

		/// <summary>
		/// Deletes an existing address record by Id.
		/// </summary>
        public async Task<Address> DeleteAddressAsync(Address address)
        {
            await this.addressRepository.DeleteAsync(address);

            return address;
        }
    }
}
