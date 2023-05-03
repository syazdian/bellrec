using Bell.Reconciliation.Web.Server.Services;

namespace Bell.Reconciliation.Web.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SyncDataController : Controller
{
    private readonly DatabaseGenerator _databaseGenerator;

    public SyncDataController(DatabaseGenerator databaseGenerator)
    {
        _databaseGenerator = databaseGenerator;
    }

    [HttpGet("GetBellSourceitems/{Id}")]
    public async Task<IActionResult> GetBellSourceitems([FromRoute] int Id = 0)
    {
        var items = await _databaseGenerator.DatabaseBellSourceGenerator(Id);
        return Ok(items);
    }
}