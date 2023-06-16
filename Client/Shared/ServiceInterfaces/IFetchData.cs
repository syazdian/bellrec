namespace Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;

public interface IFetchData
{
    public Task FetchDataFromServerDb();

    public Task GenerateDataInServerDb();

    Task<string> GetDetailByPhone(string phone);

    Task<string> GetDetailBySerialNumber(string sn);

    Task<string> GetDetailByOrderNumber(string on);
}