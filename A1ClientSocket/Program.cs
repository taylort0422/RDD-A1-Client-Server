/*  Filename: Program.cs
    By : Taylor Trainor, William Schwetz
    Date : October 5, 2021
    Description: File instantiates a client-side socket. 
                 The socket parses user input and then sends data
                 to a server.
*/

/*
    *    Title:     Send and receive semple text over a socket
    *    Authour:   Nudier Mena
    *    Date:      April 16, 2012
    *    Version:   1
    *    Availability: https://stackoverflow.com/questions/10182751/server-client-send-receive-simple-text
    */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Net.Mail;

namespace A1Client
{
    class Program
    {
        const int PORT_NO = 5000;
        const string SERVER_IP = "127.0.0.1";
        static void Main(string[] args)
        {
            Person user = new Person();
            bool serverOn = true;
            string input;
            int result;
            //---data to send to the server---
            TcpClient client = null;
            //---create a TCPClient object at the IP and port no.---
            try 
            { 
                
                client = new TcpClient(SERVER_IP, PORT_NO); 
     
                NetworkStream nwStream = client.GetStream();

                Console.WriteLine("\t\t--Communications with the server have begun--\n\n");
                Console.WriteLine("Enter 'help' if you want to see all viable commands");
                Console.WriteLine("Enter 'q' or 'quit' to quit the program\n\n");
                Console.WriteLine("Enter command: ");

                while (serverOn == true)
                {

                    input = Console.ReadLine();
                    string[] command = input.Split(' ');
                    result = user.parseInput(command[0]);

                    if (result == 0) //error
                    {
                        Console.WriteLine("Invalid input\n");
                        continue;
                    }
                    if (result == 4) //help menu
                    {
                        user.helpMessage();
                        continue;
                    }
                    if (result == 5) //quit
                    {
                        serverOn = false;
                        break;
                    }
                    //socket communication happens here and below

                    if (result == 1) //insert
                    {
                        if (command.Count() != 4)
                        {
                            continue;
                        }

                        if (user.nameValidation(command[1], command[2]) == false || user.dobValidation(command[3]) == false)
                        {
                            Console.WriteLine("Invalid Input\n");
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("\nAttempting to insert new entry...");
                            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(input);
                            nwStream.Write(bytesToSend, 0, bytesToSend.Length);

                            //next do return validation
                        }
                    }

                    if (result == 2) //update
                    {
                        if (command.Count() != 5 || user.nameValidation(command[2], command[3]) == false || user.dobValidation(command[4]) == false || user.numberValidation(command[1]) == false)
                        {
                            Console.WriteLine("Invalid Input\n");
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("\nAttempting to update your records...");
                            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(input);
                            nwStream.Write(bytesToSend, 0, bytesToSend.Length);
                        }
                    }
                    if (result == 3) //find
                    {
                        if (command.Count() == 2 && user.numberValidation(command[1]) == true)
                        {
                            Console.WriteLine("\nSearching for your record...");
                            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(input);
                            nwStream.Write(bytesToSend, 0, bytesToSend.Length);

                            //validate return input
                        }
                        else
                        {
                            Console.WriteLine("Invalid Input\n");
                            continue;
                        }
                    }
                    //receive reply from server before re-starting while loop

                    byte[] bytesToRead = new byte[client.ReceiveBufferSize];
                    int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
                    Console.WriteLine("Received : " + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead) + "\n");
                }
                
            }
            catch (System.Net.Sockets.SocketException e)
            {
                Console.WriteLine("--Cannot connect to the server--");
            }

            Console.ReadLine();
        }
    }
}

