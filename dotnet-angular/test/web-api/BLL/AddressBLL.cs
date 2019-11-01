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
			// #-#-# {83B8AA9F-713A-42FB-ADE1-8A4AA43886C8}
			// Before retrieval
			// #-#-#

            return await this.addressRepository.GetAsync();
        }

		/// <summary>
		/// Retrieves one address by Id.
		/// </summary>
		public async Task<Address> GetAddressByIdAsync(Guid id)
        {
			// #-#-# {F838CE2A-D0FB-4F8A-A826-0D653DEECB2B}
			// Before retrieval
			// #-#-#

            return await this.addressRepository.GetWithLinkedEntitiesByIdAsync(id);
        }

		/// <summary>
		/// Creates a new address record.
		/// </summary>
        public async Task<Address> CreateAddressAsync(Address address)
        {
			// Trimming strings
            address.Street = address.Street.Trim();
            address.Number = address.Number.Trim();
            address.PostalCode = address.PostalCode.Trim();
            address.City = address.City.Trim();

			// #-#-# {D4775AF3-4BFA-496A-AA82-001028A22DD6}
			// Before creation
			// #-#-#

			address = await this.addressRepository.InsertAsync(address);

			// #-#-# {1972C619-D2F2-48FD-8474-3A69621B1F78}
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

			// Trimming strings
            addressUpdate.Street = addressUpdate.Street.Trim();
            addressUpdate.Number = addressUpdate.Number.Trim();
            addressUpdate.PostalCode = addressUpdate.PostalCode.Trim();
            addressUpdate.City = addressUpdate.City.Trim();

            // Mapping
            address.Street = addressUpdate.Street;
            address.Number = addressUpdate.Number;
            address.PostalCode = addressUpdate.PostalCode;
            address.City = addressUpdate.City;
            address.Primary = addressUpdate.Primary;
            address.AccountId = addressUpdate.AccountId;

			// #-#-# {B5914243-E57E-41AE-A7C8-553F2F93267B}
			// Before update
			// #-#-#

			address = await this.addressRepository.UpdateAsync(address);

			// #-#-# {983B1B6C-14A7-4925-8571-D77447DF0ADA}
			// After update
			// #-#-#

            return address;
        }

		/// <summary>
		/// Deletes an existing address record by Id.
		/// </summary>
        public async Task<Address> DeleteAddressAsync(Address address)
        {
			// #-#-# {FE1A99E0-482D-455B-A8C1-3C2C11FACA58}
			// Before deletion
			// #-#-#

            await this.addressRepository.DeleteAsync(address);

			// #-#-# {F09857C0-44E7-4E6C-B3E6-883C0D28E1A6}
			// After deletion
			// #-#-#

            return address;
        }
    }
}
