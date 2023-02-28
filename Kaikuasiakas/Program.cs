using System;
using System.Text;
using System.Net.Sockets;

namespace Kaikuasiakas
{
    class Program
    {
        static void Main()
        {
            string testi = Console.ReadLine();


            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.Connect("localhost", 25000);
            byte[] snd = Encoding.ASCII.GetBytes(testi);
            s.Send(snd);

            byte[] receive = new byte[2048];
            int b = s.Receive(receive);
            byte[] vastaus = new byte[b];
            for (int i = 0; i < b; i++)
            {
                vastaus[i] = receive[i];
            }
            string ret = Encoding.UTF8.GetString(vastaus);

            Console.WriteLine(ret);

            int x = ret.IndexOf(';');
            StringBuilder sbP = new StringBuilder(ret);
            int y = sbP.Length;
            sbP.Remove(x, y-x);
            string palvelin = sbP.ToString();

            StringBuilder sbV = new StringBuilder(ret);
            sbV.Remove(0,x+1);
            string viesti = sbV.ToString();

            Console.WriteLine("Palvelin: " + palvelin + "\nTeksti: " + viesti);

            s.Close();

        }
    }
}

