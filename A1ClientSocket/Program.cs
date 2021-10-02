﻿using System;
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
        bool yes = false;
        const int PORT_NO = 5000;
        const string SERVER_IP = "127.0.0.1";
        static void Main(string[] args)
        {
            Person user = new Person();
            bool serverOn = true;
            string input;
            int result;
            //---data to send to the server---

            //---create a TCPClient object at the IP and port no.---
            TcpClient client = new TcpClient(SERVER_IP, PORT_NO); // make this a try-catch
            NetworkStream nwStream = client.GetStream();

            Console.WriteLine("\t\t--Communications with the server have begun--\n\n");

            Console.WriteLine("Enter 'help' if you want to see all viable commands");
            Console.WriteLine("Enter 'q' or 'quit' to quit the program\n\n");
            Console.WriteLine("Enter command: ");

            while (serverOn)
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
                }
                if (result == 5)
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

                    if (user.nameValidation(command[1], command[2]) == false || user.numberValidation(command[3]) == false)
                    {
                        Console.WriteLine("Invalid input");
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
                    if (command.Count() != 5)
                    {
                        continue;
                    }

                    if (user.nameValidation(command[2], command[3]) == false || user.numberValidation(command[4]) == false)
                    {
                        Console.WriteLine("Invalid input");
                        continue;
                    }
                    if (user.numberValidation(command[1]) == false)
                    {
                        Console.WriteLine("Invalid input");
                        continue;
                    }

                    else
                    {
                        Console.WriteLine("\nAttempting to update your records...");
                        byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(input);
                        nwStream.Write(bytesToSend, 0, bytesToSend.Length);

                        //next do return validation
                    }
                }
                if (result == 3) //find
                {
                    if (command.Count() == 2 && user.numberValidation(command[1]) == true)
                    {
                        Console.WriteLine("\nAttempting to update your records...");
                        byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(input);
                        nwStream.Write(bytesToSend, 0, bytesToSend.Length);

                        //validate return input
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input");
                        continue;
                    }
                }

            }


            // -- Here we will get user input 

            // -- Option 1 

            // -- Option 2 

            // -- Option 3 
            //---send the text---
            // -- sending text via socket Console.WriteLine("Sending : " + textToSend);
            // --sending text via socket nwStream.Write(bytesToSend, 0, bytesToSend.Length);

            //---read back the text---
            byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
            Console.WriteLine("Received : " + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));

            client.Close();
        }


    }

}
