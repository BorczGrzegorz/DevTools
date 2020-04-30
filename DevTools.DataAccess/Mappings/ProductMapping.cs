using DevTools.Application.Models;
using DevTools.Application.Models.Dto;
using DevTools.DataAccess.Serializers;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.DataAccess.Mappings
{
    internal static class ProductMapping
    {
        internal static void Register()
        {
            RegisterApplicationModel();
            RegisterDtoModel();
        }

        private static void RegisterApplicationModel() => BsonClassMap.RegisterClassMap<Product>(map =>
        {
            map.AutoMap();
            map.SetIdMember(map.GetMemberMap(x => x.Id).SetSerializer(new ProductIdSerializer()).SetElementName("Id"));
            map.MapPropertyField(nameof(Product.Projects));
            map.MapPropertyField(nameof(Product.Machines));
        });

        private static void RegisterDtoModel() => BsonClassMap.RegisterClassMap<ProductDto>(map =>
        {
            map.AutoMap();
            map.SetIdMember(map.GetMemberMap(x => x.Id).SetSerializer(new ProductIdSerializer()));
        });
    }
}
