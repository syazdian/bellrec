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

    [HttpGet("GetBellSourceitems")]
    public async Task<IActionResult> GetBellSourceitems()
    {
        var items = _databaseGenerator.DatabaseBellSourceGenerator();
        return Ok(items);
    }
}