﻿using System;
using System.Threading.Tasks;
using dfinery.backend.assignment.Models;

namespace dfinery.backend.assignment.Service.Messenger
{
    public interface IEventSQSMessenger
    {
        Task SendMessage(EventCollectReq req);
    }
}
