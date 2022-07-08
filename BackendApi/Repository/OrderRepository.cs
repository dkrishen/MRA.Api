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
            Request("api/book/AddBook?data=", "POST", order);
        }

        public void DeleteOrder(Guid id)
        {
            Request("api/book/DeleteBook?data=", "DELETE", id);
        }

        public IEnumerable<Order> GetOrders()
        {
            var jsonResponse = Request("api/book/GetAllBooks", "GET");
            return JsonConvert.DeserializeObject<IEnumerable<Order>>(jsonResponse);
        }

        public IEnumerable<Order> GetOrdersByUser(Guid id)
        {
            var jsonResponse = Request("api/book/GetBooksByUserId?data=", "GET", id);
            return JsonConvert.DeserializeObject<IEnumerable<Order>>(jsonResponse);
        }

        public void UpdateOrder(Order order)
        {
            Request("api/book/UpdateBook?data=", "PUT", order);
        }
    }
}
