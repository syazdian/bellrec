namespace Bell.Reconciliation.Web.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FetchDataController : Controller
{
    private readonly ServerDbRepository _dbRepo;
    private readonly CallApi _Api;

    public FetchDataController(ServerDbRepository dbRepo, CallApi api)//, DatabaseGenerator dbGenerator)
    {
        _dbRepo = dbRepo;
        _Api = api;
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

    [HttpGet("GetDetailByPhone/{phone}")]
    public async Task<IActionResult> GetDetailByPhone([FromRoute] string phone)
    {
        var items = _Api.GetDetailByPhone(phone);
        return Ok(items);
    }

    [HttpGet("GetDetailBySerialNumber/{sn}")]
    public async Task<IActionResult> GetDetailBySerialNumber([FromRoute] string sn)
    {
        var items = _Api.GetDetailBySerialNumber(sn);
        return Ok(items);
    }

    [HttpGet("GetDetailByOrderNumber/{on}")]
    public async Task<IActionResult> GetDetailByOrderNumber([FromRoute] string on)
    {
        var items = _Api.GetDetailByOrderNumber(on);
        return Ok(items);
    }
}