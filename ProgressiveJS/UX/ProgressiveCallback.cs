using System.Collections.Generic;

namespace ProgressiveJS.UX
{
    public class ProgressiveCallback
    {
        /// <summary>
        /// The function handle to be called.
        /// </summary>
        public string Handle { get; set; }

        /// <summary>
        /// The parameters (if any) to be passed to the function on callback.
        /// </summary>
        public List<object> Parameters { get; set; }
    }
}