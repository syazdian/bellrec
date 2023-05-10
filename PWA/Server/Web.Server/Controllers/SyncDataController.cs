using Bell.Reconciliation.Web.Server.Data;
using Bell.Reconciliation.Web.Server.Services;
using Mapster;
using Microsoft.AspNetCore.Authentication;

namespace Bell.Reconciliation.Web.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SyncDataController : Controller
{
    private readonly DatabaseGenerator _databaseGenerator;
    private readonly BellRecRepository _bellRepo;

    public SyncDataController(DatabaseGenerator databaseGenerator, BellRecRepository bellRepo)
    {
        _databaseGenerator = databaseGenerator;
        _bellRepo = bellRepo;
    }

    [HttpGet("FetchFromDatabase")]
    public async Task<IActionResult> FetchFromDatabase()
    {
        var items = await _databaseGenerator.FetchFromDatabaseBellStaplesSource();
        return Ok(items);
    }

    [HttpGet("GetBellSourceitems/{Id}")]
    public async Task<IActionResult> GetBellSourceitems([FromRoute] int Id = 0)
    {
        var items = await _databaseGenerator.BellSourceGeneratorFromMemory(Id);
        return Ok(items);
    }

    [HttpGet("FillSqlite/{RecordNom}/{DeleteOldRecords}/{DifferenceRate}")]
    public async Task<IActionResult> FillSqlite([FromRoute] int RecordNom, string DeleteOldRecords, int DifferenceRate)
    {
        var res = _bellRepo.DBGenerator(RecordNom, DeleteOldRecords, DifferenceRate);
        return Ok(res);
    }
}