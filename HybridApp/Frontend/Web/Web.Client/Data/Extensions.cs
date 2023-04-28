using Bell.Reconciliation.Frontend.Web.Database;
using Microsoft.EntityFrameworkCore;

namespace Bell.Reconciliation.Frontend.Web.Client.Data
{
    public static class Extensions
    {
        public static IServiceCollection AddSynchronizingDataFactory(
            this IServiceCollection service) =>
            service.AddSingleton<IStapleContextFactory, SynchronizedStapleDbContextFactory>();
    }
}