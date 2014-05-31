using ProgressiveJS.Enums;
using ProgressiveJS.Helpers;

namespace ProgressiveJS.Server
{
    /// <summary>
    /// An encapsulation of json response from an ajax call.
    /// </summary>
    public class ProgressiveMainResponse
    {
        /// <summary>
        /// The string that will be passed back to the page to update the target as defined by the ajax call.
        /// Default: ""
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The type of manipulator that should be used to update the target content in the calling page.
        /// Default: "html"
        /// </summary>
        public string Manipulator { get; set; }

        /// <summary>
        /// The status of the response.
        /// </summary>
        public ProgressiveStatus Status { get; set; }

        public ProgressiveMainResponse(string message = "", Manipulator manipulator = ProgressiveJS.Enums.Manipulator.Html, ProgressiveStatus status = ProgressiveStatus.Default)
        {
            Message = message;
            Manipulator = EnumHelper.GetDescription(manipulator);
            Status = status;
        }
    }
}