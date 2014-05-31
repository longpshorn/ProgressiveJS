using System.ComponentModel;

namespace ProgressiveJS.Enums
{
    public enum Manipulator
    {
        [Description("after")]
        After,

        [Description("attr")]
        Attr,

        [Description("before")]
        Before,

        [Description("html")]
        Html,

        [Description("refresh")]
        Refresh,

        [Description("removeAttr")]
        RemoveAttr,

        [Description("replaceWith")]
        ReplaceWith,

        [Description("text")]
        Text,

        [Description("wrap")]
        Wrap
    }
}
