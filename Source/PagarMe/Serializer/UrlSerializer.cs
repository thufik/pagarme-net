using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PagarMe.Serializer
{
    internal class UrlSerializer
    {
        internal static IEnumerable<Tuple<string, string>> Serialize(object value)
        {
            return Serialize(value, null);
        }

        private static IEnumerable<Tuple<string, string>> Serialize(object obj, string root)
        {
            string rootFormat = root == null ? "{1}" : "{0}[{1}]";

            foreach (
                var prop in
                    obj.GetType()
                        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Where(
                            p =>
                                Attribute.IsDefined(p, typeof(JsonPropertyAttribute)) &&
                                !Attribute.IsDefined(p, typeof(UrlIgnoreAttribute))))
            {
                var propValue = prop.GetValue(obj);
                var mutatorAttribute = prop.GetCustomAttribute<UrlMutatorAttribute>();

                if (mutatorAttribute != null)
                    propValue =
                        ((IUrlConverter)Activator.CreateInstance(mutatorAttribute.ConverterType)).UrlConvert(propValue);

                if (propValue == null)
                    continue;

                var propAttribute = prop.GetCustomAttribute<JsonPropertyAttribute>();
                string name = string.Format(rootFormat, root, propAttribute.PropertyName);
                var value = ConvertValue(prop, propValue);

                if (value == null && propValue.GetType().IsClass)
                {
                    foreach (var tuple in Serialize(propValue, name))
                        yield return tuple;
                }
                else if (value != null)
                {
                    yield return new Tuple<string, string>(name, value);
                }
            }
        }

        internal static string ConvertValue(PropertyInfo propInfo, object value)
        {
            Type expType = value.GetType();
            string result = null;

            if (propInfo != null)
            {
                var converterAttribute = propInfo.GetCustomAttribute<UrlConverterAttribute>();
                var jsonConverterAttribute = propInfo.GetCustomAttribute<JsonConverterAttribute>();

                // FIXME: Ugly hack for enum operations, should exist a better way
                if (propInfo.PropertyType.IsEnum)
                    value = Enum.ToObject(propInfo.PropertyType, (int)value);

                if (converterAttribute != null ||
                    (jsonConverterAttribute != null &&
                     typeof(IUrlConverter).IsAssignableFrom(jsonConverterAttribute.ConverterType)))
                {
                    Type converter = converterAttribute != null
                        ? converterAttribute.ConverterType
                        : jsonConverterAttribute.ConverterType;

                    value = ((IUrlConverter)Activator.CreateInstance(converter)).UrlConvert(value);

                    if (value == null)
                        return null;

                    expType = value.GetType();
                }
            }

            if (
                expType == typeof(Int32)
                || expType == typeof(Int16)
                || expType == typeof(Int64)
                || expType == typeof(Decimal)
                || expType == typeof(Double)
                || expType == typeof(Boolean)
                || expType == typeof(String)
                )
            {
                result = value.ToString();
            }
            else if (expType == typeof(DateTime) || expType == typeof(DateTime?))
            {
                DateTime dt = default(DateTime);

                if (expType == typeof(DateTime))
                    dt = (DateTime)value;
                else if (expType == typeof(DateTime?))
                    dt = ((DateTime?)value).GetValueOrDefault();

                result =
                    ((long)(dt.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToUniversalTime()).TotalMilliseconds).ToString(
                        CultureInfo.InvariantCulture);
            }

            return result;
        }
    }
}
