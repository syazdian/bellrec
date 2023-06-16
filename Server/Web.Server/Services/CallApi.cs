using Bell.Reconciliation.Web.Server.Data.Sqlserver;

namespace Bell.Reconciliation.Web.Server.Services;

public class CallApi
{
    private readonly BellRecContext _bellDbContext;

    public CallApi(BellRecContext bellContext)
    {
        _bellDbContext = bellContext;
    }

    public string GetDetailByPhone(string phoneNumber)
    {
        return _bellDbContext.OrderSnapshots.First().Data;
    }

    public string GetDetailBySerialNumber(string sn)
    {
        return _bellDbContext.OrderOperations.First().Data;
    }

    public string GetDetailByOrderNumber(string on)
    {
        //CALL https://dev.api.staplescan.com/dev/bellservices/v1.0/mw/reconciliation/orderBySim/89302610102012516746
        var res = _bellDbContext.OrderOperations.First().Data;
        return res;
    }
}