﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using HomeWork.Model;

namespace HomeWork.Abstarcts
{
    // Интерфейс. Позволяет исползовать методы получения и отправки сообщения из одного места без копипасты.
    public interface IMessageSourse
    {
        Task SendAsync(NetMessage message, IPEndPoint iPEndPoint, UdpClient UdpClient);
        NetMessage Receive(ref IPEndPoint iPEndPoint, ref UdpClient UdpClient);
    }
}