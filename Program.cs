using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks.Dataflow;

namespace slutprojekt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Bank Manager!");
            Console.Clear();
            Console.WriteLine("┌──────────────────────────────┐");
            Console.WriteLine("  Welcome to the Bank Manager!  ");
            Console.WriteLine("└──────────────────────────────┘");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            // Creating the bank instance and default users.
            Bank bank = new Bank(1, "SEB");
            bank.AddUser(new User(1, bank.GetBankName(), 0, "Admin", "admin", "admin", true));
            bank.AddUser(new User(1, bank.GetBankName(), 1, "Marrio", "andy1378", "test123", false));
            bank.AddUser(new User(1, bank.GetBankName(), 2, "Andrei", "andrei", "test123", false));
            Menu menu = new Menu(bank);
            menu.DisplayGuestMenu();
        }
    }
}