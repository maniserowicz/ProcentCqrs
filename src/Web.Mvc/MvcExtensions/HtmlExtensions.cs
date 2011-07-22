using System.Linq.Expressions;

namespace System.Web.Mvc
{
    public static class HtmlExtensions
    {
        public static string NameFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));
        }

        public static string IdFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return HtmlHelper.GenerateIdFromName(NameFor(htmlHelper, expression));
        }
    }
}