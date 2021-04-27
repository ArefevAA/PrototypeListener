using System;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace EPLRPrototypeListener
{
    public class EPLRPrototypeListenerServer
    {
        const int PORT = 5006;
        static TcpListener listener;
        static void Main(string[] args)
        {
            IPHostEntry HostEntry = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = HostEntry.AddressList[1];

            try
            {
                listener = new TcpListener(ipAddress, PORT);
                listener.Start();
                Console.WriteLine("Waiting new connections...");

                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();

                    StreamReader reader = new StreamReader(stream);
                    string clientMessage = reader.ReadLine();
                    Console.WriteLine("Input message: " + clientMessage);
                    string serverAnswer = "Unknown input value";

                    if (clientMessage.IndexOf(":") >= 0
                        && int.TryParse(clientMessage.Substring(clientMessage.IndexOf(":") + 1), out int number)
                        )
                    {
                        if (clientMessage.StartsWith("Square")) {
                            serverAnswer = SquareCalc(number).ToString();
                        }
                        else if (clientMessage.StartsWith("Inc"))
                        {
                            serverAnswer = IncCalc(number).ToString();
                        }
                        else
                        {
                            throw new Exception("Unknown input command");
                        }
                    }
                    else
                    {
                        throw new Exception("Unknown input value");
                    }

                    StreamWriter writer = new StreamWriter(stream);
                    writer.WriteLine(serverAnswer);
                    Console.WriteLine("Output message: " + serverAnswer);

                    writer.Close();
                    reader.Close();
                    stream.Close();
                    client.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (listener != null)
                    listener.Stop();
            }
        }

        public static int SquareCalc(int number)
        {
            return ClassLibF.SimpleArithmometer.SquareCalc(number);
        }

        public static int IncCalc(int number)
        {
            return ClassLibF.SimpleArithmometer.IncCalc(number);
        }
    }
}
