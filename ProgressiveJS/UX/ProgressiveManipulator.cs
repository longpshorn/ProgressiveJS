using System.ComponentModel;

namespace ProgressiveJS.UX
{
    public enum ProgressiveManipulator
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
