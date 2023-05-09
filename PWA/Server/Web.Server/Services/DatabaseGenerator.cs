using Bell.Reconciliation.Web.Server.Data;

namespace Bell.Reconciliation.Web.Server.Services
{
    public class DatabaseGenerator
    {
        public DatabaseGenerator()
        {
        }

        public async Task<BellStaplesSource> DatabaseBellSourceGenerator(int startId = 0)
        {
            BellRecContext sqlitedb = new BellRecContext();
            var bellSourcesDb = sqlitedb.BellSources.ToList();

            Common.Models.BellSource bell;
            List<Common.Models.BellSource> bellSources = new();
            foreach (var source in bellSourcesDb)
            {
                bell = new Common.Models.BellSource();
                bell.Amount = source.Amound;
                bell.Comment = source.Comment;
                bell.CommissionDetails = source.CommissionDetails;
                bell.CustomerName = source.CustomerName;
                bell.Id = int.Parse(source.Id.ToString());
                bell.IMEI = source.Imei;
                bell.LOB = source.Lob;
                bell.OrderNumber = source.OrderNumber;
                bell.Phone = source.Phone.ToString();
                bell.TransactionDate = source.TransactionDate;

                bellSources.Add(bell);
            }

            Common.Models.StaplesSource staple;
            List<Common.Models.StaplesSource> stapleSources = new();
            foreach (var source in bellSourcesDb)
            {
                staple = new Common.Models.StaplesSource();
                staple.Amount = source.Amound;
                staple.Comment = source.Comment;
                staple.CommissionDetails = source.CommissionDetails;
                staple.CustomerName = source.CustomerName;
                staple.Id = int.Parse(source.Id.ToString());
                staple.IMEI = source.Imei;
                staple.LOB = source.Lob;
                staple.OrderNumber = source.OrderNumber;
                staple.Phone = source.Phone.ToString();
                staple.TransactionDate = source.TransactionDate;

                stapleSources.Add(staple);
            }

            BellStaplesSource bellstaple = new();
            bellstaple.BellSources = bellSources.ToArray();
            bellstaple.StaplesSources = stapleSources.ToArray();

            return bellstaple;
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