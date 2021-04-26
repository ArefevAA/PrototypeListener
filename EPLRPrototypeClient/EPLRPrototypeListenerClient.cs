using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace EPLRPrototypeClient
{
    class EPLRPrototypeListenerClient
    {
        const int PORT = 5006;
        static void Main(string[] args)
        {
            IPHostEntry HostEntry = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = HostEntry.AddressList[1];

            TcpClient client = null;
            try
            {
                Console.WriteLine("Please, write a message like Square:int or Inc:int.\nFor example, you can write Square:5\n");
                Console.Write("Enter your message:");
                string message = Console.ReadLine();
                client = new TcpClient(ipAddress.ToString(), PORT);
                NetworkStream stream = client.GetStream();

                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine(message);
                writer.Flush();

                StreamReader reader = new StreamReader(stream);
                message = reader.ReadLine();
                Console.WriteLine("Server answer: " + message);

                reader.Close();
                writer.Close();
                stream.Close();
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (client != null)
                    client.Close();
            }
        }
    }
}
