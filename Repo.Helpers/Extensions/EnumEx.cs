using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Repo.Helpers.Extensions
{
    public static class EnumEx
    {
        public static string ToDisplayValue(this Enum obj)
        {
            var enumType = GetUnderlyingType(obj.GetType());

            return GetDisplayName(enumType, obj);
        }

        public static Type GetUnderlyingType(Type type)
        {
            var underlyingType = Nullable.GetUnderlyingType(type);

            return underlyingType ?? type;
        }

        public static Type GetUnderlyingType<T>()
        {
            var realModelType = typeof(T);
            return GetUnderlyingType(realModelType);
        }

        public static string GetDisplayName(Type enumType, object value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            if (enumType == null)
            {
                return string.Empty;
            }

            var name = Enum.GetName(enumType, value);
            if (string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }

            var fi = enumType.GetField(name);

            var descriptionAttributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (descriptionAttributes.Length > 0)
            {
                return descriptionAttributes[0].Description;
            }

            var displayAttributes = (DisplayAttribute[])fi.GetCustomAttributes(typeof(DisplayAttribute), false);
            if (displayAttributes.Length > 0)
            {
                return displayAttributes[0].GetName();
            }

            name = Regex.Replace(name, "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", "$1 ").TrimEnd();

            return name;
        }

        public static T Parse<T>(string stringValue)
        {
            return (T)Enum.Parse(typeof(T), stringValue);
        }
    }
}
