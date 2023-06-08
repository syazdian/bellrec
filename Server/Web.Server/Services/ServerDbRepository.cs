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
        //private readonly Bell.Reconciliation.Web.Server.Data.Sqlserver.StapleContext _bellDbContext;

        public ServerDbRepository(BellRecContext bellContext)
        //  public ServerDbRepository(StapleContext bellContext)
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
            try
            {
                var items = await _bellDbContext.StaplesSources.OrderBy(e => e.Id).Skip(startCount - 1).Take(endCount - startCount + 1).ToListAsync();
                var adapted = items.Adapt<List<StaplesSourceDto>>();
                return adapted;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task SyncBellSourceChanges(List<BellSourceDto> bellSourceDtos)
        {
            try
            {
                foreach(var bell in bellSourceDtos)
                {
                    var bellsourceInServer = _bellDbContext.BellSources.Where(c => c.OrderNumber == bell.OrderNumber).FirstOrDefault();
                    bellsourceInServer.Comment = bell.Comment;
                    bellsourceInServer.MatchStatus = bell.MatchStatus.ToString();
                    bellsourceInServer.Reconciled = bell.Reconciled.ToString();
                    bellsourceInServer.ReconciledBy = bell.ReconciledBy;
                    bellsourceInServer.ReconciledDate = bell.ReconciledDate;
                    bellsourceInServer.UpdateDate = DateTime.Now;
                }
                _bellDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task SyncStapleSourceChanges(List<StaplesSourceDto> stapleSourceDtos)
        {
            try
            {
                foreach (var staple in stapleSourceDtos)
                {
                    var staplesourceInServer = _bellDbContext.StaplesSources.Where(c => c.OrderNumber == staple.OrderNumber).FirstOrDefault();
                    staplesourceInServer.Comment = staple.Comment;
                    staplesourceInServer.MatchStatus =(int)staple.MatchStatus;
                    staplesourceInServer.Reconciled = staple.Reconciled.ToString();
                    staplesourceInServer.ReconciledBy = staple.ReconciledBy;
                    staplesourceInServer.ReconciledDate = staple.ReconciledDate; staplesourceInServer.UpdateDate = DateTime.Now;
                }
                _bellDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}