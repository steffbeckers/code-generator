using GraphQL.Types;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.GraphQL.Types
{
    public class TodoType : ObjectGraphType<Todo>
    {
        public TodoType(
			TodoRepository todoRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Title);
            Field(x => x.DueDate, nullable: true);

        }
    }
}
