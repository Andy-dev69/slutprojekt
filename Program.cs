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
            bank.AddUser(new User(1, bank.GetBankName(), 0, "Guest", "none", "none", false));
            bank.AddUser(new User(1, bank.GetBankName(), 1, "Marrio", "marrio", "test123", false));
            bank.AddUser(new User(1, bank.GetBankName(), 2, "Andrei", "andrei", "test123", false));
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
                Console.Write("Input: ");

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
                                    MainMenu(bank);
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
                        List<User> users = bank.GetAllUsers();
                        User newUser = new User(1, bank.GetBankName(), users.Count + 1, name, newUsername, newPassword, false);
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
                            MainMenu(bank);
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

        static void MainMenu(Bank bank)
        {
            Console.Clear();
            bool stop = false;
            while (!stop)
            {
                User user = bank.GetLoggedInUser();
                Console.WriteLine($"Logged in as: {user.GetUsername()}");
                Console.WriteLine($"Balance: {user.GetAccountBalance()} ");
                Console.WriteLine("┌──────────── MENU ────────────┐");
                Console.WriteLine("  1. Transfer Money             ");
                Console.WriteLine("  2. Request Money              ");
                Console.WriteLine("  3. Last Payments              ");
                Console.WriteLine($"  4. Check Invoices {user.GetInvoicesNumber()}            ");
                Console.WriteLine("  5. Account Details            ");
                Console.WriteLine("  6. Switch Account             ");
                Console.WriteLine("                                ");
                Console.WriteLine("  7. Exit                       ");
                Console.WriteLine("└──────────────────────────────┘");
                Console.Write("Input: ");

                ConsoleKeyInfo key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        stop = Transfer(bank, user);
                        Console.Clear();
                        if (stop) {
                            MainMenu(bank);
                        }
                        break;
                    case ConsoleKey.D2:
                        Console.Clear();
                        stop = Request(bank, user);
                        Console.Clear();
                        if (stop) {
                            MainMenu(bank);
                        }
                        break;
                    case ConsoleKey.D3:
                        stop = true;

                        break;
                    case ConsoleKey.D4:
                        stop = true;

                        break;
                    case ConsoleKey.D5:
                        stop = true;

                        break;
                    case ConsoleKey.D6:
                        stop = true;
                        Console.Clear();
                        GuestMenu(bank);
                        break;
                    case ConsoleKey.D7:
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

        static bool Transfer(Bank bank, User user) {
            // Create a money transfer instance
            MoneyTransfer moneyTransfer = new MoneyTransfer(bank);

            bool backToMainMenu = false;
            while (!backToMainMenu) {
                // Ask user for transfer details
                Console.WriteLine("Enter transfer details:");
                int senderAccountId = user.GetUserId();

                Console.Write("Receiver Account ID: ");
                int receiverAccountId = 0;
                while (!int.TryParse(Console.ReadLine(), out receiverAccountId) || receiverAccountId == senderAccountId) {
                    Console.Clear();
                    Console.WriteLine("Invalid input. Please enter a valid receiver account ID!");
                    Console.Write("Receiver Account ID: ");
                }

                Console.Write("Amount to transfer: ");
                int amount = 0;
                while (!int.TryParse(Console.ReadLine(), out amount)) {
                    Console.Clear();
                    Console.WriteLine("Invalid input. Please enter a valid amount to transfer!");
                    Console.Write("Amount to transfer: ");
                }
                Console.WriteLine($"Are you sure that you want to transfer {amount:C} to {bank.GetAccountById(receiverAccountId).GetUserName()}'s account? (yes/no)");
                string? sure = Console.ReadLine();
                if (sure?.ToLower() != "yes") {
                    Console.Clear();
                    Console.WriteLine("Transfer terminated.");
                    return false;
                }
                Console.Write("Confirmation Password: ");
                string? confirmationPassword = Console.ReadLine();

                Console.Clear();

                // Perform money transfer
                bool transferSuccess = moneyTransfer.TransferMoney(senderAccountId, receiverAccountId, amount, confirmationPassword);

                if (transferSuccess) {
                    Console.WriteLine("Transfer successful.");
                    Console.WriteLine($"New balance: {bank.GetAccountById(senderAccountId).GetAccountBalance()}");
                    Console.ReadKey();
                    bank.CreateAndSendInvoice(bank.GetAccountById(senderAccountId).GetInvoices().Count + 1, senderAccountId, "Transfer", amount, DateTime.Today.AddDays(0), true); // Create an invoice
                    backToMainMenu = true;
                } else {
                    Console.WriteLine("Transfer failed.");

                    Console.WriteLine("Do you want to try again? (yes/no)");
                    string? tryAgainResponse = Console.ReadLine();
                    if (tryAgainResponse?.ToLower() == "yes") {
                        Console.Clear();
                        return false;
                    } else {
                        backToMainMenu = true;
                    }
                }
            }
            return true;
        }
        
        static bool Request(Bank bank, User user) {
            bool backToMainMenu = false;
            while (!backToMainMenu) {
                // Ask user for request details
                Console.WriteLine("Enter request details:");
                int senderAccountId = user.GetUserId();

                Console.Write("Request From Account ID: ");
                int receiverAccountId = 0;
                while (!int.TryParse(Console.ReadLine(), out receiverAccountId) || receiverAccountId == senderAccountId) {
                    Console.Clear();
                    Console.WriteLine("Invalid input. Please enter a valid account ID!");
                    Console.Write("Request From Account ID: ");
                }

                Console.Write("Why are you requesting this amount?: ");
                string? title = Console.ReadLine();

                Console.Write("Amount you are requesting: ");
                int amount = 0;
                while (!int.TryParse(Console.ReadLine(), out amount)) {
                    Console.Clear();
                    Console.WriteLine("Invalid input. Please enter a valid amount!");
                    Console.Write("Amount you are requesting: ");
                }

                Console.Write("Due to in? (days): ");
                int due = 0;
                while (!int.TryParse(Console.ReadLine(), out due)) {
                    Console.Clear();
                    Console.WriteLine("Invalid input. Please enter a valid due time!");
                    Console.Write("Due to in? (days): ");
                }

                Console.WriteLine($"Are you sure that you want to send an invoice for {amount:C} to {bank.GetAccountById(receiverAccountId).GetUserName()}'s account? (yes/no)");
                string? sure = Console.ReadLine();
                if (sure?.ToLower() != "yes") {
                    Console.Clear();
                    Console.WriteLine("Transfer terminated.");
                    return false;
                }
                Console.Write("Confirmation Password: ");
                string? confirmationPassword = Console.ReadLine();

                Console.Clear();

                bool transferSuccess = true;

                User receiverAccount = bank.GetAccountById(receiverAccountId);

                // Validate sender and receiver accounts
                if (user == null || receiverAccount == null) {
                    Console.WriteLine("Invalid account ID.");
                    Console.ReadKey();
                    transferSuccess = false;
                }

                // Validate confirmation password
                if (!user.CheckPassword(confirmationPassword)) {
                    Console.WriteLine("Invalid confirmation password.");
                    Console.ReadKey();
                    transferSuccess = false;
                }

                if (transferSuccess) {
                    Console.WriteLine("Invoice sent successfully.");
                    bank.CreateAndSendInvoice(receiverAccount.GetInvoices().Count + 1, receiverAccountId, title, amount, DateTime.Today.AddDays(due), false); // Create an invoice
                    bank.CreateAndSendInvoice(user.GetInvoices().Count + 1, senderAccountId, title + " [sended]", amount, DateTime.Today.AddDays(due), false); // Create an invoice
                    Console.ReadKey();
                    backToMainMenu = true;
                } else {
                    Console.WriteLine("Sending the invoice failed.");

                    Console.WriteLine("Do you want to try again? (yes/no)");
                    string? tryAgainResponse = Console.ReadLine();
                    if (tryAgainResponse?.ToLower() == "yes") {
                        Console.Clear();
                        return false;
                    } else {
                        backToMainMenu = true;
                    }
                }
            }
            return true;
        }
    }
}