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

            Field<ListGraphType<OrderStateType>>(
                "orderStates",
                resolve: context => orderStateRepository.GetByOrderId(context.Source.Id)
            );

            //// Async test
            //FieldAsync<ListGraphType<OrderStateType>>(
            //    "orderStates",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await orderStateRepository.GetByOrderIdAsync(context.Source.Id)
            //        );
            //    }
            //);

            Field(x => x.CreatedByUserId, type: typeof(IdGraphType));
            // TODO: Field(x => x.CreatedByUser, type: typeof(UserType));
            Field(x => x.ModifiedByUserId, type: typeof(IdGraphType));
            // TODO: Field(x => x.ModifiedByUser, type: typeof(UserType));
        }
    }
}
