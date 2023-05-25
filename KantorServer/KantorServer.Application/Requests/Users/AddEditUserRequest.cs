﻿using KantorServer.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Application.Requests.Users
{
    [Serializable]
    public class AddEditUserRequest : BaseServerRequest
    {
        public UserDto User { get; set; }
    }
}
