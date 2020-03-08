using GraphQL.Types;
using System;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.GraphQL.Types
{
    public class OrderStateType : ObjectGraphType<OrderState>
    {
        public OrderStateType(
            OrderRepository orderRepository,
			OrderStateRepository orderStateRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Name);
            Field(x => x.DisplayName, nullable: true);

            Field<OrderType>(
                "order",
                resolve: context =>
                {
                    if (context.Source.OrderId != null)
                        return orderRepository.GetById((Guid)context.Source.OrderId);
                    return null;
                }
            );

            //// Async test
            //FieldAsync<OrderType>(
            //    "order",
            //    resolve: async context =>
            //    {
            //        if (context.Source.OrderId != null) {
            //            return await context.TryAsyncResolve(
            //                async c => await orderRepository.GetByIdAsync((Guid)context.Source.OrderId)
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
