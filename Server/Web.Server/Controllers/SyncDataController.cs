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

    [HttpGet("GetLatestChangedStaplesSourceItemsByDate/{dateTime}")]
    public async Task<IActionResult> GetLatestChangedStaplesSourceItemsByDate([FromRoute] DateTime dateTime)
    {
        var res = await _dbRepo.GetStaplesSourceLatestReconciledDate(dateTime);
        return Ok(res);
    }

    [HttpGet("GetLatestChangedBellSourceItemsByDate/{dateTime}")]
    public async Task<IActionResult> GetLatestChangedBellSourceItemsByDate([FromRoute] DateTime dateTime)
    {
        var res = await _dbRepo.GetBellSourceByLatestReconciledDate(dateTime);
        return Ok(res);
    }

    //[HttpGet("GenerateServerDb/{recordNom}/{deleteOldRecords}/{differenceRate}")]
    //public async Task<IActionResult> GenerateServerDb([FromRoute] int recordNom, string deleteOldRecords, int differenceRate)
    //{
    //    var res = _dbGenerator.DBGenerator(recordNom, deleteOldRecords, differenceRate);
    //    return Ok(res);
    //}
}