using GraphQL.Types;
using System;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.GraphQL.Types
{
    public class OrderType : ObjectGraphType<Order>
    {
        public OrderType(
            OrderStateRepository orderStateRepository,
			OrderRepository orderRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Number, nullable: true);
            Field(x => x.Description, nullable: true);
            Field(x => x.TotalPrice, nullable: true);

            Field<OrderStateType>(
                "orderState",
                resolve: context =>
                {
                    if (context.Source.OrderStateId != null)
                        return orderStateRepository.GetById((Guid)context.Source.OrderStateId);
                    return null;
                }
            );

            //// Async test
            //FieldAsync<OrderStateType>(
            //    "orderState",
            //    resolve: async context =>
            //    {
            //        if (context.Source.OrderStateId != null) {
            //            return await context.TryAsyncResolve(
            //                async c => await orderStateRepository.GetByIdAsync((Guid)context.Source.OrderStateId)
            //            );
            //        }
            //        
            //        return null;
            //    }
            //);

            Field(x => x.CreatedByUserId, type: typeof(IdGraphType));
            // TODO: Field(x => x.CreatedByUser, type: typeof(UserType));
            Field(x => x.ModifiedByUserId, type: typeof(IdGraphType));
            // TODO: Field(x => x.ModifiedByUser, type: typeof(UserType));
        }
    }
}
