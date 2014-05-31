using ProgressiveJS.Helpers;
using System.Web.Mvc;

namespace ProgressiveJS.UX
{
    public class ProgressiveAlert
    {
        private readonly string _alertClass;

        public ProgressiveAlert(TempDataDictionary tempData)
            : this(tempData["pjs-alert-message"], tempData["pjs-alert-status"])
        {
        }

        public ProgressiveAlert(object message = null, object status = null)
            : this(
                message != null ? message.ToString() : string.Empty,
                status != null ? (ProgressiveStatus)status : ProgressiveStatus.Error
            )
        {
        }

        public ProgressiveAlert(string message = null, ProgressiveStatus status = ProgressiveStatus.Default)
        {
            Message = message;
            Status = status;
            _alertClass = EnumHelper.GetDescription(Status);
        }

        public string Message { get; set; }

        public ProgressiveStatus Status { get; set; }

        public MvcHtmlString Render()
        {
            if (string.IsNullOrEmpty(Message))
                return MvcHtmlString.Empty;

            var tag = new TagBuilder("div");
            tag.AddCssClass("alert-box");
            tag.AddCssClass(_alertClass);
            tag.MergeAttribute("data-alert", null);

            var close = new TagBuilder("a");
            close.AddCssClass("close");
            close.InnerHtml = "&times;";

            tag.InnerHtml = string.Concat(close.ToString(), Message);

            return MvcHtmlString.Create(tag.ToString());
        }

        public static MvcHtmlString Render(TempDataDictionary tempData)
        {
            return (new ProgressiveAlert(tempData)).Render();
        }
    }
}