using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Kaikupalvelin
{
    class Program
    {
        static void Main()
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse("127.0.0.1");

            IPEndPoint iep = new IPEndPoint(ip, 25000);
            s.Bind(iep);
            s.Listen(1);

            
            Socket client = s.Accept();


            byte[] receive = new byte[2048];

            int b = client.Receive(receive);

            byte[] viesti = new byte[b];
            for (int i = 0; i < b; i++)
            {
                    viesti[i] = receive[i];
            }

            string ret = Encoding.UTF8.GetString(viesti);

            Console.WriteLine(ret);
            StringBuilder sb = new StringBuilder(ret);
            sb.Insert(0, "Otson palvelin palveluksessa; ");
            ret = sb.ToString();
            viesti = Encoding.UTF8.GetBytes(ret);

            client.Send(viesti);

            s.Close();

        }
    }
}

