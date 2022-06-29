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

    public class NorthwindQuery : ObjectGraphType
    {
        public NorthwindQuery(IServiceProvider provider)
        {
            var mongoDb = (MongoDbService)provider.GetService(typeof(MongoDbService));


            Field<ListGraphType<ProductType>>(
                name: "products",
                arguments: new QueryArguments(
                    new QueryArgument<ListGraphType<StringGraphType>> { Name = "productNames" },
                    new QueryArgument<ListGraphType<IntGraphType>> { Name = "categoryIds" },
                    new QueryArgument<ListGraphType<IntGraphType>> { Name = "supplierIds" }
                ),
                resolve: context =>
                {
                    var productNamesArg = context.GetArgument<string[]>("productNames");
                    var categoryIdsArg = context.GetArgument<int[]>("categoryIds");
                    var supplierIdsArg = context.GetArgument<int[]>("supplierIds");

                    var docs = mongoDb.GetProducts(null, null);
                    if (productNamesArg != null && productNamesArg.Any())
                    {
                        docs = docs.Where(r => productNamesArg.Any(p => r.productName.Contains(p))).AsQueryable();
                    }

                    if (categoryIdsArg != null && categoryIdsArg.Any())
                    {
                        docs = docs.Where(r => categoryIdsArg.Contains(r.categoryId)).AsQueryable();
                    }

                    if (supplierIdsArg != null && supplierIdsArg.Any())
                    {
                        docs = docs.Where(r => supplierIdsArg.Contains(r.supplierId)).AsQueryable();
                    }

                    return docs.ToArray();
                });

            Field<ListGraphType<OrderDetailType>>(
                name: "orderDetails",
                arguments: new QueryArguments(
                    new QueryArgument<ListGraphType<IntGraphType>> { Name = "orderDetailIds" }
                ),
                resolve: context =>
                {
                    var orderDetailIdsArg = context.GetArgument<int[]>("orderDetailIds");

                    var docs = mongoDb.GetOrderDetails(null);

                    if (orderDetailIdsArg != null && orderDetailIdsArg.Any())
                    {
                        docs = docs.Where(r => orderDetailIdsArg.Contains(r.entityId)).AsQueryable();
                    }

                    return docs.ToArray();
                });

            Field<ListGraphType<CategoryType>>(
                name: "categories",
                arguments: new QueryArguments(
                    new QueryArgument<ListGraphType<IntGraphType>> { Name = "categoryIds" }
                ),
                resolve: context =>
                {
                    var categoryIdsArg = context.GetArgument<int[]>("categoryIds");

                    var docs = mongoDb.GetCategories(null);

                    if (categoryIdsArg != null && categoryIdsArg.Any())
                    {
                        docs = docs.Where(r => categoryIdsArg.Contains(r.entityId)).AsQueryable();
                    }

                    return docs.ToArray();
                });

            Field<ListGraphType<SupplierType>>(
                name: "suppliers",
                arguments: new QueryArguments(
                    new QueryArgument<ListGraphType<IntGraphType>> { Name = "supplierIds" }
                ),
                resolve: context =>
                {
                    var supplierIdsArg = context.GetArgument<int[]>("categoryIds");

                    var docs = mongoDb.GetSuppliers(null);

                    if (supplierIdsArg != null && supplierIdsArg.Any())
                    {
                        docs = docs.Where(r => supplierIdsArg.Contains(r.entityId)).AsQueryable();
                    }

                    return docs.ToArray();
                });
        }
    }
}