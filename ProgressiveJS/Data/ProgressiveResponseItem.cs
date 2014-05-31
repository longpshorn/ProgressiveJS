using ProgressiveJS.Helpers;
using ProgressiveJS.UX;
using System.Collections.Generic;

namespace ProgressiveJS.Data
{
    public class ProgressiveResponseItem
    {
        public string Selector { get; set; }

        public string Manipulator { get; set; }

        public List<object> Args { get; set; }

        public ProgressiveResponseItem(List<object> args, string selector = ".pjs-response", ProgressiveManipulator manipulator = ProgressiveManipulator.Html)
            : this(selector, manipulator)
        {
            Args = args;
        }

        public ProgressiveResponseItem(string selector = ".pjs-response", ProgressiveManipulator manipulator = ProgressiveManipulator.Html)
        {
            Selector = selector;
            Manipulator = EnumHelper.GetDescription(manipulator);
        }
    }
}