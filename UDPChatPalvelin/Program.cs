using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

namespace UDPAsiakas
{
    class Program
    {
        static void Main()
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            s.Bind(new IPEndPoint(IPAddress.Any, 8900));
            
            bool t = true;

            List<EndPoint> lista = new List<EndPoint> { };

            while(t)
            {
                byte[] receive = new byte[128];
                IPEndPoint vastIep = new IPEndPoint(IPAddress.Loopback, 0);
                EndPoint ep = (EndPoint)vastIep;

                int b = s.ReceiveFrom(receive, ref ep);

                if (!lista.Contains(ep))
                {
                    lista.Add(ep);
                }

                byte[] vastaus = new byte[b];
                for (int i = 0; i < b; i++)
                {
                    vastaus[i] = receive[i];
                }
                string ret = Encoding.UTF8.GetString(vastaus);
                string lahettaja = ep.ToString();

                ret = "[" + lahettaja + "]" + ": "+ ret;
                Console.WriteLine(ret);
                byte[] kaiku = Encoding.UTF8.GetBytes(ret);
                foreach(EndPoint x in lista)
                {
                    s.SendTo(kaiku, x);
                }

            }




        }


    }


}