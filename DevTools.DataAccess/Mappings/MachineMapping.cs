using DevTools.Application.Models;
using DevTools.Application.Models.Dto;
using DevTools.DataAccess.Serializers;
using MongoDB.Bson.Serialization;

namespace DevTools.DataAccess.Mappings
{
    public static class MachineMapping
    {
        internal static void Register()
        {
            RegisterApplicationModel();
            RegisterDtoModel();
        }

        internal static void RegisterApplicationModel() => BsonClassMap.RegisterClassMap<Machine>(map =>
        {
            map.AutoMap();
            map.SetIdMember(map.GetMemberMap(x => x.Id).SetSerializer(new MachineIdSerializer()));
        });

        internal static void RegisterDtoModel() => BsonClassMap.RegisterClassMap<MachineDto>(map =>
        {
            map.AutoMap();
            map.SetIdMember(map.GetMemberMap(x => x.Id).SetSerializer(new MachineIdSerializer()));
        });
    }
}
