using Bell.Reconciliation.Web.Server.Data;
using Mapster;

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
            Common.Models.BellSource_new bell;
            List<Common.Models.BellSource_new> bellSources = new();
            foreach (var source in bellSourcesDb)
            {
                bell = source.Adapt<Common.Models.BellSource_new>();

                bellSources.Add(bell);
            }

            var stapleSourcesDb = sqlitedb.StaplesSources.ToList();
            Common.Models.StaplesSource_new staple;
            List<Common.Models.StaplesSource_new> stapleSources = new();
            foreach (var source in stapleSourcesDb)
            {
                staple = source.Adapt<Common.Models.StaplesSource_new>();

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