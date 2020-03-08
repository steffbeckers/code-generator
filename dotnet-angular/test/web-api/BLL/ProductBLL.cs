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
        private readonly CartRepository cartRepository;
        private readonly CartProductRepository cartProductRepository;

		/// <summary>
		/// The constructor of the Product business logic layer.
		/// </summary>
        public ProductBLL(
			ProductRepository productRepository,
            CartRepository cartRepository,
			CartProductRepository cartProductRepository
		)
        {
            this.productRepository = productRepository;
            this.cartRepository = cartRepository;
			this.cartProductRepository = cartProductRepository;
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
            if (!string.IsNullOrEmpty(product.Description))
                product.Description = product.Description.Trim();

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
            if (!string.IsNullOrEmpty(productUpdate.Description))
                productUpdate.Description = productUpdate.Description.Trim();

            // Mapping
            product.Name = productUpdate.Name;
            product.Description = productUpdate.Description;
            product.Price = productUpdate.Price;

			// #-#-# {B5914243-E57E-41AE-A7C8-553F2F93267B}
			// Before update
			// #-#-#

			product = await this.productRepository.UpdateAsync(product);

			// #-#-# {983B1B6C-14A7-4925-8571-D77447DF0ADA}
			// After update
			// #-#-#

            return product;
        }

        public async Task<Product> LinkCartToProductAsync(CartProduct cartProduct)
        {
            // Validation
            if (cartProduct == null) { return null; }

            // Check if product exists
            Product product = await this.productRepository.GetByIdAsync(cartProduct.ProductId);
            if (product == null)
            {
                return null;
            }

            // Check if cart exists
            Cart cart = await this.cartRepository.GetByIdAsync(cartProduct.CartId);
            if (cart == null)
            {
                return null;
            }

            // Retrieve existing link
            CartProduct cartProductLink = this.cartProductRepository.GetByProductAndCartId(cartProduct.ProductId, cartProduct.CartId);

            if (cartProductLink == null)
            {
                await this.cartProductRepository.InsertAsync(cartProduct);
            }
            else
            {
                // Mapping of fields on many-to-many
                cartProductLink.Quantity = cartProduct.Quantity;
                cartProductLink.Price = cartProduct.Price;

                await this.cartProductRepository.UpdateAsync(cartProductLink);
            }

            return await this.GetProductByIdAsync(cartProduct.ProductId);
        }

        public async Task<Product> UnlinkCartFromProductAsync(CartProduct cartProduct)
        {
            // Validation
            if (cartProduct == null) { return null; }

            // Retrieve existing link
            CartProduct cartProductLink = this.cartProductRepository.GetByProductAndCartId(cartProduct.ProductId, cartProduct.CartId);
		
            if (cartProductLink != null)
            {
                await this.cartProductRepository.DeleteAsync(cartProductLink);
            }

            return await this.GetProductByIdAsync(cartProduct.ProductId);
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
