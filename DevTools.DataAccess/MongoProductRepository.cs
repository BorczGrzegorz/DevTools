using DevTools.Application.Abstract;
using DevTools.Application.Models;
using DevTools.Application.Models.Dto;
using DevTools.Infrastructure.Exceptions;
using DevTools.Infrastructure.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevTools.DataAccess
{
    public class MongoProductRepository : IProductRepository,
                                          IProductQuery,
                                          IAddressesQuery,
                                          IProjectQuery,
                                          IMachineQuery
    {
        private readonly MongoDataContext _context;

        public MongoProductRepository(MongoDataContext mongoDataContext)
        {
            _context = mongoDataContext ?? throw new ArgumentNullException(nameof(mongoDataContext));
        }

        public bool Exist(string name)
        {
            return _context.Products.AsQueryable().Any(x => x.Name == name);
        }

        public Product Get(ProductId productId)
        {
            return _context.Products.AsQueryable().Where(x => x.Id == productId).SingleOrDefault();
        }

        public Product Get(ProjectId projectId)
        {
            FieldDefinition<Product> projecsField = nameof(Product.Projects).ToFieldName();
            FilterDefinition<Project> filterProjects = Builders<Project>.Filter.Eq(x => x.Id, projectId);
            var result = _context.Products.Find(Builders<Product>.Filter.ElemMatch(projecsField, filterProjects));
            return result.SingleOrDefault();
        }

        MachineDto[] IMachineQuery.Get(ProductId productId)
        {
            ProductDto product = _context.ProductDto.AsQueryable().SingleOrDefault(x => x.Id == productId);
            if (product == null)
            {
                throw new EntityNotEixtException("product does not exist");
            }

            return product.Machines.ToArray();
        }

        public AddressDto[] GetAddresses(ProjectId projectId)
        {
            return GetProject(projectId).Addresses;
        }

        public ProjectDto GetProject(ProjectId projectId)
        {
            ProductDto product = _context
                                 .ProductDto
                                 .AsQueryable()
                                 .Where(x => x.Projects.Any(y => y.Id == projectId))
                                 .SingleOrDefault();

            if (product == null)
            {
                // TODO change to EntityNotExistException
                throw new ArgumentException("Project does not exist");
            }

            return product.Projects.Single(x => x.Id == projectId);
        }

        public ProjectDto[] GetProjects(ProductId productId)
        {
            ProductDto product = _context
                                 .ProductDto
                                 .AsQueryable()
                                 .Where(x => x.Id == productId)
                                 .SingleOrDefault();

            if (product == null)
            {
                // TODO change to EntityNotExistException
                throw new ArgumentException("Project does not exist");
            }

            return product.Projects;
        }

        public List<ProductDto> GetAll()
        {
            return _context.ProductDto
                            .AsQueryable()
                            .ToList();
        }

        ProductDto IProductQuery.Get(ProductId id) => _context.ProductDto.AsQueryable().Single(x => x.Id == id);

        public void Save(Product product)
        {
            _context.Products.InsertOne(product);
        }

        public void Update(Product product)
        {
            var equalFilter = Builders<Product>.Filter.Eq(x => x.Id, product.Id);
            _context.Products.ReplaceOne(equalFilter, product);
        }
    }
}
