﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Application.Responses
{
    public class LoginResponse : BaseServerResponse
    {
        public string SynchronizationKey { get; set; }
        public LoginResponse(bool isCorrect, string successMsg, string failMsg) : base(isCorrect, successMsg, failMsg)
        {
        }
    }
}
