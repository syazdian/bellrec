using Bell.Reconciliation.Common.Models;
using Bell.Reconciliation.Common.Models.Domain;
using Bell.Reconciliation.Common.Utilities;
using Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;

namespace Bell.Reconciliation.Frontend.Desktop.Services;

public class InjectBellSource : IInjectBellSource
{
    private readonly List<BellSource> _sources = new List<BellSource>();

    public InjectBellSource()
    {
    }

    public async Task<List<BellSource>> GetBellSourcesAsync()
    {
        await InsertBellSourcesAsync();
        return _sources;
    }

    //public async Task<BellSource> InsertBellSourcesAsync()
    //{
    //    var newBell = new BellSource()
    //    {
    //        Id = _sources.Any() ? (_sources.Max(x => x.Id) + 1) : 1,
    //        Phone = ((long)new Random().Next(0, 100000) * (long)new Random().Next(0, 100000)).ToString().PadLeft(10, '0'),
    //        Amount = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8),
    //        Comment = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8),
    //        CommissionDetails = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8),
    //        CustomerName = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8),
    //        IMEI = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8),
    //        LOB = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8),
    //        OrderNumber = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8),
    //        TransactionDate = DateTime.Now.AddDays(new Random().Next(-100000, 0)).ToShortDateString(),
    //    };
    //    await Task.Run(() =>
    //     {
    //         _sources.Add(newBell);
    //     });
    //    return newBell;
    //}
}