using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTools.Converters
{
    public abstract class IdConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(T);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }
            if (reader.TokenType == JsonToken.String)
            {
                string value = (string)reader.Value;
                return Create(Guid.Parse(value));
            }
            throw new JsonSerializationException($"Unexpected token parsing {typeof(T).Name}. Expected String, got {reader.TokenType}.");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        protected abstract T Create(Guid value);
    }
}
