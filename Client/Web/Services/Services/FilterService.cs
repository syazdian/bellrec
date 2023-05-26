﻿using Bell.Reconciliation.Common.Models.Domain;
using Bell.Reconciliation.Common.Utilities;
using Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;
using System.Net.Http.Json;
using System.Reflection;

namespace Bell.Reconciliation.Frontend.Web.Services;

public class FilterService : IFilterService
{
    private readonly HttpClient _httpClient;
    private string baseurl;

    public FilterService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<FilterItems> GetFilterItems()
    {
        try
        {
            //var dir = Path.GetDirectoryName(typeof(FilterService).Assembly.Location)!;
            //var response = File.ReadAllText(Path.Combine(dir, "Data", "filteritems.txt"));

            //var response = await new HttpClient().GetStringAsync("https://localhost:7131/api/FilterValue/GetFilterItems");
            var response = await _httpClient.GetFromJsonAsync<FilterItems>($"/api/FilterValue/GetFilterItems");

            //var response = await httpClient.GetStringAsync("/api/FilterValue/GetFilterItems");

            // string response = "{\"loBs\":[{\"name\":\"Wireless\",\"subLoBs\":[\"Wireless\",\"IoT\",\"Mobile Data\"]},{\"name\":\"Wireline\",\"subLoBs\":[\"Home Internet\",\"Home Television\",\"Home Phone\",\"SMB Internet\"]}],\"locations\":[\"Canada\"],\"brands\":[\"Bell\",\"Virgin\",\"Lucky Mobile\"],\"rebateTypes\":[\"Commission\",\"DownPayment\",\"Tax\"]}";

            //var path = Directory.GetFiles("C:\\Users\\Public");
            //using (StreamReader sr = File.OpenText("C:\\Users\\Public\\filteritems.txt"))
            //{
            //    response = sr.ReadToEnd();
            //}
            // var response = File.ReadAllText("filteritems.txt");
            //FilterItems filterItems = new FilterItems();

            //if (!string.IsNullOrEmpty(response))
            //{
            //    filterItems = response.JsonDeserialize<FilterItems>();
            //}

            return response;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}