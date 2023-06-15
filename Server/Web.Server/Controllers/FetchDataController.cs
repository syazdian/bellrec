namespace Bell.Reconciliation.Web.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FetchDataController : Controller
{
    private readonly ServerDbRepository _dbRepo;

    public FetchDataController(ServerDbRepository dbRepo)//, DatabaseGenerator dbGenerator)
    {
        _dbRepo = dbRepo;
    }

    [HttpGet("FetchCountServerDatabase")]
    public async Task<IActionResult> FetchCountServerDatabase()
    {
        var bellStapleCountDto = await _dbRepo.CountBellStapleRows();
        return Ok(bellStapleCountDto);
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
}