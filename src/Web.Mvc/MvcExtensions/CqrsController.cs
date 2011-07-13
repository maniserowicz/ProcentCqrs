using System.Web.Mvc;
using ProcentCqrs.Domain.Core;

namespace ProcentCqrs.Web.Mvc.MvcExtensions
{
    public abstract class CqrsController : Controller
    {
        public ICommandSender _commandSender { get; set; }
    }
}