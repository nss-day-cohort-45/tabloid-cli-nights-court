using System;
using System.Collections.Generic;
using System.Text;

namespace TabloidCLI.UserInterfaceManagers
{
    class BackgroundColor : IUserInterfaceManager
    {
        private  IUserInterfaceManager _parentUI; 
        public BackgroundColor(IUserInterfaceManager parentUI) //does not need a connection string
        {
            _parentUI = parentUI;
        }
        
        public IUserInterfaceManager Execute()
        {

            Console.WriteLine("Color Menu");
            Console.WriteLine(" 1) Red");
            Console.WriteLine(" 2) Dark Blue");
            Console.WriteLine(" 3) Dark Green");
            Console.WriteLine(" 4) Dark Cyan");
            Console.WriteLine(" 5) Dark Red");
            Console.WriteLine(" 6) Dark Magenta");
            Console.WriteLine(" 7) Dark Yellow");
            Console.WriteLine(" 8) Gray");
            Console.WriteLine(" 9) Blue");
            Console.WriteLine(" 10) Green");
            Console.WriteLine(" 11) Reset");
            Console.WriteLine(" 0) Return to Main Menu");


            Console.Write("> ");
            string choice = Console.ReadLine();
            Console.Clear(); //clear console before returning to set entire backgroun not use text background
            switch (choice)
            {
                case "1":
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Clear();
                    return this;
                case "2":
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.Clear();
                    return this;
                case "3":
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.Clear();
                    return this;
                case "4":
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.Clear();
                    return this;
                case "5":
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.Clear();
                    return this;
                case "6":
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    Console.Clear();
                    return this;
                case "7":
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.Clear();
                    return this;
                case "8":
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Clear();
                    return this;
                case "9":
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Clear();
                    return this;
                case "10":
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.Clear();
                    return this;
                case "11":
                    Console.ResetColor();
                    Console.Clear();
                    return this;

                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        } 
    }
}
