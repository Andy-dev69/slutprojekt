using System;
using System.Threading.Tasks.Dataflow;

namespace MyApp
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
            Bank bank = new Bank(1, "SEB");
            User user = new User(1, "SEB", 0, "Test", "Guest", "test123", false);
            Console.WriteLine($"Welcome back {user.GetUsername()}");
            GuestMenu(bank);
        }

        static void GuestMenu(Bank bank)
        {
            Console.Clear();
            bool stop = false;
            while (!stop)
            {
                Console.WriteLine("┌────────── MENU ──────────┐");
                Console.WriteLine("  1. Login                  ");
                Console.WriteLine("  2. Create a new account   ");
                Console.WriteLine("                            ");
                Console.WriteLine("  3. Exit                   ");
                Console.WriteLine("└──────────────────────────┘");

                ConsoleKeyInfo key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        stop = true;
                        string? username;
                        string? password;
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("Login to system.");
                            Console.Write("Username: ");
                            username = Console.ReadLine();
                            Console.Write("Password: ");
                            password = Console.ReadLine();

                            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password)) {
                                if (bank.CheckLogin(username, password)) {
                                    Console.WriteLine("Login successful!");
                                    break;
                                }
                                else {
                                    Console.WriteLine("Login failed. Invalid username or password.");
                                    Console.WriteLine("Press '1' to retry, or any other key to go back.");
                                    string? choice = Console.ReadLine();
                                    if (choice != "1") {
                                        stop = false;
                                        Console.Clear();
                                        break;
                                    }
                                }
                            } else {
                                Console.WriteLine("Username or password cannot be empty.");
                                Console.WriteLine("Press '1' to retry, or any other key to go back.");
                                string? choice = Console.ReadLine();
                                if (choice != "1") {
                                    stop = false;
                                    Console.Clear();
                                    break;
                                }
                            }
                        }
                        break;
                    case ConsoleKey.D2:
                        stop = true;
                        Console.Clear();
                        string? name;
                        string? newUsername;
                        string? newPassword;
                        while (true) {
                            Console.Write("Enter your full name: ");
                            name = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(name)) {
                                Console.Clear();
                                Console.WriteLine("Name cannot be empty.");
                            }
                            else {
                                break;
                            }
                        }

                        // Check if the username already exists
                        while (true) {
                            Console.Write("Enter a username: ");
                            newUsername = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(newUsername) || bank.GetAllUsers().Any(u => u.GetUsername() == newUsername)) {
                                Console.Clear();
                                Console.WriteLine("Username already exists or cannot be empty. Please choose a different username.");
                            }
                            else {
                                break;
                            }
                        }

                        while (true) {
                            Console.Write("Enter a password: ");
                            newPassword = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(newPassword)) {
                                Console.Clear();
                                Console.WriteLine("Password cannot be empty. Please enter a valid password.");
                            }
                            else {
                                break;
                            }
                        }

                        // Create new user account
                        User newUser = new User(1, bank.GetBankName(), 0, name, newUsername, newPassword, false);
                        bank.AddUser(newUser);

                        // Ask user if they want to auto-login to the new account
                        Console.WriteLine("Account created successfully!");
                        Console.WriteLine("Do you want to login to your new account? (yes/no)");
                        string? autoLoginChoice = Console.ReadLine();

                        if (autoLoginChoice?.ToLower() == "yes") {
                            Console.WriteLine("Logging in...");
                            bank.CheckLogin(newUsername, newPassword);
                            Console.WriteLine($"Logged in as: {bank.GetLoggedInUser().GetUsername()}");
                            Console.ReadLine();
                        } else {
                            stop = false;
                        }
                        break;
                    case ConsoleKey.D3:
                        stop = true;
                        Console.Clear();
                        Console.WriteLine("┌────────────────────┐");
                        Console.WriteLine("  System Terminated!  ");
                        Console.WriteLine("└────────────────────┘");
                        break;

                    default:
                        Console.Clear();
                        break;
                }
            }
        }
    }
}