using BackendApi.Models;
using System;
using System.Collections.Generic;

namespace BackendApi.Repository
{
    public interface IOrderRepository
    {
        public IEnumerable<Order> GetOrders();
        public IEnumerable<Order> GetOrdersByUser(Guid id);
        public void AddOrder(Order order);
        public void UpdateOrder(Order order);
        public void DeleteOrder(Guid id);
    }
}
