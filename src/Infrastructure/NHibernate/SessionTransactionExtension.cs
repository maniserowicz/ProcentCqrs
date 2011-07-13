using System;
using NHibernate;

namespace ProcentCqrs.Infrastructure.NHibernate
{
    public static class SessionTransactionExtension
    {
        public static void InTransaction(this ISession @this, Action action)
        {
            using (var tx = @this.BeginTransaction())
            {
                try
                {
                    action();
                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }
    }
}