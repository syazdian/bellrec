﻿namespace Bell.Reconciliation.Common.Models.Domain;

public class FilterItems
{
    public FilterItems()
    {
        LoBs = new List<LoB>();
        Locations = new List<string>();
        Brands = new List<string>();
        RebateTypes = new List<string>();
    }

    public List<LoB> LoBs { get; set; }
    public List<string> Locations { get; set; }
    public List<string> Brands { get; set; }
    public List<string> RebateTypes { get; set; }
}