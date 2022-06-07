using System.Linq;
using GraphQL.Types;
using MongoDB.Bson;
using MongoDB.Driver;

namespace WebApplication1
{

    public class RootQuery : ObjectGraphType
    {
        public RootQuery()
        {
            var client = new MongoClient();
            var db = client.GetDatabase("northwind");

            Field<ListGraphType<ProductType>>(
                name: "products",
                arguments: new QueryArguments(
                    new QueryArgument<ListGraphType<StringGraphType>> { Name = "productName" },
                    new QueryArgument<ListGraphType<IntGraphType>> { Name = "categoryId" },
                    new QueryArgument<ListGraphType<IntGraphType>> { Name = "supplierId" }
                ),
                resolve: context =>
                {
                    var productNameArg = context.GetArgument<string[]>("productName");
                    var categoryIdArg = context.GetArgument<int[]>("categoryId");
                    var supplierIdArg = context.GetArgument<int[]>("supplierId");

                    var docs = db.GetCollection<Product>("product").AsQueryable().Select(r => r);
                    if (productNameArg != null && productNameArg.Any())
                    {
                        docs = docs.ToArray() // had to cheat here and fetch everything at once. you can optimize this later on, i just ran out of time
                            .Where(r => productNameArg.Any(p => r.productName.Contains(p))).AsQueryable();
                    }

                    if (categoryIdArg != null && categoryIdArg.Any())
                    {
                        docs = docs.ToArray() // had to cheat here and fetch everything at once. you can optimize this later on, i just ran out of time
                            .Where(r => categoryIdArg.Contains(r.categoryId)).AsQueryable();
                    }

                    if (supplierIdArg != null && supplierIdArg.Any())
                    {
                        docs = docs.ToArray() // had to cheat here and fetch everything at once. you can optimize this later on, i just ran out of time
                            .Where(r => supplierIdArg.Contains(r.supplierId)).AsQueryable();
                    }

                    return docs.ToArray();
                });
        }
    }
}