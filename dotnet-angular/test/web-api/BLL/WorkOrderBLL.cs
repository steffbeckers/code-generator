using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.BLL
{
	/// <summary>
	/// The business logic layer for WorkOrders.
	/// </summary>
    public class WorkOrderBLL
    {
        private readonly WorkOrderRepository workOrderRepository;

		/// <summary>
		/// The constructor of the WorkOrder business logic layer.
		/// </summary>
        public WorkOrderBLL(
			WorkOrderRepository workOrderRepository
		)
        {
            this.workOrderRepository = workOrderRepository;
        }

		/// <summary>
		/// Retrieves all workorders.
		/// </summary>
		public async Task<IEnumerable<WorkOrder>> GetAllWorkOrdersAsync()
        {
			// #-#-# {83B8AA9F-713A-42FB-ADE1-8A4AA43886C8}
			// Before retrieval
			// #-#-#

            return await this.workOrderRepository.GetWithLinkedEntitiesAsync();
        }

		/// <summary>
		/// Retrieves one workorder by Id.
		/// </summary>
		public async Task<WorkOrder> GetWorkOrderByIdAsync(Guid id)
        {
			// #-#-# {F838CE2A-D0FB-4F8A-A826-0D653DEECB2B}
			// Before retrieval
			// #-#-#

            return await this.workOrderRepository.GetWithLinkedEntitiesByIdAsync(id);
        }

		/// <summary>
		/// Creates a new workorder record.
		/// </summary>
        public async Task<WorkOrder> CreateWorkOrderAsync(WorkOrder workOrder)
        {
            // Validation
            if (workOrder == null) { return null; }

			// Trimming strings

			// #-#-# {D4775AF3-4BFA-496A-AA82-001028A22DD6}
			// Before creation
			// #-#-#

			workOrder = await this.workOrderRepository.InsertAsync(workOrder);

			// #-#-# {1972C619-D2F2-48FD-8474-3A69621B1F78}
			// After creation
			// #-#-#

            return workOrder;
        }

		/// <summary>
		/// Updates an existing workorder record by Id.
		/// </summary>
        public async Task<WorkOrder> UpdateWorkOrderAsync(WorkOrder workOrderUpdate)
        {
            // Validation
            if (workOrderUpdate == null) { return null; }

            // Retrieve existing
            WorkOrder workOrder = await this.workOrderRepository.GetByIdAsync(workOrderUpdate.Id);
            if (workOrder == null)
            {
                return null;
            }

			// Trimming strings

            // Mapping
            workOrder.Date = workOrderUpdate.Date;
            workOrder.AccountId = workOrderUpdate.AccountId;

			// #-#-# {B5914243-E57E-41AE-A7C8-553F2F93267B}
			// Before update
			// #-#-#

			workOrder = await this.workOrderRepository.UpdateAsync(workOrder);

			// #-#-# {983B1B6C-14A7-4925-8571-D77447DF0ADA}
			// After update
			// #-#-#

            return workOrder;
        }

		/// <summary>
		/// Deletes an existing workorder record by Id.
		/// </summary>
        public async Task<WorkOrder> DeleteWorkOrderByIdAsync(Guid workOrderId)
        {
            WorkOrder workOrder = await this.workOrderRepository.GetByIdAsync(workOrderId);

            return await this.DeleteWorkOrderAsync(workOrder);
        }

		/// <summary>
		/// Deletes an existing workorder record.
		/// </summary>
        public async Task<WorkOrder> DeleteWorkOrderAsync(WorkOrder workOrder)
        {
            // Validation
            if (workOrder == null) { return null; }

			// #-#-# {FE1A99E0-482D-455B-A8C1-3C2C11FACA58}
			// Before deletion
			// #-#-#

            await this.workOrderRepository.DeleteAsync(workOrder);

			// #-#-# {F09857C0-44E7-4E6C-B3E6-883C0D28E1A6}
			// After deletion
			// #-#-#

            return workOrder;
        }
    }
}
