using System;
using System.ComponentModel;
using System.Reflection;

namespace ProgressiveJS.Helpers
{
    public static class EnumHelper
    {
        /// <summary>
        /// Gets the description of the specified enum type.
        /// </summary>
        /// <param name="enumName">The enum type value.</param>
        /// <returns>A string containing the text of the enum description</returns>
        public static string GetDescription(Enum enumName)
        {
            if (enumName == null)
                throw new ArgumentNullException("The enum must not be null.");

            Type type = enumName.GetType();
            MemberInfo[] memInfo = type.GetMember(enumName.ToString());
            if (memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return enumName.ToString();
        }
    }
}
