using System;

namespace BackendApi.Models
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public Guid MeetingRoomId { get; set; }
        public string MeetingRoomName { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
