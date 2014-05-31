using ProgressiveJS.UX;
using System.Collections.Generic;

namespace ProgressiveJS.Data
{
    public class ProgressiveResponse
    {
        /// <summary>
        /// The encapsulated main response that will be used to replace the target as specified from the ajax call.
        /// </summary>
        public ProgressiveMainResponse MainResponse { get; set; }

        /// <summary>
        /// A list of additional items that can be used to update various portions of the page from which the ajax call was made.
        /// </summary>
        public List<ProgressiveResponseItem> Items { get; set; }

        /// <summary>
        /// A list of commands to be called when the ProgressiveResponse is returned.
        /// </summary>
        public List<ProgressiveCommand> Commands { get; set; }

        /// <summary>
        /// A status code message that can be used to take subsequent action.
        /// </summary>
        public ProgressiveStatus StatusCode { get; set; }

        public ProgressiveResponse(string message = "", ProgressiveManipulator manipulator = ProgressiveManipulator.Html, ProgressiveStatus status = ProgressiveStatus.Default)
        {
            MainResponse = new ProgressiveMainResponse(message, manipulator, status);
            Items = new List<ProgressiveResponseItem>();
            Commands = new List<ProgressiveCommand>();
        }
    }
}