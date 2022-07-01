using System;

namespace BackendApi.Models
{
    public class OrderInputDto
    {
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
