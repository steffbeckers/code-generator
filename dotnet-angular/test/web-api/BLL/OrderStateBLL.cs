using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.BLL
{
	/// <summary>
	/// The business logic layer for OrderStates.
	/// </summary>
    public class OrderStateBLL
    {
        private readonly OrderStateRepository orderStateRepository;

		/// <summary>
		/// The constructor of the OrderState business logic layer.
		/// </summary>
        public OrderStateBLL(
			OrderStateRepository orderStateRepository
		)
        {
            this.orderStateRepository = orderStateRepository;
        }

		/// <summary>
		/// Retrieves all orderstates.
		/// </summary>
		public async Task<IEnumerable<OrderState>> GetAllOrderStatesAsync()
        {
			// #-#-# {83B8AA9F-713A-42FB-ADE1-8A4AA43886C8}
			// Before retrieval
			// #-#-#

            return await this.orderStateRepository.GetWithLinkedEntitiesAsync();
        }

		/// <summary>
		/// Retrieves one orderstate by Id.
		/// </summary>
		public async Task<OrderState> GetOrderStateByIdAsync(Guid id)
        {
			// #-#-# {F838CE2A-D0FB-4F8A-A826-0D653DEECB2B}
			// Before retrieval
			// #-#-#

            return await this.orderStateRepository.GetWithLinkedEntitiesByIdAsync(id);
        }

		/// <summary>
		/// Creates a new orderstate record.
		/// </summary>
        public async Task<OrderState> CreateOrderStateAsync(OrderState orderState)
        {
            // Validation
            if (orderState == null) { return null; }

			// Trimming strings
            if (!string.IsNullOrEmpty(orderState.Name))
                orderState.Name = orderState.Name.Trim();
            if (!string.IsNullOrEmpty(orderState.DisplayName))
                orderState.DisplayName = orderState.DisplayName.Trim();

			// #-#-# {D4775AF3-4BFA-496A-AA82-001028A22DD6}
			// Before creation
			// #-#-#

			orderState = await this.orderStateRepository.InsertAsync(orderState);

			// #-#-# {1972C619-D2F2-48FD-8474-3A69621B1F78}
			// After creation
			// #-#-#

            return orderState;
        }

		/// <summary>
		/// Updates an existing orderstate record by Id.
		/// </summary>
        public async Task<OrderState> UpdateOrderStateAsync(OrderState orderStateUpdate)
        {
            // Validation
            if (orderStateUpdate == null) { return null; }

            // Retrieve existing
            OrderState orderState = await this.orderStateRepository.GetByIdAsync(orderStateUpdate.Id);
            if (orderState == null)
            {
                return null;
            }

			// Trimming strings
            if (!string.IsNullOrEmpty(orderStateUpdate.Name))
                orderStateUpdate.Name = orderStateUpdate.Name.Trim();
            if (!string.IsNullOrEmpty(orderStateUpdate.DisplayName))
                orderStateUpdate.DisplayName = orderStateUpdate.DisplayName.Trim();

            // Mapping
            orderState.Name = orderStateUpdate.Name;
            orderState.DisplayName = orderStateUpdate.DisplayName;

			// #-#-# {B5914243-E57E-41AE-A7C8-553F2F93267B}
			// Before update
			// #-#-#

			orderState = await this.orderStateRepository.UpdateAsync(orderState);

			// #-#-# {983B1B6C-14A7-4925-8571-D77447DF0ADA}
			// After update
			// #-#-#

            return orderState;
        }

		/// <summary>
		/// Deletes an existing orderstate record by Id.
		/// </summary>
        public async Task<OrderState> DeleteOrderStateByIdAsync(Guid orderStateId)
        {
            OrderState orderState = await this.orderStateRepository.GetByIdAsync(orderStateId);

            return await this.DeleteOrderStateAsync(orderState);
        }

		/// <summary>
		/// Deletes an existing orderstate record.
		/// </summary>
        public async Task<OrderState> DeleteOrderStateAsync(OrderState orderState)
        {
            // Validation
            if (orderState == null) { return null; }

			// #-#-# {FE1A99E0-482D-455B-A8C1-3C2C11FACA58}
			// Before deletion
			// #-#-#

            await this.orderStateRepository.DeleteAsync(orderState);

			// #-#-# {F09857C0-44E7-4E6C-B3E6-883C0D28E1A6}
			// After deletion
			// #-#-#

            return orderState;
        }
    }
}
