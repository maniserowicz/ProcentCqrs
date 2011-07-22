using System.Collections.Generic;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;

namespace System.Web.Mvc
{
    public static class AjaxHelperExtensions
    {
        public static MvcForm BeginForm(this AjaxHelper @this, ActionResult result, IDictionary<string, string> actions = null)
        {
            var options = new AjaxOptions
                {
                    OnSuccess = actions.ValueOrDefault("success"),
                    OnFailure = "cqrs.ajax.onError",
                    HttpMethod = "POST",
                    OnBegin = actions.ValueOrDefault("before"),
                };

            return @this.BeginForm(result, options, null);
        }
    }
}