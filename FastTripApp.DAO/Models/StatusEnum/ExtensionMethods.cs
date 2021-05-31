using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FastTripApp.DAO.Models.StatusEnum
{
    static class ExtensionMethods
    {
        public static string GetStringValue(this Enum value)
        {
            Type type = value.GetType();

            FieldInfo fieldInfo = type.GetField(value.ToString());
            // Get the stringvalue attributes
            StringValueAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(StringValueAttribute), false) as StringValueAttribute[];

            // Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].StringValue : null;
        }
    }
}
