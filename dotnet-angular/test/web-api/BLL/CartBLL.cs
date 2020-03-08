using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.BLL
{
	/// <summary>
	/// The business logic layer for Carts.
	/// </summary>
    public class CartBLL
    {
        private readonly CartRepository cartRepository;
        private readonly ProductRepository productRepository;
        private readonly CartProductRepository cartProductRepository;

		/// <summary>
		/// The constructor of the Cart business logic layer.
		/// </summary>
        public CartBLL(
			CartRepository cartRepository,
            ProductRepository productRepository,
			CartProductRepository cartProductRepository
		)
        {
            this.cartRepository = cartRepository;
            this.productRepository = productRepository;
			this.cartProductRepository = cartProductRepository;
        }

		/// <summary>
		/// Retrieves all carts.
		/// </summary>
		public async Task<IEnumerable<Cart>> GetAllCartsAsync()
        {
			// #-#-# {83B8AA9F-713A-42FB-ADE1-8A4AA43886C8}
			// Before retrieval
			// #-#-#

            return await this.cartRepository.GetWithLinkedEntitiesAsync();
        }

		/// <summary>
		/// Retrieves one cart by Id.
		/// </summary>
		public async Task<Cart> GetCartByIdAsync(Guid id)
        {
			// #-#-# {F838CE2A-D0FB-4F8A-A826-0D653DEECB2B}
			// Before retrieval
			// #-#-#

            return await this.cartRepository.GetWithLinkedEntitiesByIdAsync(id);
        }

		/// <summary>
		/// Creates a new cart record.
		/// </summary>
        public async Task<Cart> CreateCartAsync(Cart cart)
        {
            // Validation
            if (cart == null) { return null; }

			// Trimming strings
            if (!string.IsNullOrEmpty(cart.Name))
                cart.Name = cart.Name.Trim();

			// #-#-# {D4775AF3-4BFA-496A-AA82-001028A22DD6}
			// Before creation
			// #-#-#

			cart = await this.cartRepository.InsertAsync(cart);

			// #-#-# {1972C619-D2F2-48FD-8474-3A69621B1F78}
			// After creation
			// #-#-#

            return cart;
        }

		/// <summary>
		/// Updates an existing cart record by Id.
		/// </summary>
        public async Task<Cart> UpdateCartAsync(Cart cartUpdate)
        {
            // Validation
            if (cartUpdate == null) { return null; }

            // Retrieve existing
            Cart cart = await this.cartRepository.GetByIdAsync(cartUpdate.Id);
            if (cart == null)
            {
                return null;
            }

			// Trimming strings
            if (!string.IsNullOrEmpty(cartUpdate.Name))
                cartUpdate.Name = cartUpdate.Name.Trim();

            // Mapping
            cart.Name = cartUpdate.Name;
            cart.UserId = cartUpdate.UserId;

			// #-#-# {B5914243-E57E-41AE-A7C8-553F2F93267B}
			// Before update
			// #-#-#

			cart = await this.cartRepository.UpdateAsync(cart);

			// #-#-# {983B1B6C-14A7-4925-8571-D77447DF0ADA}
			// After update
			// #-#-#

            return cart;
        }

        public async Task<Cart> LinkProductToCartAsync(CartProduct cartProduct)
        {
            // Validation
            if (cartProduct == null) { return null; }

            // Check if cart exists
            Cart cart = await this.cartRepository.GetByIdAsync(cartProduct.CartId);
            if (cart == null)
            {
                return null;
            }

            // Check if product exists
            Product product = await this.productRepository.GetByIdAsync(cartProduct.ProductId);
            if (product == null)
            {
                return null;
            }

            // Retrieve existing link
            CartProduct cartProductLink = this.cartProductRepository.GetByCartAndProductId(cartProduct.CartId, cartProduct.ProductId);

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

            return await this.GetCartByIdAsync(cartProduct.CartId);
        }

        public async Task<Cart> UnlinkProductFromCartAsync(CartProduct cartProduct)
        {
            // Validation
            if (cartProduct == null) { return null; }

            // Retrieve existing link
            CartProduct cartProductLink = this.cartProductRepository.GetByCartAndProductId(cartProduct.CartId, cartProduct.ProductId);
		
            if (cartProductLink != null)
            {
                await this.cartProductRepository.DeleteAsync(cartProductLink);
            }

            return await this.GetCartByIdAsync(cartProduct.CartId);
        }

		/// <summary>
		/// Deletes an existing cart record by Id.
		/// </summary>
        public async Task<Cart> DeleteCartByIdAsync(Guid cartId)
        {
            Cart cart = await this.cartRepository.GetByIdAsync(cartId);

            return await this.DeleteCartAsync(cart);
        }

		/// <summary>
		/// Deletes an existing cart record.
		/// </summary>
        public async Task<Cart> DeleteCartAsync(Cart cart)
        {
            // Validation
            if (cart == null) { return null; }

			// #-#-# {FE1A99E0-482D-455B-A8C1-3C2C11FACA58}
			// Before deletion
			// #-#-#

            await this.cartRepository.DeleteAsync(cart);

			// #-#-# {F09857C0-44E7-4E6C-B3E6-883C0D28E1A6}
			// After deletion
			// #-#-#

            return cart;
        }
    }
}
