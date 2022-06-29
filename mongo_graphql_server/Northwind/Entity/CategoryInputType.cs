using GraphQL.Types;


namespace Northwind.Entity
{
    public class CategoryInputType : InputObjectGraphType
    {
        public CategoryInputType()
        {
            Name = "CategoryInput";

            Field< NonNullGraphType<IntGraphType>>("entityId");
            Field<NonNullGraphType<StringGraphType>>("categoryName");
            Field<StringGraphType>("description");
        }
    }
}