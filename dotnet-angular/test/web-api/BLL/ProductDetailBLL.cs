using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.BLL
{
	/// <summary>
	/// The business logic layer for ProductDetails.
	/// </summary>
    public class ProductDetailBLL
    {
        private readonly ProductDetailRepository productDetailRepository;

		/// <summary>
		/// The constructor of the ProductDetail business logic layer.
		/// </summary>
        public ProductDetailBLL(
			ProductDetailRepository productDetailRepository
		)
        {
            this.productDetailRepository = productDetailRepository;
        }

		/// <summary>
		/// Retrieves all productdetails.
		/// </summary>
		public async Task<IEnumerable<ProductDetail>> GetAllProductDetailsAsync()
        {
			// #-#-# {83B8AA9F-713A-42FB-ADE1-8A4AA43886C8}
            // Before retrieval
            // #-#-#

            return await this.productDetailRepository.GetWithLinkedEntitiesAsync();
        }

		/// <summary>
		/// Retrieves one productdetail by Id.
		/// </summary>
		public async Task<ProductDetail> GetProductDetailByIdAsync(Guid id)
        {
			// #-#-# {F838CE2A-D0FB-4F8A-A826-0D653DEECB2B}
            // Before retrieval
            // #-#-#

            return await this.productDetailRepository.GetWithLinkedEntitiesByIdAsync(id);
        }

		/// <summary>
		/// Creates a new productdetail record.
		/// </summary>
        public async Task<ProductDetail> CreateProductDetailAsync(ProductDetail productDetail)
        {
            // Validation
            if (productDetail == null) { return null; }

			// Trimming strings
            if (!string.IsNullOrEmpty(productDetail.Comment))
                productDetail.Comment = productDetail.Comment.Trim();

			// #-#-# {D4775AF3-4BFA-496A-AA82-001028A22DD6}
            // Before creation
            // #-#-#

			productDetail = await this.productDetailRepository.InsertAsync(productDetail);

			// #-#-# {1972C619-D2F2-48FD-8474-3A69621B1F78}
            // After creation
            // #-#-#

            return productDetail;
        }

		/// <summary>
		/// Updates an existing productdetail record by Id.
		/// </summary>
        public async Task<ProductDetail> UpdateProductDetailAsync(ProductDetail productDetailUpdate)
        {
            // Validation
            if (productDetailUpdate == null) { return null; }

            // Retrieve existing
            ProductDetail productDetail = await this.productDetailRepository.GetByIdAsync(productDetailUpdate.Id);
            if (productDetail == null)
            {
                return null;
            }

			// Trimming strings
            if (!string.IsNullOrEmpty(productDetailUpdate.Comment))
                productDetailUpdate.Comment = productDetailUpdate.Comment.Trim();

            // Mapping
            productDetail.Comment = productDetailUpdate.Comment;
            productDetail.ProductId = productDetailUpdate.ProductId;

			// #-#-# {B5914243-E57E-41AE-A7C8-553F2F93267B}
            // Before update
            // #-#-#

			productDetail = await this.productDetailRepository.UpdateAsync(productDetail);

			// #-#-# {983B1B6C-14A7-4925-8571-D77447DF0ADA}
            // After update
            // #-#-#

            return productDetail;
        }

		/// <summary>
		/// Deletes an existing productdetail record by Id.
		/// </summary>
        public async Task<ProductDetail> DeleteProductDetailByIdAsync(Guid productDetailId)
        {
            ProductDetail productDetail = await this.productDetailRepository.GetByIdAsync(productDetailId);

            return await this.DeleteProductDetailAsync(productDetail);
        }

		/// <summary>
		/// Deletes an existing productdetail record.
		/// </summary>
        public async Task<ProductDetail> DeleteProductDetailAsync(ProductDetail productDetail)
        {
            // Validation
            if (productDetail == null) { return null; }

			// #-#-# {FE1A99E0-482D-455B-A8C1-3C2C11FACA58}
            // Before deletion
            // #-#-#

            await this.productDetailRepository.DeleteAsync(productDetail);

			// #-#-# {F09857C0-44E7-4E6C-B3E6-883C0D28E1A6}
            // After deletion
            // #-#-#

            return productDetail;
        }
    }
}
