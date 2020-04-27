﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatRoom
{
    class Program
    {
        static void Main(string[] args)
        {
            #region server
            const string ip = "127.0.0.1";
            const int host = 8080;

            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), host);
            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpSocket.Bind(tcpEndPoint);
            tcpSocket.Listen(int.MaxValue);

            while(true)
            {
                var listeren = tcpSocket.Accept();
                var buffer = new byte[256];
                var size = 0;
                var data = new StringBuilder();

                do
                {
                    size = listeren.Receive(buffer);
                    data.Append(Encoding.UTF8.GetString(buffer, 0, size));
                }
                while (listeren.Available > 0);

                Console.WriteLine(data);
                listeren.Send(Encoding.UTF8.GetBytes("Сообщения отправлено."));

                listeren.Shutdown(SocketShutdown.Both);
                listeren.Close();
            }
            #endregion
        }
    }
}