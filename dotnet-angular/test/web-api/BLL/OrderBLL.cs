using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.BLL
{
	/// <summary>
	/// The business logic layer for Orders.
	/// </summary>
    public class OrderBLL
    {
        private readonly OrderRepository orderRepository;

		/// <summary>
		/// The constructor of the Order business logic layer.
		/// </summary>
        public OrderBLL(
			OrderRepository orderRepository
		)
        {
            this.orderRepository = orderRepository;
        }

		/// <summary>
		/// Retrieves all orders.
		/// </summary>
		public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
			// #-#-# {83B8AA9F-713A-42FB-ADE1-8A4AA43886C8}
			// Before retrieval
			// #-#-#

            return await this.orderRepository.GetWithLinkedEntitiesAsync();
        }

		/// <summary>
		/// Retrieves one order by Id.
		/// </summary>
		public async Task<Order> GetOrderByIdAsync(Guid id)
        {
			// #-#-# {F838CE2A-D0FB-4F8A-A826-0D653DEECB2B}
			// Before retrieval
			// #-#-#

            return await this.orderRepository.GetWithLinkedEntitiesByIdAsync(id);
        }

		/// <summary>
		/// Creates a new order record.
		/// </summary>
        public async Task<Order> CreateOrderAsync(Order order)
        {
            // Validation
            if (order == null) { return null; }

			// Trimming strings
            if (!string.IsNullOrEmpty(order.Number))
                order.Number = order.Number.Trim();
            if (!string.IsNullOrEmpty(order.Description))
                order.Description = order.Description.Trim();

			// #-#-# {D4775AF3-4BFA-496A-AA82-001028A22DD6}
			// Before creation
			// #-#-#

			order = await this.orderRepository.InsertAsync(order);

			// #-#-# {1972C619-D2F2-48FD-8474-3A69621B1F78}
			// After creation
			// #-#-#

            return order;
        }

		/// <summary>
		/// Updates an existing order record by Id.
		/// </summary>
        public async Task<Order> UpdateOrderAsync(Order orderUpdate)
        {
            // Validation
            if (orderUpdate == null) { return null; }

            // Retrieve existing
            Order order = await this.orderRepository.GetByIdAsync(orderUpdate.Id);
            if (order == null)
            {
                return null;
            }

			// Trimming strings
            if (!string.IsNullOrEmpty(orderUpdate.Number))
                orderUpdate.Number = orderUpdate.Number.Trim();
            if (!string.IsNullOrEmpty(orderUpdate.Description))
                orderUpdate.Description = orderUpdate.Description.Trim();

            // Mapping
            order.Number = orderUpdate.Number;
            order.Description = orderUpdate.Description;
            order.TotalPrice = orderUpdate.TotalPrice;

			// #-#-# {B5914243-E57E-41AE-A7C8-553F2F93267B}
			// Before update
			// #-#-#

			order = await this.orderRepository.UpdateAsync(order);

			// #-#-# {983B1B6C-14A7-4925-8571-D77447DF0ADA}
			// After update
			// #-#-#

            return order;
        }

		/// <summary>
		/// Deletes an existing order record by Id.
		/// </summary>
        public async Task<Order> DeleteOrderByIdAsync(Guid orderId)
        {
            Order order = await this.orderRepository.GetByIdAsync(orderId);

            return await this.DeleteOrderAsync(order);
        }

		/// <summary>
		/// Deletes an existing order record.
		/// </summary>
        public async Task<Order> DeleteOrderAsync(Order order)
        {
            // Validation
            if (order == null) { return null; }

			// #-#-# {FE1A99E0-482D-455B-A8C1-3C2C11FACA58}
			// Before deletion
			// #-#-#

            await this.orderRepository.DeleteAsync(order);

			// #-#-# {F09857C0-44E7-4E6C-B3E6-883C0D28E1A6}
			// After deletion
			// #-#-#

            return order;
        }
    }
}
