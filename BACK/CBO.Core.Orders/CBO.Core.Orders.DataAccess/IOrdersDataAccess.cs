﻿namespace CBO.Core.Orders.DataAccess
{
    public interface IOrdersDataAccess
    {
        Task<string> Login(string clientId, string password);
    }
}