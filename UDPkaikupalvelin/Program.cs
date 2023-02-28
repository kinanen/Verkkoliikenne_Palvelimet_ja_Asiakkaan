using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace UDPAsiakas
{
    class Program
    {
        static void Main()
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            s.Bind(new IPEndPoint(IPAddress.Any, 8900));

            byte[] receive = new byte[128];
            IPEndPoint vastIep = new IPEndPoint(IPAddress.Loopback, 0);
            EndPoint ep = (EndPoint)vastIep;

            int b = s.ReceiveFrom(receive, ref ep);

            byte[] vastaus = new byte[b];
            for (int i = 0; i < b; i++)
            {
                vastaus[i] = receive[i];
            }
            string ret = Encoding.UTF8.GetString(vastaus);
            string palvelin = "Otso Kinasen UDP palvelin: ";
            ret = palvelin + ret;
            Console.WriteLine(ret);
            byte[] kaiku = Encoding.UTF8.GetBytes(ret);
            IPAddress ip = IPAddress.Loopback;
            IPEndPoint iep = new IPEndPoint(ip, 9800);

            s.SendTo(kaiku, iep);

            s.Close();
        }


    }


}