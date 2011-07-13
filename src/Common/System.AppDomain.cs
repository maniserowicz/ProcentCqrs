using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System
{
    public static class AppDomainExtensions
    {
        public static Assembly[] GetAppAssemblies(this AppDomain @this)
        {
            return @this.GetAssemblies()
                .Where(x => x.FullName.StartsWith("ProcentCqrs."))
                .ToArray();
        }

        public static IEnumerable<Type> GetAppTypes(this AppDomain @this)
        {
            return @this.GetAssemblies()
                .Where(x => x.FullName.StartsWith("ProcentCqrs."))
                .SelectMany(x => x.GetTypes());
        }
    }
}