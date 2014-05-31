using ProgressiveJS.Helpers;
using ProgressiveJS.UX;
using System.Collections.Generic;

namespace ProgressiveJS.Data
{
    public class ProgressiveResult : JsonNetResult
    {
        public ProgressiveResult(string message = "", ProgressiveManipulator manipulator = ProgressiveManipulator.Html, ProgressiveStatus status = ProgressiveStatus.Default)
        {
            Data = new ProgressiveResponse(message, manipulator, status);
        }

        public ProgressiveResult Attr(string selector, string attr, string value)
        {
            return AddItem(selector, new List<object> { attr, value }, ProgressiveManipulator.Attr);
        }

        public ProgressiveResult After(string selector, string content)
        {
            return AddContent(selector, content, ProgressiveManipulator.After);
        }

        public ProgressiveResult Before(string selector, string content)
        {
            return AddContent(selector, content, ProgressiveManipulator.Before);
        }

        public ProgressiveResult Html(string selector, string html)
        {
            return AddContent(selector, html);
        }

        public ProgressiveResult Refresh(string selector, string attr)
        {
            return AddContent(selector, attr, ProgressiveManipulator.Refresh);
        }

        public ProgressiveResult RemoveAttr(string selector, string attr)
        {
            return AddContent(selector, attr, ProgressiveManipulator.RemoveAttr);
        }

        public ProgressiveResult ReplaceWith(string selector, string content)
        {
            return AddContent(selector, content, ProgressiveManipulator.ReplaceWith);
        }

        public ProgressiveResult SetValue(string key, object value)
        {
            return AddContent("#" + key, value, ProgressiveManipulator.Refresh);
        }

        public ProgressiveResult Text(string selector, string text)
        {
            return AddContent(selector, text, ProgressiveManipulator.Text);
        }

        public ProgressiveResult AddContent(string selector, object content, ProgressiveManipulator manipulator = ProgressiveManipulator.Html)
        {
            return AddItem(selector, new List<object> { content }, manipulator);
        }

        public ProgressiveResult AddItem(string selector = ".pjs-response", List<object> args = null, ProgressiveManipulator manipulator = ProgressiveManipulator.Html)
        {
            var data = Data as ProgressiveResponse;
            if (data != null)
                data.Items.Add(new ProgressiveResponseItem(args, selector, manipulator));
            return this;
        }

        public ProgressiveResult SetMessage(string message = "", ProgressiveManipulator manipulator = ProgressiveManipulator.Html, ProgressiveStatus status = ProgressiveStatus.Default)
        {
            var data = Data as ProgressiveResponse;
            if (data != null)
            {
                data.MainResponse.Message = message;
                data.MainResponse.Manipulator = EnumHelper.GetDescription(manipulator);
                data.MainResponse.Status = status;
            }
            return this;
        }

        public ProgressiveResult AddCommand(string command, List<object> args = null)
        {
            var data = Data as ProgressiveResponse;
            if (data != null)
                data.Commands.Add(new ProgressiveCommand(command, args));
            return this;
        }

        public ProgressiveResult SetStatus(ProgressiveStatus status, string message = null)
        {
            var data = Data as ProgressiveResponse;
            if (data != null)
            {
                data.StatusCode = status;
                if (!string.IsNullOrEmpty(message))
                    data.MainResponse.Message = message;
            }
            return this;
        }
    }
}