using Northwind.Entity;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;

namespace Northwind.Services
{
    public class MongoDbService
    {
        private IMongoDatabase database;
        public MongoDbService()
        {
            var client = new MongoClient();
            database = client.GetDatabase("northwind");
        }
        public IQueryable<OrderDetail> GetOrderDetails(int? productId)
        {
            var docs = database. GetCollection<OrderDetail>("orderDetail").AsQueryable().Select(r => r);
            
            if (productId.HasValue)
                docs = docs.Where(r => r.productId == productId.Value);
            
            return docs;
        }

        public IQueryable<Product> GetProducts(int? supplierId, int? categoryId)
        {
            var docs = database. GetCollection<Product>("product").AsQueryable().Select(r => r);
            
            if (supplierId.HasValue)
                docs = docs.Where(r => r.supplierId == supplierId.Value);

            if (categoryId.HasValue)
                docs = docs.Where(r => r.categoryId == categoryId.Value);
            return docs;
        }

        public Product GetProduct(int id)
        {
            var doc = database.GetCollection<Product>("product").AsQueryable().Where(r => r.entityId == id).FirstOrDefault();
            return doc;
        }

        public IQueryable<Category> GetCategories(int[] ids)
        {
            var docs = database.GetCollection<Category>("category").AsQueryable().Select(r => r);

            if (ids != null && ids.Any())
            {
                docs = docs.Where(r => ids.Contains(r.entityId)).AsQueryable();
            }

            return docs;
        }


        public bool MutateCategory(Category cat)
        {
            var doc = database.GetCollection<CategoryRaw>("category").AsQueryable().Where(r => r.entityId == cat.entityId).Select(r => r).FirstOrDefault();
            if (doc == null)
            {
                database.GetCollection<Category>("category").InsertOne(cat);

            } else
            {
                var builder = Builders<CategoryRaw>.Filter;
                var filter = builder.Eq("_id", doc._id);

                doc.description = cat.description;
                doc.categoryName = cat.categoryName;

                var result = database.GetCollection<CategoryRaw>("category").ReplaceOne(filter, doc);
                return result.ModifiedCount > 0;
            }
            return true;
        }


        public IQueryable<Supplier> GetSuppliers(int[] ids)
        {
            var docs = database.GetCollection<Supplier>("supplier").AsQueryable().Select(r => r);

            if (ids != null && ids.Any())
            {
                docs = docs.Where(r => ids.Contains(r.entityId)).AsQueryable();
            }

            return docs;
        }

    }
}
