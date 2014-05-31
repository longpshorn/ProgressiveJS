using System.Collections.Generic;

namespace ProgressiveJS.Server
{
    public class ProgressiveCommand
    {
        /// <summary>
        /// The JavaScript client-side command that should be run when the result is returned.
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// The arguments that should be passed to the client-side command.
        /// </summary>
        public List<object> Arguments { get; set; }

        public ProgressiveCommand(string command, List<object> arguments = null)
        {
            Command = command;

            Arguments = arguments != null
                ? arguments
                : new List<object>();
        }
    }
}
