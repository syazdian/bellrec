namespace Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;

public interface ISyncData
{
    public Task UpdateChangesToServerDb();

    public Task UpdateLocalDbWithNewChanges();
}