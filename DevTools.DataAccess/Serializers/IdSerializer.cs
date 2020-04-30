using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using System;

namespace DevTools.DataAccess.Serializers
{
    abstract class IdSerializer<T> : IBsonSerializer<T>
    {
        public Type ValueType => typeof(T);

        public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return Deserialize(context.Reader);
        }

        T IBsonSerializer<T>.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return Deserialize(context.Reader);
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            T id = (T)value;
            Serialize(context, args, id);
        }
        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, T value)
        {
            BsonSerializer.Serialize(context.Writer, typeof(Guid?), Convert(value));
        }

        private T Deserialize(IBsonReader reader)
        {
            Guid? value = BsonSerializer.Deserialize<Guid?>(reader);
            return Convert(value);
        }

        protected abstract T Convert(Guid? value);
        protected abstract Guid? Convert(T model);
    }
}
