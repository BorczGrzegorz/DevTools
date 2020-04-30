using DevTools.Application.Abstract;
using DevTools.Application.Models;
using DevTools.Application.Models.Dto;
using DevTools.Infrastructure.Exceptions;
using DevTools.Infrastructure.Models;
using Extensions.Infrastructure.Exceptions;
using Extensions.Infrastructure.Models;
using System;
using System.Linq;

namespace DevTools.Application
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public ProductDto Add(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Product name is requied!");
            }

            if (_productRepository.Exist(name))
            {
                throw new EntityExistException($"Product already exist {name}");
            }

            Product product = new Product(name);
            _productRepository.Save(product);
            return new ProductDto(product);
        }

        public AddressDto[] AddAddresses(ProjectId projectId, NewAddressDto[] addresses)
        {
            addresses = addresses ?? throw new ArgumentNullException(nameof(addresses));
            Product product = _productRepository.Get(projectId) ?? throw new EntityNotEixtException(nameof(projectId));
            Address[] createdAddresses = product.AddAddresses(projectId, addresses);
            _productRepository.Update(product);
            return createdAddresses.Select(x => new AddressDto(x)).ToArray();
        }

        public AddressDto RemoveAddress(ProjectId projectId, AddressId addressId)
        {
            Product product = _productRepository.Get(projectId) ?? throw new EntityNotEixtException(nameof(projectId));
            Address deletedAddress = product
                                    .Projects
                                    .Single(x => x.Id == projectId)
                                    .DeleteAddress(addressId);

            _productRepository.Update(product);
            return new AddressDto(deletedAddress);
        }

        public MachineDto RemoveMachine(ProductId productId, MachineId machineId)
        {
            Product product = _productRepository.Get(productId) ?? throw new EntityNotEixtException(nameof(productId));
            Machine deletedMachine = product.DeleteMachine(machineId);
            _productRepository.Update(product);
            return new MachineDto(deletedMachine);
        }

        public ProjectDto RemoveProject(ProjectId projectId)
        {
            Product product = _productRepository.Get(projectId) ?? throw new EntityNotEixtException(nameof(projectId));
            Project deletedProject = product.DeleteProject(projectId);
            _productRepository.Update(product);
            return new ProjectDto(deletedProject);
        }

        public MachineDto[] AddMachines(ProductId productId, NewMachineDto[] machines)
        {
            machines = machines ?? throw new ArgumentNullException(nameof(machines));
            Product product = GetProduct(productId);
            Machine[] createdMachines = product.AddMachines(machines);
            _productRepository.Update(product);
            return createdMachines.Select(x => new MachineDto(x)).ToArray();
        }

        public ProjectDto[] AddProjects(ProductId productId, string[] projectsNames)
        {
            projectsNames = projectsNames ?? throw new ArgumentNullException(nameof(projectsNames));
            Product product = GetProduct(productId);
            Project[] createdProjects = product.AddProjects(projectsNames);
            _productRepository.Update(product);
            return createdProjects.Select(x => new ProjectDto(x)).ToArray();
        }

        private Product GetProduct(ProductId id) 
            => _productRepository.Get(id) ?? throw new EntityNotEixtException($"Product {id} not found");
    }
}
