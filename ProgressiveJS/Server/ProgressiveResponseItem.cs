using ProgressiveJS.Enums;
using ProgressiveJS.Helpers;
using System.Collections.Generic;

namespace ProgressiveJS.Server
{
    public class ProgressiveResponseItem
    {
        public string Selector { get; set; }

        public string Manipulator { get; set; }

        public List<object> Args { get; set; }

        public ProgressiveResponseItem(List<object> args, string selector = ".pjs-response", Manipulator manipulator = ProgressiveJS.Enums.Manipulator.Html)
            : this(selector, manipulator)
        {
            Args = args;
        }

        public ProgressiveResponseItem(string selector = ".pjs-response", Manipulator manipulator = ProgressiveJS.Enums.Manipulator.Html)
        {
            Selector = selector;
            Manipulator = EnumHelper.GetDescription(manipulator);
        }
    }
}