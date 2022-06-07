using System;

namespace BackendApi.Models
{
    public class Order
    {
        public int MeetingRoomId { get; set; }
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public DateTime Time { get; set; }
    }
}
