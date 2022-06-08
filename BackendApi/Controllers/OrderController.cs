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
                UserId = Guid.Parse("9BD399D4-06D0-43F1-822C-D9DB91B422BA"),
                Time = DateTime.Now
            },
            new Order
            {
                MeetingRoomId = 2,
                OrderId = Guid.Parse("e2371dc9-a849-4f3c-9004-df8fc921c13c"),
                UserId = Guid.Parse("9BD399D4-06D0-43F1-822C-D9DB91B422BA"),
                Time = DateTime.Now
            },
            new Order
            {
                MeetingRoomId = 1,
                OrderId = Guid.Parse("e2371dc9-a849-4f3c-9004-df8fc921c13d"),
                UserId = Guid.Parse("ECD0CB0A-0148-402D-8689-59712CFB44B3"),
                Time = DateTime.Now
            }
        };

        [HttpGet]
        [Authorize(Roles = "User")]
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
