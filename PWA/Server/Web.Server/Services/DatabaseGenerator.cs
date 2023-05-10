using Bell.Reconciliation.Common.Models;
using Bell.Reconciliation.Web.Server.Data;
using Mapster;

namespace Bell.Reconciliation.Web.Server.Services
{
    public class DatabaseGenerator
    {
        public DatabaseGenerator()
        {
        }

        public async Task<BellStaplesSourceDto> FetchFromDatabaseBellStaplesSource()
        {
            BellRecContext sqlitedb = new BellRecContext();

            var bellSourcesDb = sqlitedb.BellSources.ToList();
            List<Common.Models.BellSourceDto> bellSources = bellSourcesDb.Adapt<List<Common.Models.BellSourceDto>>();

            var stapleSourcesDb = sqlitedb.StaplesSources.ToList();
            Common.Models.StaplesSourceDto staple;
            List<Common.Models.StaplesSourceDto> stapleSources = stapleSourcesDb.Adapt<List<Common.Models.StaplesSourceDto>>();

            BellStaplesSourceDto bellstaple = new();
            bellstaple.BellSources = bellSources.ToArray();
            bellstaple.StaplesSources = stapleSources.ToArray();

            return bellstaple;
        }

        #region MAY BE OBSELETE

        public async Task<List<Common.Models.BellSourceDto>> BellSourceGeneratorFromMemory(int startId = 0)
        {
            List<Common.Models.BellSourceDto> bellSources = new();

            for (int i = startId + 1; i < startId + 1000; i++)
            {
                bellSources.Add(await GeneratoreBellSourceObject(i));
            }
            return bellSources;
        }

        public async Task<Common.Models.BellSourceDto> GeneratoreBellSourceObject(int i)
        {
            return await Task.Run(() =>
            {
                var newBell = new Common.Models.BellSourceDto()
                {
                    Id = i,
                    Phone = ((long)new Random().Next(1000000, 9999999)),
                    Amount = ((long)new Random().Next(1, 1000))
                    //CommissionDetails = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8),
                    //CustomerName = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8),
                    //IMEI = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8),
                    //LOB = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8),
                    //OrderNumber = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8),
                    //TransactionDate = DateTime.Now.AddDays(new Random().Next(-100000, 0)).ToShortDateString(),
                };
                return newBell;
            });
        }

        #endregion MAY BE OBSELETE
    }
}