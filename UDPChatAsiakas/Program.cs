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
            int eka = 0;
            Console.WriteLine("Liity kirjoittamalla \"nimesi\":");
            while (t)
            {
                IPEndPoint vastIep = new IPEndPoint(IPAddress.Loopback, 9999);
                EndPoint ep = (EndPoint)vastIep;


                string msg = " ";
                if (eka < 1)
                {
                    msg = Console.ReadLine();
                    msg = "JOIN " + msg;
                    eka++;
                }

                
                if (msg == "exit")
                {
                    t = false;
                    byte[] quit = Encoding.UTF8.GetBytes("QUIT");

                    s.SendTo(quit, vastIep);

                }

                byte[] mesB = Encoding.UTF8.GetBytes(msg);


                s.SendTo(mesB, ep);

                byte[] recB = new byte[128];
                int b = s.ReceiveFrom(recB, ref ep);
                byte[] vastaus = new byte[b];
                for (int j = 0; j < b; j++)
                {
                    vastaus[j] = recB[j];
                }
                string ret = Encoding.UTF8.GetString(vastaus);
                char d = ' ';
                Console.WriteLine(ret);
                string[] koodi = ret.Split(d);
                string TILA = "Closed";

                if (koodi[0] == "ACK")
                {
                    TILA = "JOIN";
                    if (koodi[1] == "201")
                    {
                        Console.WriteLine("Odota toista pelaajaa.");
                    }
                    if (koodi[1] == "202")
                    {
                        Console.WriteLine("Aloita Peli: arvaa numero 0-10");
                        TILA = "DATA ";
                        msg = Console.ReadLine();
                        if (msg == "exit") { t = false; }
                        msg = TILA + msg;
                        mesB = Encoding.UTF8.GetBytes(msg);
                        s.SendTo(mesB, ep);
                    }
                    if (koodi[1] == "203")
                    {
                        Console.WriteLine("Vastustaja liittynyt, hän aloittaa");
                    }
                    if (koodi[1] == "401")
                    {
                        Console.WriteLine("Peli jo käynnissä yritä myöhemmin uudelleen.");
                        s.Close();
                    }
                    if (koodi[1] == "407")
                    {
                        Console.WriteLine("Arvaus ei ollut numero.");
                        Console.WriteLine("Aloita Peli: arvaa numero 0-10");
                        msg = Console.ReadLine();
                        if (msg == "exit") { t = false; }
                        mesB = Encoding.UTF8.GetBytes(msg);
                        s.SendTo(mesB, ep);


                    }
                    else { Console.WriteLine(""); }

                }
                if (koodi[0] == "QUIT") { t = false; }

            }
            byte[] B = Encoding.UTF8.GetBytes("QUIT");
            IPEndPoint qIep = new IPEndPoint(IPAddress.Loopback, 9999);
            EndPoint qep = (EndPoint)qIep;

            s.SendTo(B, qep);
            s.Close();


        }


    }


}