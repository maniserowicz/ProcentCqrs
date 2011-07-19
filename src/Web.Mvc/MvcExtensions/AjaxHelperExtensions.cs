using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;

namespace System.Web.Mvc
{
    public static class AjaxHelperExtensions
    {
        public static MvcForm BeginForm(this AjaxHelper @this, ActionResult result, string onSuccess = null)
        {
            var options = new AjaxOptions
                {
                    OnSuccess = onSuccess,
                    OnFailure = "cqrs.ajax.onError",
                    HttpMethod = "POST",
                };

            return @this.BeginForm(result, options, null);
        }
    }
}