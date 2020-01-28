using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.BLL
{
	/// <summary>
	/// The business logic layer for Suppliers.
	/// </summary>
    public class SupplierBLL
    {
        private readonly SupplierRepository supplierRepository;
        private readonly ProductRepository productRepository;
        private readonly ProductSupplierRepository productSupplierRepository;

		/// <summary>
		/// The constructor of the Supplier business logic layer.
		/// </summary>
        public SupplierBLL(
			SupplierRepository supplierRepository,
            ProductRepository productRepository,
			ProductSupplierRepository productSupplierRepository
		)
        {
            this.supplierRepository = supplierRepository;
            this.productRepository = productRepository;
			this.productSupplierRepository = productSupplierRepository;
        }

		/// <summary>
		/// Retrieves all suppliers.
		/// </summary>
		public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync()
        {
			// #-#-# {83B8AA9F-713A-42FB-ADE1-8A4AA43886C8}
			// Before retrieval
			// #-#-#

            return await this.supplierRepository.GetWithLinkedEntitiesAsync();
        }

		/// <summary>
		/// Retrieves one supplier by Id.
		/// </summary>
		public async Task<Supplier> GetSupplierByIdAsync(Guid id)
        {
			// #-#-# {F838CE2A-D0FB-4F8A-A826-0D653DEECB2B}
			// Before retrieval
			// #-#-#

            return await this.supplierRepository.GetWithLinkedEntitiesByIdAsync(id);
        }

		/// <summary>
		/// Creates a new supplier record.
		/// </summary>
        public async Task<Supplier> CreateSupplierAsync(Supplier supplier)
        {
            // Validation
            if (supplier == null) { return null; }

			// Trimming strings
            if (!string.IsNullOrEmpty(supplier.Name))
                supplier.Name = supplier.Name.Trim();
            if (!string.IsNullOrEmpty(supplier.Phone))
                supplier.Phone = supplier.Phone.Trim();

			// #-#-# {D4775AF3-4BFA-496A-AA82-001028A22DD6}
			// Before creation
			// #-#-#

			supplier = await this.supplierRepository.InsertAsync(supplier);

			// #-#-# {1972C619-D2F2-48FD-8474-3A69621B1F78}
			// After creation
			// #-#-#

            return supplier;
        }

		/// <summary>
		/// Updates an existing supplier record by Id.
		/// </summary>
        public async Task<Supplier> UpdateSupplierAsync(Supplier supplierUpdate)
        {
            // Validation
            if (supplierUpdate == null) { return null; }

            // Retrieve existing
            Supplier supplier = await this.supplierRepository.GetByIdAsync(supplierUpdate.Id);
            if (supplier == null)
            {
                return null;
            }

			// Trimming strings
            if (!string.IsNullOrEmpty(supplierUpdate.Name))
                supplierUpdate.Name = supplierUpdate.Name.Trim();
            if (!string.IsNullOrEmpty(supplierUpdate.Phone))
                supplierUpdate.Phone = supplierUpdate.Phone.Trim();

            // Mapping
            supplier.Name = supplierUpdate.Name;
            supplier.Phone = supplierUpdate.Phone;

			// #-#-# {B5914243-E57E-41AE-A7C8-553F2F93267B}
			// Before update
			// #-#-#

			supplier = await this.supplierRepository.UpdateAsync(supplier);

			// #-#-# {983B1B6C-14A7-4925-8571-D77447DF0ADA}
			// After update
			// #-#-#

            return supplier;
        }

        public async Task<Supplier> LinkProductToSupplierAsync(ProductSupplier productSupplier)
        {
            // Validation
            if (productSupplier == null) { return null; }

            // Check if supplier exists
            Supplier supplier = await this.supplierRepository.GetByIdAsync(productSupplier.SupplierId);
            if (supplier == null)
            {
                return null;
            }

            // Check if product exists
            Product product = await this.productRepository.GetByIdAsync(productSupplier.ProductId);
            if (product == null)
            {
                return null;
            }

            // Retrieve existing link
            ProductSupplier productSupplierLink = this.productSupplierRepository.GetBySupplierAndProductId(productSupplier.SupplierId, productSupplier.ProductId);

            if (productSupplierLink == null)
            {
                await this.productSupplierRepository.InsertAsync(productSupplier);
            }
            else
            {
                // Mapping of fields on many-to-many
                productSupplierLink.Comment = productSupplier.Comment;

                await this.productSupplierRepository.UpdateAsync(productSupplierLink);
            }

            return await this.GetSupplierByIdAsync(productSupplier.SupplierId);
        }

        public async Task<Supplier> UnlinkProductFromSupplierAsync(ProductSupplier productSupplier)
        {
            // Validation
            if (productSupplier == null) { return null; }

            // Retrieve existing link
            ProductSupplier productSupplierLink = this.productSupplierRepository.GetBySupplierAndProductId(productSupplier.SupplierId, productSupplier.ProductId);
		
            if (productSupplierLink != null)
            {
                await this.productSupplierRepository.DeleteAsync(productSupplierLink);
            }

            return await this.GetSupplierByIdAsync(productSupplier.SupplierId);
        }

		/// <summary>
		/// Deletes an existing supplier record by Id.
		/// </summary>
        public async Task<Supplier> DeleteSupplierAsync(Supplier supplier)
        {
			// #-#-# {FE1A99E0-482D-455B-A8C1-3C2C11FACA58}
			// Before deletion
			// #-#-#

            await this.supplierRepository.DeleteAsync(supplier);

			// #-#-# {F09857C0-44E7-4E6C-B3E6-883C0D28E1A6}
			// After deletion
			// #-#-#

            return supplier;
        }
    }
}
