using BackendApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BackendApi.Repository
{
    public class OrderRepository : RepositoryBase , IOrderRepository
    {
        public OrderRepository() : base("https://localhost:44301/")
        {

        }

        public void AddOrder(Order order)
        {
            PostRequest("api/book/AddBook?data=", order);
        }

        public IEnumerable<Order> GetOrders()
        {
            var jsonResponse = GetRequest("api/book/GetAllBooks");
            return JsonConvert.DeserializeObject<IEnumerable<Order>>(jsonResponse);
        }

        public IEnumerable<Order> GetOrdersByUser(Guid id)
        {
            var jsonResponse = GetRequest("api/book/GetBooksByUserId?data=", id);
            return JsonConvert.DeserializeObject<IEnumerable<Order>>(jsonResponse);
        }
    }
}
