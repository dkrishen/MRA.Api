using BackendApi.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BackendApi.Repository
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration.GetSection("Auth").GetValue<string>("Issuer"))
        {
        }

        public IEnumerable<UserShortDto> GetUsersByIds(IEnumerable<Guid> ids)
        {
            var jsonResponse = Request("api/user/GetUsersByIds?data=", "GET", ids);
            return JsonConvert.DeserializeObject<IEnumerable<UserShortDto>>(jsonResponse);
        }
    }
}
