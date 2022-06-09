using BackendApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BackendApi.Repository
{
    public class MeetingRoomRepository : RepositoryBase , IMeetingRoomRepository
    {
        public MeetingRoomRepository() : base("http://localhost:32218/")
        {

        }

        public IEnumerable<MeetingRoom> GetAllRooms()
        {
            var jsonResponse = GetRequest("api/rooms/GetAllRooms");
            return JsonConvert.DeserializeObject<IEnumerable<MeetingRoom>>(jsonResponse);
        }

        public MeetingRoom GetRoomByRoomId(Guid id)
        {
            var jsonResponse = GetRequest("api/rooms/GetRoomById?data=", id);
            return JsonConvert.DeserializeObject<MeetingRoom>(jsonResponse);
        }

        public IEnumerable<MeetingRoom> GetRoomsByRoomIds(IEnumerable<Guid> ids)
        {
            var jsonResponse = GetRequest("api/rooms/GetRoomsByIds?data=", ids);
            return JsonConvert.DeserializeObject<IEnumerable<MeetingRoom>>(jsonResponse);
        }
    }
}
