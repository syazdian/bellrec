namespace Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;

public interface ISyncData
{
    public Task UpsertDataInServerDb();

    public Task FetchDataFromServerDb();

    public Task GenerateDataInServerDb();
}