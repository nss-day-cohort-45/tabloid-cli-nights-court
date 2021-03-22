using System;
using System.Collections.Generic;
using System.Text;

namespace TabloidCLI.UserInterfaceManagers
{
    class BackgroundColor
    {
        //doesnt need IUserInterfaceManager????
        public void  Execute()
        {

            Console.WriteLine("Color Menu");
            Console.WriteLine(" 1) Red");
            Console.WriteLine(" 2) Dark Blue");
            Console.WriteLine(" 3) Dark Green");
            Console.WriteLine(" 4) Dark Cyan");
            Console.WriteLine(" 5) Dark Red");
            Console.WriteLine(" 6) Magenta");
            Console.WriteLine(" 7) Dark Yellow");
            Console.WriteLine(" 8) Gray");
            Console.WriteLine(" 9) Blue");
            Console.WriteLine(" 10) Green");
            Console.WriteLine(" 0) Return to Main Menu");

            Console.Write("> ");
            string choice = Console.ReadLine();
            Console.Clear();
            switch (choice)
            {
                case "1":
                    Console.BackgroundColor = ConsoleColor.Red;
                    return this;
                case "2":
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    return this;
                case "3":
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    return this;
                case "4":
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    return this;
                case "5":
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                case "6":
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                case "7":
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                case "8":
                    Console.BackgroundColor = ConsoleColor.Gray;
                case "9":
                    Console.BackgroundColor = ConsoleColor.Blue;
                case "10":
                    Console.BackgroundColor = ConsoleColor.Green;

                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        } 
    }
}
