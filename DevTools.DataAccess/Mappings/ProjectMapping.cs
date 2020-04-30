using DevTools.Application.Models;
using DevTools.Application.Models.Dto;
using DevTools.DataAccess.Serializers;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.DataAccess.Mappings
{
    internal static class ProjectMapping
    {
        internal static void Register() 
        {
            RegisterApplicationModel();
            RegisterDtoModel();
        }

        internal static void RegisterApplicationModel() => BsonClassMap.RegisterClassMap<Project>(map =>
        {
            map.AutoMap();
            map.SetIdMember(map.GetMemberMap(x => x.Id).SetSerializer(new ProjectIdSerializer()));
            map.MapPropertyField(nameof(Project.Addresses));
        });

        internal static void RegisterDtoModel() => BsonClassMap.RegisterClassMap<ProjectDto>(map =>
        {
            map.AutoMap();
            map.SetIdMember(map.GetMemberMap(x => x.Id).SetSerializer(new ProjectIdSerializer()));
        });
    }
}
