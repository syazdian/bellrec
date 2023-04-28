using Microsoft.EntityFrameworkCore;

namespace Bell.Reconciliation.Frontend.Web.Database;

public class DefaultStapleContextFactory : IStapleContextFactory
{
    private readonly IDbContextFactory<StapleSourceContext> _contextFactory;

    public DefaultStapleContextFactory(IDbContextFactory<StapleSourceContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<StapleSourceContext> CreateStapleSourceContextAsync()
    {
        return await _contextFactory.CreateDbContextAsync();
    }
}