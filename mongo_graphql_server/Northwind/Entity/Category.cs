using GraphQL.Types;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Northwind.Services;
using System.Linq;

namespace Northwind.Entity
{
    public class CategoryRaw
    {
        public ObjectId _id { get; set; }
        public int entityId { get; set; }
        public string description { get; set; }
        public string categoryName { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class Category {
        public int entityId { get; set; }
        public string description { get; set; }
        public string categoryName { get; set; }
    }



    public class CategoryType : AutoRegisteringObjectGraphType<Category>
    {
        public CategoryType()
        {
            Name = nameof(CategoryType);

            Field<ListGraphType<ProductType>>(
                "products",
                resolve: context =>
                {
                    var mongoDb = ServiceResolver.GetService<MongoDbService>();
                    return mongoDb.GetProducts(context.Source.entityId, null).ToArray();
                }
            );

            //Field(h => h.entityId);
            //Field(h => h.description);
            //Field(h => h.categoryName);
        }
    }
}