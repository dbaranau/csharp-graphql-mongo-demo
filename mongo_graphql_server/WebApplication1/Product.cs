using GraphQL.Types;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication1
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
    }

    public class ProductType : AutoRegisteringObjectGraphType<Product>
    {
        public ProductType()
        {
            Name = nameof(ProductType);
        }
    }
}