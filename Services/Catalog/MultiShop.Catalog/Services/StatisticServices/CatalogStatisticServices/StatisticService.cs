using MongoDB.Bson;
using MongoDB.Driver;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.StatisticServices.CatalogStatisticServices
{
    public class StatisticService : IStatisticService
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMongoCollection<Brand> _brandCollection;

        public StatisticService(IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(_databaseSettings.ProductCollectionName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
            _brandCollection = database.GetCollection<Brand>(_databaseSettings.BrandCollectionName);
        }

        public async Task<long> GetBrandCount()
        {
            return await _brandCollection.CountDocumentsAsync(FilterDefinition<Brand>.Empty);
        }

        public async Task<long> GetCategoryCount()
        {
            return await _categoryCollection.CountDocumentsAsync(FilterDefinition<Category>.Empty);
        }

        public async Task<string> GetMaxPriceProductName()
        {
            var filter = Builders<Product>.Filter.Empty;
            var sort = Builders<Product>.Sort.Descending(p => p.ProductPrice);
            var result = Builders<Product>.Projection.Include(p => p.ProductName).Exclude("ProductId");
            var product = await _productCollection.Find(filter).Sort(sort).Project(result).FirstOrDefaultAsync();
            return product.GetValue("ProductName").AsString;
        }

        public async Task<string> GetMinPriceProductName()
        {
            var filter = Builders<Product>.Filter.Empty;
            var sort = Builders<Product>.Sort.Ascending(p => p.ProductPrice);
            var result = Builders<Product>.Projection.Include(p => p.ProductName).Exclude("ProductId");
            var product = await _productCollection.Find(filter).Sort(sort).Project(result).FirstOrDefaultAsync();
            return product.GetValue("ProductName").AsString;
        }

        public async Task<decimal> GetProductAvgPrice()
        {
            var pipeline = new BsonDocument[]
            {
        new BsonDocument
        {
            { "$group", new BsonDocument
                {
                    { "_id", BsonNull.Value },
                    { "avgPrice", new BsonDocument { { "$avg", "$ProductPrice" } } }
                }
            }
        }
            };

            var result = await _productCollection.AggregateAsync<BsonDocument>(pipeline);
            var document = await result.FirstOrDefaultAsync();

            if (document == null || document["avgPrice"].IsBsonNull)
                return 0;

            return document["avgPrice"].ToDecimal();
        }

        public async Task<long> GetProductCount()
        {
            return await _productCollection.CountDocumentsAsync(FilterDefinition<Product>.Empty);
        }
    }
}
