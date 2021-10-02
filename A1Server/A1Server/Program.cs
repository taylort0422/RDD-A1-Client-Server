using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using Newtonsoft.Json;
using System.IO;



namespace A1Server
{
    class Program
    {


        static void Main(string[] args)
        {
            Server newServer = new Server();
            newServer.ServerStartup();

            //SocketListener newServer = new SocketListener();
            //newServer.StartServer();
            //string dbFileName = "memberDB.json";
            //string jsonString = File.ReadAllText(dbFileName);
            //Members newMembers = JsonConvert.DeserializeObject<Members>(jsonString);
            //Console.WriteLine(newMembers.allMembers.First().FirstName);
            //Console.WriteLine(newMembers.allMembers.Count);
            Console.ReadKey();
        }
    }
}
