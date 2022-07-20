using BackendApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BackendApi.Repository
{
    public class MeetingRoomRepository : RepositoryBase , IMeetingRoomRepository
    {
        // TODO: move URL to appsetting
        public MeetingRoomRepository() : base("http://localhost:32218/")
        {

        }

        public IEnumerable<MeetingRoom> GetAllRooms()
        {
            var jsonResponse = Request("api/rooms/GetAllRooms", "GET");
            return JsonConvert.DeserializeObject<IEnumerable<MeetingRoom>>(jsonResponse);
        }

        public MeetingRoom GetRoomByRoomId(Guid id)
        {
            var jsonResponse = Request("api/rooms/GetRoomById?data=", "GET", id);
            return JsonConvert.DeserializeObject<MeetingRoom>(jsonResponse);
        }

        public IEnumerable<MeetingRoom> GetRoomsByRoomIds(IEnumerable<Guid> ids)
        {
            var jsonResponse = Request("api/rooms/GetRoomsByIds?data=", "GET", ids);
            return JsonConvert.DeserializeObject<IEnumerable<MeetingRoom>>(jsonResponse);
        }
    }
}
