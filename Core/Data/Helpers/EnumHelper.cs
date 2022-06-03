using System.ComponentModel;

namespace Data.Helpers;

public static class EnumHelper
{
    public static string GetEnumDescription(Enum value)
    {
        var field = value.GetType().GetField(value.ToString());

        var attributes = (DescriptionAttribute[]) field?.GetCustomAttributes(typeof(DescriptionAttribute), false)!;

        return attributes.Length > 0 ? attributes[0].Description : value.ToString();
    }
}