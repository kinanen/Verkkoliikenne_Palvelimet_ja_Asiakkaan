using System;
using System.Text;
using System.Net.Sockets;

namespace itkp104Ohj
{
    class HttpAsiakas
    {
        static void Main()
        {
            string html = Soketti("http://users.jyu.fi/~arjuvi/opetus/itkp104/testi.html");


        }

        private static string Soketti(string url)
        {
            
            StringBuilder sbUrl = new StringBuilder(url);
            if (url.Contains("http://"))
            {
                string http = "http://";
                int l = http.Length;
                int y = url.LastIndexOf("http://");
                y = y + l;
                sbUrl.Remove(0, y);
            }

            string server = sbUrl.ToString();

            string resurssi; 
            if(server.Contains('/'))
            {
                int z = server.IndexOf('/');
                int l = server.Length;
                l = l-z;
                sbUrl.Remove(z, l);
                StringBuilder resSb = new StringBuilder(server);
                resSb.Remove(0, z);
                resurssi = resSb.ToString();

            }
            else { resurssi = "/"; }

            server = sbUrl.ToString();

            string message = "GET " + resurssi + " HTTP/1.1\r\nHost:" + server + "\r\nConnection:Close\r\n\r\n";

            byte[] snd = Encoding.ASCII.GetBytes(message);

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            s.Connect(server, 80);

            s.Send(snd);

            byte[] receive = new byte[2048];
            int b = s.Receive(receive);
            byte[] webpage = new byte[b];
            for(int i=0; i<b; i++)
            {
                webpage[i] = receive[i];
            }
            string ret = Encoding.UTF8.GetString(webpage);

            StringBuilder sbHtml = new StringBuilder(ret);
            string rivinvaihto = "\r\n\r\n";

            int x = sbHtml.ToString().LastIndexOf(rivinvaihto);
            x = x + rivinvaihto.Length;
            sbHtml.Remove(0,x);


            ret = sbHtml.ToString();

            Console.WriteLine(ret);
            
            Console.ReadKey();

            s.Close();

            return ret;
        }

    } 
}

