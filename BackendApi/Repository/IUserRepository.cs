﻿using BackendApi.Models;
using System;
using System.Collections.Generic;

namespace BackendApi.Repository
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetUsersByIds(IEnumerable<Guid> ids);
    }
}
