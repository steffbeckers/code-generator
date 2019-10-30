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
			// #-#-# {6B392F7F-C4B3-4E64-8703-AE95C834E86A}
			// Before creation
			// #-#-#

			address = await this.addressRepository.InsertAsync(address);

			// #-#-# {086618AE-01D1-4162-8C4C-03080741C2CB}
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

			// #-#-# {573CD65B-4771-4335-85AC-74C5FB2E2AC8}
			// Before update mapping
			// #-#-#

            // Mapping
            address.Street = addressUpdate.Street;
            address.Number = addressUpdate.Number;
            address.PostalCode = addressUpdate.PostalCode;
            address.City = addressUpdate.City;
            address.Country = addressUpdate.Country;
            address.AccountId = addressUpdate.AccountId;

			// #-#-# {61904A6D-4EB9-47DF-B58E-8DDA26B0FB8C}
			// Before update
			// #-#-#

			address = await this.addressRepository.UpdateAsync(address);

			// #-#-# {0DB85255-BF4B-462A-A3E9-847F47A6C1F0}
			// After update
			// #-#-#

            return address;
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
