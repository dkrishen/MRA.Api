using BackendApi.Models;
using BackendApi.Repository;
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
        IMeetingRoomRepository _meetingRoomRepository;

        public OrderController(IMeetingRoomRepository meetingRoomRepository)
        {
            _meetingRoomRepository = meetingRoomRepository;
        }

        List<Order> orders = new List<Order>()
        {
            new Order
            {
                MeetingRoomId = Guid.Parse("1DDA7260-08E8-4B32-A9EE-F7E1CA69BC9C"),
                OrderId = Guid.Parse("e2371dc9-a849-4f3c-9004-df8fc921c13b"),
                UserId = Guid.Parse("9BD399D4-06D0-43F1-822C-D9DB91B422BA"),
                Time = DateTime.Now
            },
            new Order
            {
                MeetingRoomId = Guid.Parse("2DDA7260-08E8-4B32-A9EE-F7E1CA69BC9C"),
                OrderId = Guid.Parse("e2371dc9-a849-4f3c-9004-df8fc921c13c"),
                UserId = Guid.Parse("9BD399D4-06D0-43F1-822C-D9DB91B422BA"),
                Time = DateTime.Now
            },
            new Order
            {
                MeetingRoomId = Guid.Parse("1DDA7260-08E8-4B32-A9EE-F7E1CA69BC9C"),
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
            var resultOrders = orders;

            var roomIds = resultOrders.Select(o => o.MeetingRoomId).ToHashSet<Guid>();
            var rooms = _meetingRoomRepository.GetRoomsByRoomIds(roomIds);

            var userIds = resultOrders.Select(o => o.UserId);
            // var users = ; //TODO

            var result = new List<OrderViewModel>();
            foreach (var order in resultOrders)
            {
                result.Add(new OrderViewModel
                {
                    Id = order.OrderId,
                    MeetingRoomId = order.MeetingRoomId,
                    MeetingRoomName = rooms.Where(r => r.Id == order.MeetingRoomId).FirstOrDefault().Name,
                    UserId = order.UserId,
                    Username = "tmp null",
                    // Username = users.Where(u => u.id == order.UserId).FirstOrDefault().Username,
                    StartTime = order.Time,
                    EndTime = DateTime.Now
                });
            }

            return Ok(result);
        }

        [HttpGet]
        [Authorize (Roles = "User")]
        [Route("GetOrdersByUser")]
        public IActionResult GetOrdersByUser()
        {
            var userId = Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var resultOrders = orders.Where(order => order.UserId == userId);

            var roomIds = resultOrders.Select(o => o.MeetingRoomId).ToHashSet<Guid>();
            var rooms = _meetingRoomRepository.GetRoomsByRoomIds(roomIds);

            var result = new List<OrderViewModel>();
            foreach(var order in resultOrders)
            {
                result.Add(new OrderViewModel
                {
                    Id = order.OrderId,
                    MeetingRoomId = order.MeetingRoomId,
                    MeetingRoomName = rooms.Where(r => r.Id == order.MeetingRoomId).FirstOrDefault().Name,
                    UserId = order.UserId,
                    Username = "tmp null",
                    // Username = users.Where(u => u.id == order.UserId).FirstOrDefault().Username,
                    StartTime = order.Time,
                    EndTime = DateTime.Now
                });
            }

            return Ok(result);
        }
    }
}
