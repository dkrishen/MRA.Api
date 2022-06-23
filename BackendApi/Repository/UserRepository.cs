﻿using BackendApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BackendApi.Repository
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository() : base("https://localhost:44396/")
        {
        }

        public IEnumerable<UserShortDto> GetUsersByIds(IEnumerable<Guid> ids)
        {
            var jsonResponse = GetRequest("api/user/GetUsersByIds?data=", ids);
            return JsonConvert.DeserializeObject<IEnumerable<UserShortDto>>(jsonResponse);
        }
    }
}