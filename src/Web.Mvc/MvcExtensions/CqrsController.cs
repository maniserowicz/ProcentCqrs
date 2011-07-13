using System.Web.Mvc;
using ProcentCqrs.Domain.Core;
using Simple.Data;

namespace ProcentCqrs.Web.Mvc.MvcExtensions
{
    public abstract class CqrsController : Controller
    {
        public ICommandSender _commandSender { get; set; }

        protected dynamic _db = Database.OpenNamedConnection("cqrs-readmodel");
    }
}