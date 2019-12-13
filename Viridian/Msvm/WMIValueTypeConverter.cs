using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace Viridian.Msvm
{
    // TypeConverter to handle null values for ValueType properties
    public class WMIValueTypeConverter : TypeConverter
    {
        private readonly TypeConverter baseConverter;

        private readonly Type baseType;

        public WMIValueTypeConverter(Type inBaseType)
        {
            baseConverter = TypeDescriptor.GetConverter(inBaseType);
            baseType = inBaseType;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type srcType) => baseConverter.CanConvertFrom(context, srcType);

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => baseConverter.CanConvertTo(context, destinationType);

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) => baseConverter.ConvertFrom(context, culture, value);

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary dictionary) => baseConverter.CreateInstance(context, dictionary);

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) => baseConverter.GetCreateInstanceSupported(context);

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributeVar) => baseConverter.GetProperties(context, value, attributeVar);

        public override bool GetPropertiesSupported(ITypeDescriptorContext context) => baseConverter.GetPropertiesSupported(context);

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) => baseConverter.GetStandardValues(context);

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) => baseConverter.GetStandardValuesExclusive(context);

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => baseConverter.GetStandardValuesSupported(context);

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (baseType.BaseType == typeof(Enum))
            {
                if (value?.GetType() == destinationType)
                {
                    return value;
                }
                if ((value == null) && (context != null) && (context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false))
                {
                    return "NULL_ENUM_VALUE";
                }
                return baseConverter.ConvertTo(context, culture, value, destinationType);
            }
            if ((baseType == typeof(bool)) && (baseType.BaseType == typeof(ValueType)))
            {
                if ((value == null) && (context != null) && (context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false))
                {
                    return "";
                }
                return baseConverter.ConvertTo(context, culture, value, destinationType);
            }
            if ((context != null) && (context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false))
            {
                return "";
            }
            return baseConverter.ConvertTo(context, culture, value, destinationType);
        }
    }
}
