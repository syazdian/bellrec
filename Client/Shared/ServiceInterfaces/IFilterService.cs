﻿namespace Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;

public interface IFilterService
{
    public Task<FilterItems> GetFilterItems();
}