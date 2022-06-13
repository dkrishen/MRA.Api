using BackendApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BackendApi.Repository
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository() : base("http://localhost:30577/")
        {
        }

        public IEnumerable<User> GetUsersByIds(IEnumerable<Guid> ids)
        {
            var jsonResponse = GetRequest("api/user/GetUsersByIds?data=", ids);
            return JsonConvert.DeserializeObject<IEnumerable<User>>(jsonResponse);
        }
    }
}
