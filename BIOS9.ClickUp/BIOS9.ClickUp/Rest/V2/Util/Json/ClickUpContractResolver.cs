using System.Reflection;
using BIOS9.ClickUp.Core.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BIOS9.ClickUp.Rest.V2.Util.Json;

// Based on https://github.com/discord-net/Discord.Net/blob/dev/src/Discord.Net.Rest/Net/Converters/DiscordContractResolver.cs
internal class ClickUpContractResolver : DefaultContractResolver
{
    private static readonly MethodInfo _shouldSerialize = typeof(ClickUpContractResolver).GetTypeInfo().GetDeclaredMethod("ShouldSerialize");

    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var property = base.CreateProperty(member, memberSerialization);
        if (property.Ignored)
            return property;

        if (member is PropertyInfo propInfo)
        {
            var converter = GetConverter(property, propInfo, propInfo.PropertyType, 0);
            if (converter != null)
            {
                property.Converter = converter;
            }
        }
        else
            throw new InvalidOperationException($"{member.DeclaringType.FullName}.{member.Name} is not a property.");
        return property;
    }

    private static JsonConverter GetConverter(JsonProperty property, PropertyInfo propInfo, Type type, int depth)
    {
        if (type.IsConstructedGenericType)
        {
            Type genericType = type.GetGenericTypeDefinition();
            if (depth == 0 && genericType == typeof(Optional<>))
            {
                var typeInput = propInfo.DeclaringType;
                var innerTypeOutput = type.GenericTypeArguments[0];

                var getter = typeof(Func<,>).MakeGenericType(typeInput, type);
                var getterDelegate = propInfo.GetMethod.CreateDelegate(getter);
                var shouldSerialize = _shouldSerialize.MakeGenericMethod(typeInput, innerTypeOutput);
                var shouldSerializeDelegate = (Func<object, Delegate, bool>)shouldSerialize.CreateDelegate(typeof(Func<object, Delegate, bool>));
                property.ShouldSerialize = x => shouldSerializeDelegate(x, getterDelegate);

                return MakeGenericConverter(property, propInfo, typeof(OptionalConverter<>), innerTypeOutput, depth);
            }
        }

        return null;
    }

    private static bool ShouldSerialize<TOwner, TValue>(object owner, Delegate getter)
    {
        return (getter as Func<TOwner, Optional<TValue>>)((TOwner)owner).IsSpecified;
    }

    private static JsonConverter MakeGenericConverter(JsonProperty property, PropertyInfo propInfo, Type converterType, Type innerType, int depth)
    {
        var genericType = converterType.MakeGenericType(innerType).GetTypeInfo();
        var innerConverter = GetConverter(property, propInfo, innerType, depth + 1);
        return genericType.DeclaredConstructors.First().Invoke(new object[] { innerConverter }) as JsonConverter;
    }
}