namespace Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;

public interface ISyncData
{
    public Task UpsertChangesToServerDb();

    public Task UpdateLocalDbWithNewChanges();
}