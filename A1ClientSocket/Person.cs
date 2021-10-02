using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace A1Client
{
    class Person
    {
        public int MemberID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int parseInput(string message)
        {
            if (message.ToLower() == "insert")
            {
                return 1;
            }
            if (message.ToLower() == "update")
            {
                return 2;
            }
            if (message.ToLower() == "find")
            {
                return 3;
            }
            if (message.ToLower() == "help")
            {
                return 4;
            }
            if (message.ToLower() == "q" || message.ToLower() == "quit")
            {
                return 5;
            }
            else
            {
                return 0;
            }
        }

        public void helpMessage()
        {
            Console.WriteLine("\nInsert 'FirstName' 'LastName' 'YYMMDD'\n");
            Console.WriteLine("\tAllows user to add a new entry to the datebase\n");
            Console.WriteLine("Update 'MemberID' 'FirstName' 'LastName' 'YYMMDD'\n");
            Console.WriteLine("\tUpdate an existing record\n");
            Console.WriteLine("Find 'MemberID\n");
            Console.WriteLine("\tSearch for a user's record\n");
        }

        public bool nameValidation(string first, string last)
        {
            bool validate = false;
            if (Regex.IsMatch(first, "^[a-zA-Z]*$") && Regex.IsMatch(first, "^[a-zA-Z]*$"))
            {
                validate = true;
                return validate;
            }
            else
            {
                return validate;
            }
        }

        public bool numberValidation(string number)
        {
            bool validate = false;

            if (Regex.IsMatch(number, "^[0-9]*$"))
            {
                validate = true;
                return validate;
            }
            else
            {
                return validate;
            }
        }

        public bool dobValidation(string dob)
        {
            var regx = new Regex(@"^(0[1-9]|1[012])[- ](0[1-9]|[12][0-9]|3[01])[-](19|20)\d\d$");
            return (regx.IsMatch(dob));
        }

    }
}
