using System;

namespace BackendApi.Models
{
    public class MeetingRoom
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
