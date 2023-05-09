using Bell.Reconciliation.Common.Models;
using Bell.Reconciliation.Web.Server.Data;

namespace Bell.Reconciliation.Web.Server.Services
{
    public class DatabaseGenerator
    {
        public DatabaseGenerator()
        {
        }

        public async Task<List<Common.Models.BellSource>> DatabaseBellSourceGenerator(int startId = 0)
        {
            List<Common.Models.BellSource> bellSources = new();

            for (int i = startId + 1; i < startId + 1000; i++)
            {
                bellSources.Add(await GeneratoreBellSourceObject(i));
            }
            return bellSources;
        }

        public async Task<Common.Models.BellSource> GeneratoreBellSourceObject(int i)
        {
            return await Task.Run(() =>
            {
                var newBell = new Common.Models.BellSource()
                {
                    Id = i,
                    Phone = ((long)new Random().Next(0, 100000) * (long)new Random().Next(0, 100000)).ToString().PadLeft(10, '0'),
                    Amount = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8),
                    Comment = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8),
                    CommissionDetails = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8),
                    CustomerName = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8),
                    IMEI = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8),
                    LOB = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8),
                    OrderNumber = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8),
                    TransactionDate = DateTime.Now.AddDays(new Random().Next(-100000, 0)).ToShortDateString(),
                };
                return newBell;
            });
        }
    }
}