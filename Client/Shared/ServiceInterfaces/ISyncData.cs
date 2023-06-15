namespace Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;

public interface ISyncData
{
    public Task StartSyncData();

    public Task UpdateChangesToServerDb();

    public Task UpdateLocalDbWithNewChangesFromServer();
}