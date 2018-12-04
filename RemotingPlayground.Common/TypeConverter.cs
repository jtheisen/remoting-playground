using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace RemotingPlayground
{
    public static class ExpressionSerialization
    {
        public static String Serialize<T>(Expression<Func<T>> expression)
        {
            return JsonConvert.SerializeObject(expression, Newtonsoft.Json.Formatting.Indented, settings);
        }

        public static LambdaExpression Deserialize(String json)
        {
            return JsonConvert.DeserializeObject<LambdaExpression>(json, settings);
        }

        static JsonSerializerSettings settings = new JsonSerializerSettings
        {
            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
            TypeNameHandling = TypeNameHandling.All,
            Converters = new JsonConverter[] { new TypeConverter(), new Aq.ExpressionJsonSerializer.ExpressionJsonConverter(Assembly.GetExecutingAssembly()) }
        };
    }

    class TypeConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Type);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var type = value as Type;

            writer.WriteValue(type.Name);
        }
    }
}
