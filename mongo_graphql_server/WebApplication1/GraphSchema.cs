using GraphQL.Types;

namespace WebApplication1
{
    public class GraphSchema : Schema
    {
        public GraphSchema()
        {
            Query = new RootQuery();
        }
    }
}