using ProgressiveJS.Helpers;
using System.Web.Mvc;

namespace ProgressiveJS.Extensions
{
    public static class JsonNetExtensions
    {
        public static JsonNetResult JsonNet(this Controller controller, object data)
        {
            return new JsonNetResult(data);
        }

        public static JsonNetResult ToJsonNetResult(this object data)
        {
            return new JsonNetResult(data);
        }
    }
}
