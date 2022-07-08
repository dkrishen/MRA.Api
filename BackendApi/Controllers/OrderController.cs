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
        IUserRepository _userRepository;
        IOrderRepository _orderRepository;

        public OrderController(IMeetingRoomRepository meetingRoomRepository, IUserRepository userRepository, IOrderRepository orderRepository)
        {
            _meetingRoomRepository = meetingRoomRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
        }

        //List<Order> orders = new List<Order>()
        //{
        //    new Order
        //    {
        //        MeetingRoomId = Guid.Parse("1DDA7260-08E8-4B32-A9EE-F7E1CA69BC9C"),
        //        Id = Guid.Parse("e2371dc9-a849-4f3c-9004-df8fc921c13b"),
        //        UserId = Guid.Parse("219d8c42-3ab6-4ca1-80fc-6cdcb9cfffc4"),
        //        Date = DateTime.Now,
        //        StartTime = DateTime.Now,
        //        EndTime = DateTime.Now,
        //    },
        //    new Order
        //    {
        //        MeetingRoomId = Guid.Parse("2DDA7260-08E8-4B32-A9EE-F7E1CA69BC9C"),
        //        Id = Guid.Parse("e2371dc9-a849-4f3c-9004-df8fc921c13c"),
        //        UserId = Guid.Parse("219d8c42-3ab6-4ca1-80fc-6cdcb9cfffc4"),
        //        Date = DateTime.Now,
        //        StartTime = DateTime.Now,
        //        EndTime = DateTime.Now,
        //    },
        //    new Order
        //    {
        //        MeetingRoomId = Guid.Parse("1DDA7260-08E8-4B32-A9EE-F7E1CA69BC9C"),
        //        Id = Guid.Parse("e2371dc9-a849-4f3c-9004-df8fc921c13d"),
        //        UserId = Guid.Parse("fc8c7d78-fad1-4056-903e-df63b65ee79a"),
        //        Date = DateTime.Now,
        //        StartTime = DateTime.Now,
        //        EndTime = DateTime.Now,
        //    }
        //};

        [HttpGet]
        [Authorize]
        [Route("GetAllOrders")]
        public IActionResult GetAllOrders()
        {
            var orders = _orderRepository.GetOrders();

            var roomIds = orders.Select(o => o.MeetingRoomId).ToHashSet<Guid>();
            var rooms = _meetingRoomRepository.GetRoomsByRoomIds(roomIds);

            var userIds = orders.Select(o => o.UserId).ToHashSet<Guid>();
            var users = _userRepository.GetUsersByIds(userIds);

            var result = new List<OrderViewModel>();
            foreach (var order in orders)
            {
                result.Add(new OrderViewModel
                {
                    Id = order.Id,
                    MeetingRoomId = order.MeetingRoomId,
                    MeetingRoomName = rooms.Where(r => r.Id == order.MeetingRoomId).FirstOrDefault().Name,
                    UserId = order.UserId,
                    Username = users.Where(u => u.Id == order.UserId).SingleOrDefault().Username,
                    Date = order.Date,
                    StartTime = (order.StartTime.Hours < 10 ? '0' + order.StartTime.Hours.ToString() : order.StartTime.Hours.ToString()) + ':' + (order.StartTime.Minutes < 10 ? '0' + order.StartTime.Minutes.ToString() : order.StartTime.Minutes.ToString()),
                    EndTime = (order.EndTime.Hours < 10 ? '0' + order.EndTime.Hours.ToString() : order.EndTime.Hours.ToString()) + ':' + (order.EndTime.Minutes < 10 ? '0' + order.EndTime.Minutes.ToString() : order.EndTime.Minutes.ToString())
                });
            }

            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        [Route("GetOrdersByUser")]
        public IActionResult GetOrdersByUser()
        {
            var userId = Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var orders = _orderRepository.GetOrdersByUser(userId);

            var roomIds = orders.Select(o => o.MeetingRoomId).ToHashSet<Guid>();
            var rooms = _meetingRoomRepository.GetRoomsByRoomIds(roomIds);

            var userIds = orders.Select(o => o.UserId).ToHashSet<Guid>();
            var users = _userRepository.GetUsersByIds(userIds);

            var result = new List<OrderViewModel>();
            foreach (var order in orders)
            {
                result.Add(new OrderViewModel
                {
                    Id = order.Id,
                    MeetingRoomId = order.MeetingRoomId,
                    MeetingRoomName = rooms.Where(r => r.Id == order.MeetingRoomId).FirstOrDefault().Name,
                    UserId = order.UserId,
                    Username = users.Where(u => u.Id == order.UserId).SingleOrDefault().Username,
                    Date = order.Date,
                    StartTime = (order.StartTime.Hours < 10 ? '0' + order.StartTime.Hours.ToString() : order.StartTime.Hours.ToString()) + ':' + (order.StartTime.Minutes < 10 ? '0' + order.StartTime.Minutes.ToString() : order.StartTime.Minutes.ToString()),
                    EndTime = (order.EndTime.Hours < 10 ? '0' + order.EndTime.Hours.ToString() : order.EndTime.Hours.ToString()) + ':' + (order.EndTime.Minutes < 10 ? '0' + order.EndTime.Minutes.ToString() : order.EndTime.Minutes.ToString())
                });
            }

            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        [Route("AddOrder")]
        public IActionResult AddOrder([FromBody] OrderInputDto order)
        {
            var userId = Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

            // TODO: make this with using automapper and in logic layer
            var newOrder = new Order()
            {
                MeetingRoomId = new Guid("1DDA7260-08E8-4B32-A9EE-F7E1CA69BC9C"),
                UserId = userId,
                Date = DateTime.Parse(order.Date),
                StartTime = TimeSpan.Parse(order.StartTime),
                EndTime = TimeSpan.Parse(order.EndTime),
                Id = Guid.NewGuid()
            };


            _orderRepository.AddOrder(newOrder);
            //{
            //    MeetingRoomId = Guid.Parse("1DDA7260-08E8-4B32-A9EE-F7E1CA69BC9C"),
            //    Id = Guid.Parse("e2371dc9-a849-4f3c-9004-df8fc921c13d"),
            //    UserId = Guid.Parse("fc8c7d78-fad1-4056-903e-df63b65ee79a"),
            //    Date = DateTime.Parse(order.Date),
            //    StartTime = DateTime.Parse(order.StartTime),
            //    EndTime = DateTime.Parse(order.EndTime),
            //});

            return Ok();
        }

        [HttpDelete]
        [Authorize]
        [Route("DeleteOrder")]
        public IActionResult DeleteOrder([FromBody] GuidDto data)
        {

            _orderRepository.DeleteOrder(data.id);

            return Ok();
        }

        [HttpPut]
        [Authorize]
        [Route("UpdateOrder")]
        public IActionResult UpdateOrder([FromBody] OrderEditDto order)
        {


      //  TODO: make this with using automapper and in logic layer
            var newOrder = new Order()
            {
                MeetingRoomId = order.Id,
                UserId = order.UserId,
                Date = DateTime.Parse(order.Date),
                StartTime = TimeSpan.Parse(order.StartTime),
                EndTime = TimeSpan.Parse(order.EndTime),
                Id = order.Id
            };

            _orderRepository.UpdateOrder(newOrder);

            return Ok();
        }

    }
}
