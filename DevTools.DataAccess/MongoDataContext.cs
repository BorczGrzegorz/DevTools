using DevTools.Application.Models;
using DevTools.Application.Models.Dto;
using DevTools.DataAccess.Mappings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.DataAccess
{
    public class MongoDataContext
    {
        private readonly IMongoDatabase _database;
        [Obsolete()]
        public IMongoDatabase Database => _database;
        public IMongoCollection<Product> Products { get; private set; }
        public IMongoCollection<ProductDto> ProductDto { get; private set; }

        public MongoDataContext(string connectionString, string databaseName)
        {
            IMongoClient client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
            Products = GetCollection<Product>(nameof(Products));
            ProductDto = GetCollection<ProductDto>(nameof(Products));
        }

        static MongoDataContext()
        {
            ProductMapping.Register();
            MachineMapping.Register();
            ProjectMapping.Register();
            AddressMapping.Register();
        }

        private IMongoCollection<TEntity> GetCollection<TEntity>(string collectionName)
        {
            return _database.GetCollection<TEntity>(collectionName);
        }
    }
}
