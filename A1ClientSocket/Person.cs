/*  Filename: Person.cs
    By : Taylor Trainor, William Schwetz
    Date : October 5, 2021
    Description: Class contains validation functions
    Class also consains assorted UI for users.
*/
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

        /*  Function:   parseInput
            Purpose:    Check which command a user enters
            Parameters: A string of user input
            Returns:    An int indicating the user's answer
        */
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

        /*  Function:   helpMessage
            Purpose:    Prints out a basic summary of available commands
            Parameters: N/A
            Returns:    N/A
        */
        public void helpMessage()
        {
            Console.WriteLine("\nInsert 'FirstName' 'LastName' 'MM-DD-YYYY'");
            Console.WriteLine("\t- Allows user to add a new entry to the datebase\n");
            Console.WriteLine("Update 'MemberID' 'FirstName' 'LastName' 'MM-DD-YYYY'");
            Console.WriteLine("\t- Update an existing record\n");
            Console.WriteLine("Find 'MemberID'");
            Console.WriteLine("\t- Search for a user's record\n");
        }

        /*  Function:   nameValidation
            Purpose:    Validate a name is alphanumeric
            Parameters: 2 strings of user input
            Returns:    A bool indicating data validity
        */
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

        /*  Function:   numberValidation
            Purpose:    Validate a number 
            Parameters: a string of user input
            Returns:    A bool indicating data validity
        */
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

        /*  Function:   dobValidation
            Purpose:    Validate a dob is intered in valid mm-dd-yyyy format
            Parameters: A string of user input
            Returns:    A bool indicating data validity
        */
        public bool dobValidation(string dob)
        {
            var regx = new Regex(@"^(0[1-9]|1[012])[-](0[1-9]|[12][0-9]|3[01])[-](19|20)\d\d$");
            return (regx.IsMatch(dob));
        }

    }
}
