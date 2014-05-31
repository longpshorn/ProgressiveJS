using ProgressiveJS.Enums;
using ProgressiveJS.Helpers;
using System.Web.Mvc;

namespace ProgressiveJS.Server
{
    public class AlertMessage
    {
        private readonly string _alertClass;

        public AlertMessage(TempDataDictionary tempData)
            : this(tempData["pjs-message"], tempData["pjs-status"])
        {
        }

        public AlertMessage(object message = null, object status = null)
            : this(
                message != null ? message.ToString() : string.Empty,
                status != null ? (MessageStatus)status : MessageStatus.Error
            )
        {
        }

        public AlertMessage(string message = null, MessageStatus status = MessageStatus.Default)
        {
            Message = message;
            Status = status;
            _alertClass = EnumHelper.GetDescription(Status);
        }

        public string Message { get; set; }

        public MessageStatus Status { get; set; }

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
            return (new AlertMessage(tempData)).Render();
        }
    }
}