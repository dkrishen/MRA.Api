using BackendApi.Models;
using System;
using System.Collections.Generic;

namespace BackendApi.Repository
{
    public interface IMeetingRoomRepository
    {
        public IEnumerable<MeetingRoom> GetAllRooms();
        public MeetingRoom GetRoomByRoomId(Guid id);
        public IEnumerable<MeetingRoom> GetRoomsByRoomIds(IEnumerable<Guid> id);
    }
}
