using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.BLL
{
	/// <summary>
	/// The business logic layer for Products.
	/// </summary>
    public class ProductBLL
    {
        private readonly ProductRepository productRepository;
        private readonly SupplierRepository supplierRepository;
        private readonly ProductSupplierRepository productSupplierRepository;

		/// <summary>
		/// The constructor of the Product business logic layer.
		/// </summary>
        public ProductBLL(
			ProductRepository productRepository,
            SupplierRepository supplierRepository,
			ProductSupplierRepository productSupplierRepository
		)
        {
            this.productRepository = productRepository;
            this.supplierRepository = supplierRepository;
			this.productSupplierRepository = productSupplierRepository;
        }

		/// <summary>
		/// Retrieves all products.
		/// </summary>
		public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
			// #-#-# {83B8AA9F-713A-42FB-ADE1-8A4AA43886C8}
            // Before retrieval
            // #-#-#

            return await this.productRepository.GetWithLinkedEntitiesAsync();
        }

		/// <summary>
		/// Retrieves one product by Id.
		/// </summary>
		public async Task<Product> GetProductByIdAsync(Guid id)
        {
			// #-#-# {F838CE2A-D0FB-4F8A-A826-0D653DEECB2B}
            // Before retrieval
            // #-#-#

            return await this.productRepository.GetWithLinkedEntitiesByIdAsync(id);
        }

		/// <summary>
		/// Creates a new product record.
		/// </summary>
        public async Task<Product> CreateProductAsync(Product product)
        {
            // Validation
            if (product == null) { return null; }

			// Trimming strings
            if (!string.IsNullOrEmpty(product.Name))
                product.Name = product.Name.Trim();
            if (!string.IsNullOrEmpty(product.Code))
                product.Code = product.Code.Trim();

			// #-#-# {D4775AF3-4BFA-496A-AA82-001028A22DD6}
            // Before creation
            // #-#-#

			product = await this.productRepository.InsertAsync(product);

			// #-#-# {1972C619-D2F2-48FD-8474-3A69621B1F78}
            // After creation
            // #-#-#

            return product;
        }

		/// <summary>
		/// Updates an existing product record by Id.
		/// </summary>
        public async Task<Product> UpdateProductAsync(Product productUpdate)
        {
            // Validation
            if (productUpdate == null) { return null; }

            // Retrieve existing
            Product product = await this.productRepository.GetByIdAsync(productUpdate.Id);
            if (product == null)
            {
                return null;
            }

			// Trimming strings
            if (!string.IsNullOrEmpty(productUpdate.Name))
                productUpdate.Name = productUpdate.Name.Trim();
            if (!string.IsNullOrEmpty(productUpdate.Code))
                productUpdate.Code = productUpdate.Code.Trim();

            // Mapping
            product.Name = productUpdate.Name;
            product.Code = productUpdate.Code;
            product.Quantity = productUpdate.Quantity;
            product.Price = productUpdate.Price;
            product.Active = productUpdate.Active;

			// #-#-# {B5914243-E57E-41AE-A7C8-553F2F93267B}
            // Before update
            // #-#-#

			product = await this.productRepository.UpdateAsync(product);

			// #-#-# {983B1B6C-14A7-4925-8571-D77447DF0ADA}
            // After update
            // #-#-#

            return product;
        }

        public async Task<Product> LinkSupplierToProductAsync(ProductSupplier productSupplier)
        {
            // Validation
            if (productSupplier == null) { return null; }

            // Check if product exists
            Product product = await this.productRepository.GetByIdAsync(productSupplier.ProductId);
            if (product == null)
            {
                return null;
            }

            // Check if supplier exists
            Supplier supplier = await this.supplierRepository.GetByIdAsync(productSupplier.SupplierId);
            if (supplier == null)
            {
                return null;
            }

            // Retrieve existing link
            ProductSupplier productSupplierLink = this.productSupplierRepository.GetByProductAndSupplierId(productSupplier.ProductId, productSupplier.SupplierId);

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

            return await this.GetProductByIdAsync(productSupplier.ProductId);
        }

        public async Task<Product> UnlinkSupplierFromProductAsync(ProductSupplier productSupplier)
        {
            // Validation
            if (productSupplier == null) { return null; }

            // Retrieve existing link
            ProductSupplier productSupplierLink = this.productSupplierRepository.GetByProductAndSupplierId(productSupplier.ProductId, productSupplier.SupplierId);
		
            if (productSupplierLink != null)
            {
                await this.productSupplierRepository.DeleteAsync(productSupplierLink);
            }

            return await this.GetProductByIdAsync(productSupplier.ProductId);
        }

		/// <summary>
		/// Deletes an existing product record by Id.
		/// </summary>
        public async Task<Product> DeleteProductByIdAsync(Guid productId)
        {
            Product product = await this.productRepository.GetByIdAsync(productId);

            return await this.DeleteProductAsync(product);
        }

		/// <summary>
		/// Deletes an existing product record.
		/// </summary>
        public async Task<Product> DeleteProductAsync(Product product)
        {
            // Validation
            if (product == null) { return null; }

			// #-#-# {FE1A99E0-482D-455B-A8C1-3C2C11FACA58}
            // Before deletion
            // #-#-#

            await this.productRepository.DeleteAsync(product);

			// #-#-# {F09857C0-44E7-4E6C-B3E6-883C0D28E1A6}
            // After deletion
            // #-#-#

            return product;
        }
    }
}
