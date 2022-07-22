using BackendApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BackendApi.Repository
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        // TODO: move URL to appsetting
        public UserRepository() : base("http://host.docker.internal:5000/")
        {
        }

        public IEnumerable<UserShortDto> GetUsersByIds(IEnumerable<Guid> ids)
        {
            var jsonResponse = Request("api/user/GetUsersByIds?data=", "GET", ids);
            return JsonConvert.DeserializeObject<IEnumerable<UserShortDto>>(jsonResponse);
        }
    }
}
