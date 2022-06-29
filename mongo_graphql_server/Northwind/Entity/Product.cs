using GraphQL.Types;
using Northwind.Services;
using MongoDB.Bson.Serialization.Attributes;
using System.Linq;

namespace Northwind.Entity
{
    [BsonIgnoreExtraElements]
    public class Product
    {
        public int entityId { get; set; }
        public decimal unitPrice { get; set; }
        public int categoryId { get; set; }
        public int supplierId { get; set; }
        public string productName { get; set; }
        public int discontinued { get; set; }
        public int? reorderLevel { get; set; }
        public int? unitsInStock { get; set; }
        public int? unitsOnOrder { get; set; }
        public int? quantityPerUnit { get; set; }

        //public Category category { get; set; }
    }


    public class ProductType : AutoRegisteringObjectGraphType<Product>
    {
        public ProductType()
        {
            Name = nameof(ProductType);

            Field<CategoryType>(
                "category",
                resolve: context =>
                {
                    var mongoDb = ServiceResolver.GetService<MongoDbService>();
                    return mongoDb.GetCategories(new int[] { context.Source.categoryId }).FirstOrDefault();
                }
            );

            Field<SupplierType>(
                "supplier",
                resolve: context =>
                {
                    var mongoDb = ServiceResolver.GetService<MongoDbService>();
                    return mongoDb.GetSuppliers(new int[] { context.Source.supplierId }).FirstOrDefault();
                }
            );

            Field<ListGraphType<OrderDetailType>>(
                "orderDetails",
                resolve: context =>
                {
                    var mongoDb = ServiceResolver.GetService<MongoDbService>();
                    return mongoDb.GetOrderDetails(context.Source.entityId).ToArray();
                }
            );
            //Field(h => h.entityId).Description("entityId");
            //Field(h => h.unitPrice);
            //Field(h => h.categoryId);
            //Field(h => h.supplierId);
            //Field(h => h.productName);
            //Field(h => h.discontinued);

            ////Field<IntGraphType>(h => h.reorderLevel.HasValue ? h.reorderLevel.Value : 0, nullable:true);
            ////Field(h => h.unitsInStock);
            ////Field(h => h.unitsOnOrder);
            ////Field(h => h.quantityPerUnit);

            ////Field(h => h.category);
        }
    }
}