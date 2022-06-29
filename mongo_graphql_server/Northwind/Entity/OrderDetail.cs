using GraphQL.Types;
using Northwind.Services;
using MongoDB.Bson.Serialization.Attributes;
using System.Linq;

namespace Northwind.Entity
{
    [BsonIgnoreExtraElements]
    public class OrderDetail
    {
        public int entityId { get; set; }
        public int orderId { get; set; }
        
        public decimal discount { get; set; }
        public decimal unitPrice { get; set; }
        public int quantity { get; set; }
        public int productId { get; set; }
    }


    public class OrderDetailType : AutoRegisteringObjectGraphType<OrderDetail>
    {
        public OrderDetailType()
        {
            Name = nameof(OrderDetailType);

            Field<ProductType>(
                "product",
                resolve: context =>
                {
                    var mongoDb = ServiceResolver.GetService<MongoDbService>();
                    return mongoDb.GetProduct(context.Source.productId);
                }
            );
        }
    }
}