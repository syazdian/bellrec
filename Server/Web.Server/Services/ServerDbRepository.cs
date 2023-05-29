using Bell.Reconciliation.Common.Models;
using Bell.Reconciliation.Web.Server.Data.Sqlserver;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace Bell.Reconciliation.Web.Server.Services
{
    public class ServerDbRepository
    {
        private readonly IConfiguration _config;
        private readonly BellRecContext _bellDbContext;

        public ServerDbRepository(BellRecContext bellContext)
        {
            _bellDbContext = bellContext;
        }

        public async Task<BellStaplesSourceDto> FetchFromDatabaseBellStaplesSource()
        {
            var bellSourcesDb = _bellDbContext.BellSources.ToList();
            List<BellSourceDto> bellSources = bellSourcesDb.Adapt<List<BellSourceDto>>();

            var stapleSourcesDb = await _bellDbContext.StaplesSources.ToListAsync();
            List<StaplesSourceDto> stapleSources = stapleSourcesDb.Adapt<List<StaplesSourceDto>>();

            BellStaplesSourceDto bellstaple = new();
            bellstaple.BellSources = bellSources.ToArray();
            bellstaple.StaplesSources = stapleSources.ToArray();

            return bellstaple;
        }

        public async Task<BellStapleCountDto> CountBellStapleRows()
        {
            try
            {
                BellStapleCountDto bellStapleCountDto = new BellStapleCountDto();
                bellStapleCountDto.BellCount = await _bellDbContext.BellSources.CountAsync();
                bellStapleCountDto.StaplesCount = await _bellDbContext.StaplesSources.CountAsync();
                return bellStapleCountDto;
            }
            catch (Exception EX)
            {
                throw;
            }
        }

        public async Task<List<BellSourceDto>> GetBellSource(int startCount = 1, int endCount = 1)
        {
            try
            {
                var items = await _bellDbContext.BellSources.OrderBy(e => e.Id).Skip(startCount - 1).Take(endCount - startCount + 1).ToListAsync();
                var adapted = items.Adapt<List<BellSourceDto>>();
                return adapted;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<StaplesSourceDto>> GetStaplesSource(int startCount = 1, int endCount = 1)
        {
            var items = await _bellDbContext.StaplesSources.OrderBy(e => e.Id).Skip(startCount - 1).Take(endCount - startCount + 1).ToListAsync();
            var adapted = items.Adapt<List<StaplesSourceDto>>();
            return adapted;
        }

        #region MAY BE OBSELETE

        public async Task<List<BellSourceDto>> BellSourceGeneratorFromMemory(int startId = 0)
        {
            List<BellSourceDto> bellSources = new();

            for (int i = startId + 1; i < startId + 1000; i++)
            {
                bellSources.Add(await GeneratoreBellSourceObject(i));
            }
            return bellSources;
        }

        public async Task<BellSourceDto> GeneratoreBellSourceObject(int i)
        {
            return await Task.Run(() =>
            {
                var newBell = new Common.Models.BellSourceDto()
                {
                    //Id = i,
                    //Phone = ((long)new Random().Next(1000000, 9999999)),
                    //Amount = ((long)new Random().Next(1, 1000))
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