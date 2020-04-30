using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.DataAccess
{
    public static class Extensions
    {
        public static string ToFieldName(this string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return propertyName;
            }
            return $"_{char.ToLower(propertyName[0])}{propertyName.Substring(1)}";
        }

        public static void MapPropertyField<TClass>(this BsonClassMap<TClass> map, string name) 
        {
            map.MapField(name.ToFieldName()).SetElementName(name);
        }
    }
}
