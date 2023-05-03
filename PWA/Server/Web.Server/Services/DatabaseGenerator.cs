using Bell.Reconciliation.Common.Models;

namespace Bell.Reconciliation.Web.Server.Services
{
    public class DatabaseGenerator
    {
        public DatabaseGenerator()
        {
        }

        public async Task<List<BellSource>> DatabaseBellSourceGenerator()
        {
            List<BellSource> bellSources = new();

            for (int i = 0; i < 100000; i++)
            {
                bellSources.Add(await GeneratoreBellSourceObject(i));
            }
            return bellSources;
        }

        public async Task<BellSource> GeneratoreBellSourceObject(int i)
        {
            var newBell = new BellSource()
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
        }
    }
}