using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CSA_Testat2_client
{
    class Program
    {
        static void Main(string[] args)
        {
            var ipAddress = "192.168.1.13";
            var port = 34343;
            var stringMessage = ""; 
            if (args.Length == 0)
            {
                Console.WriteLine("To few arguments given.");
                Console.WriteLine("Either the first argument is a file or -s to start the robot.");
                Console.ReadKey();
                return;
            }
            if (args[0].ToLower() == "-s")
            {
                stringMessage = "start";
            } else
            {
                if (File.Exists(args[0]))
                {
                    stringMessage = File.ReadAllText(Path.GetFullPath(args[0]));
                } else
                {
                    Console.WriteLine("File not found sorry!");
                    Console.ReadKey();
                    return;
                }
            }
            try
            {
                TcpClient client = new TcpClient(ipAddress, port);
                var message = System.Text.Encoding.ASCII.GetBytes(stringMessage);
                NetworkStream stream = client.GetStream();

                Console.WriteLine("Writing...");
                stream.Write(message, 0, message.Length);
                stream.Close();
                client.Close();
                Console.WriteLine("Done Writing. Pleas press a key to exit.");
            } catch (Exception e)
            {
                Console.WriteLine("Exception appeard. Destination Host unreachable");
            }
            Console.ReadKey();
        }
    }
}
