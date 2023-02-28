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

            string testi = Console.ReadLine();
            byte[] snd = Encoding.ASCII.GetBytes(testi);
            IPAddress ip = IPAddress.Loopback;
            IPEndPoint iep = new IPEndPoint(ip, 9800);

            s.SendTo(snd, iep);

            bool t = true;

            while (t == true)
            {
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
                char j = ':';
                string[] v = ret.Split(j);
                Console.WriteLine(ret);

                /*string palvelin = v[0];
                string viesti = v[1];
                Console.WriteLine("Palvelin: " + palvelin + "\nTeksti: " + viesti);
                */
                t = false;

            }


            s.Close();
        }


    }


}