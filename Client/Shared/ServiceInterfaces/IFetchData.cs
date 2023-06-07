namespace Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;

public interface IFetchData
{
    public Task FetchDataFromServerDb();

    public Task GenerateDataInServerDb();
}