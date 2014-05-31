using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ProgressiveJS.Enums
{
    public enum MessageStatus
    {
        [Description("default")]
        Default = 0,

        [Description("success")]
        Success = 1,

        [Description("warning")]
        Warning = 2,

        [Description("error")]
        Error = 3
    }
}