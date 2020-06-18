using GraphQL;
using GraphQL.Types;

namespace RJM.API.GraphQL
{
    public class RJMSchema : Schema
    {
        public RJMSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<RJMQuery>();
            Mutation = resolver.Resolve<RJMMutation>();
        }

        // #-#-#
        // Test
    }
}
