﻿using Bell.Reconciliation.App.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Bell.Reconciliation.App.Server.Controllers;

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
        _filterItems.Locations.Add("Canada");
        return Ok(_filterItems);
    }
}