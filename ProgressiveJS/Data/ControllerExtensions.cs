using ProgressiveJS.UX;
using System.IO;
using System.Web.Mvc;

namespace ProgressiveJS.Data
{
    public static class ControllerExtensions
    {
        #region RenderProgressiveAlert
        public static void SetProgressiveAlertData(this Controller controller, ProgressiveStatus status, string successMessage, string errorMessage)
        {
            controller.SetProgressiveAlertData(
                status,
                status.Equals(ProgressiveStatus.Default) || status.Equals(ProgressiveStatus.Success)
                    ? successMessage
                    : errorMessage
            );
        }

        public static void SetProgressiveAlertData(this Controller controller, ProgressiveStatus status, string message)
        {
            controller.TempData["pjs-alert-status"] = status;
            controller.TempData["pjs-alert-message"] = message;
        }

        /// <summary>
        /// A controller extension method to modularize the setting of the TempData values used in the "_ProgressiveAlert" view.
        /// </summary>
        /// <param name="controller">The controller class that is being extended.</param>
        /// <param name="status">A static indicator for the state of the message to be printed.</param>
        /// <param name="successMessage">The message to be if the result was successful.</param>
        /// <param name="failureMessage">The message to be if the result was unsuccessful.</param>
        /// <returns>
        /// A rendering of the _ProgressiveAlert partial view that makes use of the TempData dictionary to provide a
        /// modularized alert style message to users.
        /// </returns>
        public static string RenderProgressiveAlert(this Controller controller, ProgressiveStatus status, string successMessage, string errorMessage)
        {
            return controller.RenderProgressiveAlert(
                status,
                status.Equals(ProgressiveStatus.Default) || status.Equals(ProgressiveStatus.Success)
                    ? successMessage
                    : errorMessage
            );
        }

        /// <summary>
        /// A controller extension method to modularize the setting of the TempData values used in the "_ProgressiveAlert" view.
        /// </summary>
        /// <param name="controller">The controller class that is being extended.</param>
        /// <param name="status">A static indicator for the state of the message to be printed.</param>
        /// <param name="message">The message to be printed.</param>
        /// <returns>
        /// A rendering of the _ProgressiveAlert partial view that makes use of the TempData dictionary to provide a
        /// modularized alert style message to users.
        /// </returns>
        public static string RenderProgressiveAlert(this Controller controller, ProgressiveStatus status, string message)
        {
            if (!controller.Request.IsAjaxRequest())
                controller.SetProgressiveAlertData(status, message);

            return (new ProgressiveAlert(message, status)).Render().ToString();
        }
        #endregion

        #region RenderPartialViewToString
        // From http://craftycodeblog.com/2010/05/15/asp-net-mvc-render-partial-view-to-string/
        // Allows contents of a partial view to be returned as a string
        public static string RenderPartialViewToString(this Controller controller)
        {
            return controller.RenderPartialViewToString(null, null);
        }

        public static string RenderPartialViewToString(this Controller controller, string viewName)
        {
            return controller.RenderPartialViewToString(viewName, null);
        }

        public static string RenderPartialViewToString(this Controller controller, object model)
        {
            return controller.RenderPartialViewToString(null, model);
        }

        public static string RenderPartialViewToString(this Controller controller, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = controller.ControllerContext.RouteData.GetRequiredString("action");

            controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
        #endregion
    }
}
