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

            Field<ListGraphType<OrderType>>(
                "orders",
                resolve: context => orderRepository.GetByOrderStateId(context.Source.Id)
            );

            //// Async test
            //FieldAsync<ListGraphType<OrderType>>(
            //    "orders",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await orderRepository.GetByOrderStateIdAsync(context.Source.Id)
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
