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

    class Server
    {
        public const int PORT_NO = 5000;
        public const string SERVER_IP = "127.0.0.1";
        private string dbFileName = "memberDB.json";
        private Members membersDB;
        private int currentConnections = 0;

        public void UpdateMember()
        {
            Console.WriteLine("UPDATE! NOW");
        }

        public bool UpdateRecord(string updatedMember)
        {
            return false;
            //string[] memberValues = newRecord.Split(' ');
        }

        public bool InsertRecord(string newRecord)
        {
            if (membersDB.allMembers.Count < 40001)
            {
                string[] memberValues = newRecord.Split(' ');
                Member tmpMember = new Member { MemberID = (membersDB.allMembers.Count + 1), FirstName = memberValues[0], LastName = memberValues[1], DateOfBirth = DateTime.Parse(memberValues[2]) };

                membersDB.allMembers.Add(tmpMember);
                File.WriteAllText(dbFileName, JsonConvert.SerializeObject(membersDB));
                return true;
            }
            else
            {
                return false;
            }


        }
        public string FindRecord(int idToFind)
        {

            var foundMember = membersDB.allMembers.FirstOrDefault(o => o.MemberID == idToFind);
            if (foundMember != null)
            {
                string retString = foundMember.MemberID.ToString() + " " + foundMember.FirstName + " " + foundMember.LastName + " " + foundMember.DateOfBirth;
                return retString;
            }
            else
            {
                return "NO RECORD";
            }
        }
        public void ParseIncoming(string newMessage)
        {

        }
        public void ServerStartup()
        {
            Console.WriteLine("\t\t--Server start up has begun--\n\n");
            //---listen at the specified IP and port no.---
            IPAddress localAdd = IPAddress.Parse(SERVER_IP);
            TcpListener listener = new TcpListener(localAdd, PORT_NO);
            byte[] returnBuffer = null;
            Console.WriteLine("Listening...");

            listener.Start();
            while (true)
            {
                //---incoming client connected---
                TcpClient client = listener.AcceptTcpClient();
                if (client.Connected)
                {
                    Console.WriteLine("Client connected from " + ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString());
                    currentConnections++;
                }
                //---get the incoming data through a network stream---
                NetworkStream nwStream = client.GetStream();
                byte[] buffer = new byte[client.ReceiveBufferSize];

                //---read incoming stream---
                int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

                //---convert the data received into a string---
                string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                //string dataReceived = "-FIND 2";
                Console.WriteLine("Received : " + dataReceived);

                // Open contents of database
                string jsonString = File.ReadAllText(dbFileName);
                membersDB = JsonConvert.DeserializeObject<Members>(jsonString);

                if (dataReceived.IndexOf("insert ") == 0)
                {
                    if (InsertRecord(dataReceived.Remove(0, 8)))
                    {
                        returnBuffer = Encoding.ASCII.GetBytes("RECORD ADDED");
                    }
                    else
                    {
                        returnBuffer = Encoding.ASCII.GetBytes("RECORD NOT ADDED");
                    }

                    //---write back the text to the client---
                    nwStream.Write(returnBuffer, 0, returnBuffer.Length);
                }
                else if (dataReceived.IndexOf("update ") == 0)
                {
                    if (UpdateRecord(dataReceived.Remove(0, 8)))
                    {
                        returnBuffer = Encoding.ASCII.GetBytes("RECORD ADDED");
                    }
                    else
                    {
                        returnBuffer = Encoding.ASCII.GetBytes("RECORD NOT UPDATED");
                    }

                }
                else if (dataReceived.IndexOf("find ") == 0)
                {
                    returnBuffer = Encoding.ASCII.GetBytes(FindRecord(Int32.Parse(dataReceived.Remove(0, 6))));
                    Console.WriteLine("FOUND : " + System.Text.Encoding.Default.GetString(returnBuffer));
                    //---write back the text to the client---
                    nwStream.Write(returnBuffer, 0, returnBuffer.Length);
                }


                client.Close();
                currentConnections--;
            }
            listener.Stop();

            //Console.ReadLine();
        }
    }
}
