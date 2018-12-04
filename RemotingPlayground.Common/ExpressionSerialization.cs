using Newtonsoft.Json;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace RemotingPlayground
{
    public static class ExpressionSerialization
    {
        public static String Serialize(LambdaExpression expression)
        {
            expression = EmbedParametersVisitor.Embed(expression);

            return JsonConvert.SerializeObject(expression, Formatting.Indented, settings);
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

    public class EmbedParametersVisitor : ExpressionVisitor
    {
        public static LambdaExpression Embed(LambdaExpression expression)
        {
            return new EmbedParametersVisitor().VisitAndConvert(expression, "Embed");
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            var potentialValue = GetValueOfPotentialConstantMember(node);

            if (potentialValue != null)
            {
                return Expression.Constant(potentialValue);
            }
            else
            {
                return node;
            }
        }

        Object GetValueOfPotentialConstantMember(Expression node)
        {
            if (node is ConstantExpression cNode)
            {
                return cNode.Value;
            }
            else if (node is MemberExpression mNode)
            {
                if (mNode.Expression == null) return null;

                var nestedValue = GetValueOfPotentialConstantMember(mNode.Expression);

                if (mNode.Member is PropertyInfo propertyInfo)
                {
                    return propertyInfo.GetValue(nestedValue);
                }
                else if (mNode.Member is FieldInfo fieldInfo)
                {
                    return fieldInfo.GetValue(nestedValue);
                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
                return new Exception("Unexpectedly found a " + node.GetType().Name);
            }
        }
    }

}
