using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProgressiveJS.UX
{
    public class ProgressiveItem : JsonNetSerializer
    {
        /// <summary>
        /// The url that should be hit when the ajax updater event is fired.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Additional payload (if any) to send with the ajax updater event.
        /// </summary>
        public object Payload { get; set; }

        /// <summary>
        /// The selector for the target that should be updated when this form is successfully submitted via an ajax call.
        /// Default: ".pjs-response"
        /// </summary>
        public string Target { get; set; }

        private const string _get = "get";
        private const string _post = "post";

        private string _type = FormMethod.Post.ToString().ToLower();
        /// <summary>
        /// The type of html method to use with the ajax updater event.
        /// </summary>
        public FormMethod Type {
            get
            {
                switch (_type)
                {
                    case _get:
                        return FormMethod.Get;
                    case _post:
                    default:
                        return FormMethod.Post;
                }
            }
            set
            {
                _type = value.ToString().ToLower();
            }
        }

        /// <summary>
        /// The type of event that will trigger the ajax updater to fire.
        /// </summary>
        public string EventType { get; set; }

        /// <summary>
        /// Comma-separated list of additional fields (found via their jQuery selector) that should be submitted with
        /// this form even if they are not within the form itself.
        /// </summary>
        public string AdditionalFields { get; set; }

        /// <summary>
        /// A list of callbacks that should be called after the form has completed its submission.
        /// </summary>
        public List<ProgressiveCallback> Callbacks { get; set; }

        public ProgressiveItem(
            string url,
            object payload = null,
            string target = ".pjs-response",
            FormMethod type = FormMethod.Post,
            string eventType = "click",
            string additionalFields = null
        )
        {
            Url = url;
            Payload = payload;
            Target = target;
            _type = type.ToString().ToLower();
            EventType = eventType;
            AdditionalFields = additionalFields != null ? additionalFields.Replace(" ", string.Empty) : null;
            Callbacks = new List<ProgressiveCallback>();
        }

        public ProgressiveItem AddAdditionalField(string selector)
        {
            var list = AdditionalFields.Split(',').ToList();
            list.Add(selector);
            AdditionalFields = string.Join(",", list.Distinct());
            return this;
        }

        public ProgressiveItem AddCallBack(string handle, params object[] param)
        {
            Callbacks.Add(
                new ProgressiveCallback
                {
                    Parameters = new List<object>(param),
                    Handle = handle
                }
            );
            return this;
        }
    }
}