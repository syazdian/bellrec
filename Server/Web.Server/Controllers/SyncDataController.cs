using Bell.Reconciliation.Web.Server.Data;
using Bell.Reconciliation.Web.Server.Services;
using Mapster;
using Microsoft.AspNetCore.Authentication;

namespace Bell.Reconciliation.Web.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SyncDataController : Controller
{
    private readonly ServerDbRepository _dbRepo;
    // private readonly DatabaseGenerator _dbGenerator;

    public SyncDataController(ServerDbRepository dbRepo)//, DatabaseGenerator dbGenerator)
    {
        _dbRepo = dbRepo;
        // _dbGenerator = dbGenerator;
    }

    [HttpGet("ReturnHello")]
    public async Task<IActionResult> ReturnHello()
    {
        return Ok("HELLO");
    }

    [HttpGet("FetchCountServerDatabase")]
    public async Task<IActionResult> FetchCountServerDatabase()
    {
        var bellStapleCountDto = await _dbRepo.CountBellStapleRows();
        return Ok(bellStapleCountDto);
    }

    [HttpGet("FetchFromServerDatabase")]
    public async Task<IActionResult> FetchFromServerDatabase()
    {
        var items = await _dbRepo.FetchFromDatabaseBellStaplesSource();
        return Ok(items);
    }

    [HttpGet("GetBellSourceItems/{startCount}/{endCount}")]
    public async Task<IActionResult> GetBellSourceItems([FromRoute] int startCount = 1, int endCount = 1)
    {
        var items = await _dbRepo.GetBellSource(startCount, endCount);
        return Ok(items);
    }

    [HttpGet("GetStaplesSourceItems/{startCount}/{endCount}")]
    public async Task<IActionResult> GetStaplesSourceItems([FromRoute] int startCount = 1, int endCount = 1)
    {
        var items = await _dbRepo.GetStaplesSource(startCount, endCount);
        return Ok(items);
    }

    [HttpPost("SyncChangesStaple")]
    public async Task<IActionResult> SyncChangesStaple(List<StaplesSourceDto> stapleSourceChanges)
    {
        await _dbRepo.SyncStapleSourceChanges(stapleSourceChanges);
        return Ok();
    }

    [HttpPost("SyncChangesBell")]
    public async Task<IActionResult> SyncChangesBell(List<BellSourceDto> bellSourceChanges)
    {
        await _dbRepo.SyncBellSourceChanges(bellSourceChanges);
        return Ok();
    }

    //[HttpGet("GenerateServerDb/{recordNom}/{deleteOldRecords}/{differenceRate}")]
    //public async Task<IActionResult> GenerateServerDb([FromRoute] int recordNom, string deleteOldRecords, int differenceRate)
    //{
    //    var res = _dbGenerator.DBGenerator(recordNom, deleteOldRecords, differenceRate);
    //    return Ok(res);
    //}
}