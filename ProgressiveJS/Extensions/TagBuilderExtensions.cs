using System.Web.Mvc;

namespace ProgressiveJS.Extensions
{
    public static class TagBuilderExtensions
    {
        public static TagBuilder AddAttr(this TagBuilder tag, string attrName, object attrValue = null)
        {
            if (attrValue == null)
            {
                tag.MergeAttribute(attrName, null);
                return tag;
            }

            var value = attrValue.ToString();
            if (!string.IsNullOrEmpty(value))
                tag.MergeAttribute(attrName, value);

            return tag;
        }
    }
}
