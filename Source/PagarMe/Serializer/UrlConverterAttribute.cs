using System;

namespace PagarMe.Serializer
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class UrlConverterAttribute : Attribute
    {
        public Type ConverterType { get; private set; }

        public UrlConverterAttribute(Type type)
        {
            ConverterType = type;
        }
    }
}
