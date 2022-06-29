using GraphQL.Types;
using MongoDB.Bson.Serialization.Attributes;
using Northwind.Services;
using System.Linq;

namespace Northwind.Entity
{
    [BsonIgnoreExtraElements]
    public class Supplier
    {
        public int entityId { get; set; }
        public string fax { get; set; }
        public string city { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string region { get; set; }
        public string address { get; set; }
        public string country { get; set; }
        public string HomePage { get; set; }
        public string postalCode { get; set; }
        public string companyName { get; set; }
        public string contactName { get; set; }
        public string contactTitle { get; set; }
    }
    public class SupplierType : AutoRegisteringObjectGraphType<Supplier>
    {
        public SupplierType()
        {
            Name = nameof(SupplierType);

            Field<ListGraphType<ProductType>>(
                "products",
                resolve: context =>
                {
                    var mongoDb = ServiceResolver.GetService<MongoDbService>();
                    return mongoDb.GetProducts(context.Source.entityId, null).ToArray();
                }
            );

            //Field(h => h.entityId);
            //Field(h => h.city);
            //Field(h => h.fax);
            //Field(h => h.email);
            //Field(h => h.phone);
            //Field(h => h.region);
            //Field(h => h.address);
            //Field(h => h.HomePage);
            //Field(h => h.postalCode);
            //Field(h => h.companyName);
            //Field(h => h.contactName);
            //Field(h => h.contactTitle);
        }
    }
}