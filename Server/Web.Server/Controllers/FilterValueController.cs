namespace Bell.Reconciliation.Web.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilterValueController : Controller
{
    private readonly FilterItems _filterItems;

    public FilterValueController(FilterItems filterItems)
    {
        _filterItems = filterItems;
    }

    [HttpGet("GetFilterItems")]
    public async Task<IActionResult> GetFilterItems()
    {
        return Ok(_filterItems);
    }
}