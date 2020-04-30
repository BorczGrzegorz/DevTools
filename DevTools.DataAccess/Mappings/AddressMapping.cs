using DevTools.Application.Models;
using DevTools.Application.Models.Dto;
using DevTools.DataAccess.Serializers;
using MongoDB.Bson.Serialization;

namespace DevTools.DataAccess.Mappings
{
    internal static class AddressMapping
    {
        internal static void Register()
        {
            RegisterApplicationModel();
            RegisterDtoModel();
        }

        internal static void RegisterApplicationModel() => BsonClassMap.RegisterClassMap<Address>(map =>
        {
            map.AutoMap();
            map.SetIdMember(map.GetMemberMap(x => x.Id).SetSerializer(new AddressIdSerializer()));
        });

        internal static void RegisterDtoModel() => BsonClassMap.RegisterClassMap<AddressDto>(map =>
        {
            map.AutoMap();
            map.SetIdMember(map.GetMemberMap(x => x.Id).SetSerializer(new AddressIdSerializer()));
        });
    }
}
