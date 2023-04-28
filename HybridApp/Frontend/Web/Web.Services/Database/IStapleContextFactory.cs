namespace Bell.Reconciliation.Frontend.Web.Database;

public interface IStapleContextFactory
{
    Task<StapleSourceContext> CreateStapleSourceContextAsync();
}