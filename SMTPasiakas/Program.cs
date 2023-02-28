using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace SMTPasiakas
{
    class Program
    {
        static void Main(string[] args)
        {
            

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


            try
            {
                s.Connect("localhost", 25000);
            }
            catch (Exception ex)
            {
                Console.WriteLine("virhe :" + ex.Message);
                Console.ReadKey();
                return;
            }

            NetworkStream ns = new NetworkStream(s);
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            Boolean whl = true;
            string msg;
            string inpt;
            string ans; 
            while(whl)
            {
                msg = sr.ReadLine();
                Console.WriteLine(msg);
                string[] status = msg.Split(' ');
                
                switch (status[0])
                {
                    case "220":
                        sw.WriteLine("HELO");
                        break;
                    case "221":
                        whl = false;
                        break;
                    case "250":
                        switch (status[1])
                        {
                            case "2.1.0":
                                Console.WriteLine("Anna vastaanottajan osoite");
                                inpt = Console.ReadLine();
                                sw.WriteLine("RCPT TO: " + inpt);
                                break;

                            case "2.1.5":
                                sw.WriteLine("DATA: ");
                                break;

                            case "2.0.0":
                                Console.WriteLine("Viesti lähetetty, ohelma sammutetaan");
                                sw.WriteLine("QUIT\r\n");
                                break;

                            default:
                                Console.WriteLine("Anna sähköpostiosoite");
                                inpt = Console.ReadLine();
                                sw.WriteLine("MAIL FROM: " + inpt );
                                //Console.WriteLine("250 virhe");
                                //ans = sr.ReadLine();

                                //Console.WriteLine(ans);
                                                                
                                break;


                        }
                        break;

                    case "354":
                        Console.WriteLine("Syötä viesti");
                        inpt = Console.ReadLine();
                        sw.WriteLine(inpt);
                        sw.WriteLine(".");
                        //sw.Flush();
                        break;

                    
                    default:
                        Console.WriteLine("Default virhe");
                        Console.ReadKey();
                        sw.WriteLine("QUIT\r\n");
                        break;
                }
                sw.Flush();

            } 
            

            s.Close();
        }

        public static string smtpAsiakas(string vastaanotettu)
        {
            string lahetettava;
            string[] t = vastaanotettu.Split(' ');

            if (t[0] == "220")
            {
                lahetettava = "HELO jyu.fi\r\n";
            }
            else if (t[0] == "250")
            {
                if (t[1] == "2.1.0")
                {
                    lahetettava = "RCPT TO: saaja@g.fi\r\n";
                }
                else if (t[1] == "2.1.5")
                {
                    lahetettava = "DATA: tassa sahkopostiviesti\r\n";
                }
                else if (t[1] == "2.0.0")
                {
                    lahetettava = "QUIT\r\n";
                }
                else
                {
                    lahetettava = "MAIL FROM: lahettaja@g.fi\r\n";
                }

            }
            else if (t[0] == "354")
            {
                lahetettava = ".\r\n";
            }

            else { lahetettava = "QUIT\r\n"; }
            Console.Write(vastaanotettu);

            Console.Write(lahetettava);
            return lahetettava;
        }
    }
    }
}

