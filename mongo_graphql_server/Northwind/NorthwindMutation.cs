using System;
using System.Linq;
using GraphQL;
using GraphQL.Types;
using Northwind.Entity;
using Northwind.Services;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Northwind
{

    public class NorthwindMutation : ObjectGraphType<object>
    {
        public NorthwindMutation(IServiceProvider provider)
        {
            Name = "Mutation";

            Field<CategoryType>(
                "createCategory",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<CategoryInputType>> { Name = "newCategory" }
                ),
                resolve: context =>
                {
                    var cat = context.GetArgument<Category>("newCategory");

                    var mongoDb = ServiceResolver.GetService<MongoDbService>();

                    mongoDb.MutateCategory(cat);

                    return cat;
                });
        }
    }
}