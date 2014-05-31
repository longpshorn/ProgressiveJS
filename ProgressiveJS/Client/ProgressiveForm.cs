using System.Collections.Generic;
using System.Linq;

namespace ProgressiveJS.Client
{
    public class ProgressiveForm : JsonNetSerializer
    {
        /// <summary>
        /// The selector for the target that should be updated when this form is successfully submitted via an ajax call.
        /// Default: ".pjs-response"
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// A boolean indication of whether the response target is a "Global" target (i.e., exists anywhere in the page)
        /// or if it exists locally within the associated form.
        /// Default: false.
        /// </summary>
        public bool GlobalTarget { get; set; }

        /// <summary>
        /// A boolean indication of whether the associated form should be reset after it is submitted.
        /// Default: true.
        /// </summary>
        public bool ResetForm { get; set; }

        /// <summary>
        /// A boolean indication of whether the associated form should be cleared after it is submitted.
        /// Default: false.
        /// </summary>
        public bool ClearForm { get; set; }

        /// <summary>
        /// The "location" the user should be redirected to in the event that this form is successfully submitted.
        /// </summary>
        public string LocationOnSuccess { get; set; }

        /// <summary>
        /// Comma-separated list of additional fields (found via their jQuery selector) that should be submitted with
        /// this form even if they are not within the form itself.
        /// </summary>
        public string AdditionalFields { get; set; }

        /// <summary>
        /// A list of callbacks that should be called after the form has completed its submission.
        /// </summary>
        public List<Callback> Callbacks { get; set; }

        public ProgressiveForm(
            string target = ".pjs-response",
            bool globalTarget = true,
            bool resetForm = true,
            bool clearForm = false,
            string locationOnSuccess = null,
            string additionalFields = null
        )
        {
            Target = target;
            GlobalTarget = globalTarget;
            ResetForm = resetForm;
            ClearForm = clearForm;
            LocationOnSuccess = locationOnSuccess;
            AdditionalFields = additionalFields != null ? additionalFields.Replace(" ", string.Empty) : null;
            Callbacks = new List<Callback>();
        }

        public ProgressiveForm AddAdditionalField(string selector)
        {
            var list = AdditionalFields.Split(',').ToList();
            list.Add(selector);
            AdditionalFields = string.Join(",", list.Distinct());
            return this;
        }

        public ProgressiveForm AddCallBack(string handle, params object[] param)
        {
            Callbacks.Add(
                new Callback
                {
                    Parameters = new List<object>(param),
                    Handle = handle
                }
            );
            return this;
        }
    }
}
