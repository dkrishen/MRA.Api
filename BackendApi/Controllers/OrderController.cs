using BackendApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        List<Order> orders = new List<Order>()
        {
            new Order
            {
                MeetingRoomId = 1,
                OrderId = Guid.Parse("e2371dc9-a849-4f3c-9004-df8fc921c13b"),
                UserId = Guid.Parse("e2371dc9-a849-4f3c-9004-df8fc921c13a"),
                Time = DateTime.Now
            },
            new Order
            {
                MeetingRoomId = 2,
                OrderId = Guid.Parse("e2371dc9-a849-4f3c-9004-df8fc921c13c"),
                UserId = Guid.Parse("e2371dc9-a849-4f3c-9004-df8fc921c13a"),
                Time = DateTime.Now
            },
            new Order
            {
                MeetingRoomId = 1,
                OrderId = Guid.Parse("e2371dc9-a849-4f3c-9004-df8fc921c13d"),
                UserId = Guid.Parse("7b0a1ec1-80f5-46b5-a108-fb938d3e26c0"),
                Time = DateTime.Now
            }
        };

        [HttpGet]
        [Route("GetAllOrders")]
        public IActionResult GetAllOrders()
        {
            return Ok(orders);
        }

        [HttpGet]
        [Authorize (Roles = "User")]
        [Route("GetOrdersByUser")]
        public IActionResult GetOrdersByUser()
        {
            var userId = Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var result = orders.Where(order => order.UserId == userId);
            
            return Ok(result);
        }
    }
}
