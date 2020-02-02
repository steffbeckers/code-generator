using GraphQL;
using GraphQL.Types;

namespace Test.API.GraphQL
{
    public class TestSchema : Schema
    {
        public TestSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<TestQuery>();
            Mutation = resolver.Resolve<TestMutation>();
        }
    }
}
