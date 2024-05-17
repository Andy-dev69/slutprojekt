class Menu
{
    private Bank _bank;

    public Menu(Bank bank) {
        _bank = bank;
    }

    public void DisplayGuestMenu() {
        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("┌────────── MENU ──────────┐");
            Console.WriteLine("  1. Login                  ");
            Console.WriteLine("  2. Create a new account   ");
            Console.WriteLine("                            ");
            Console.WriteLine("  3. Exit                   ");
            Console.WriteLine("└──────────────────────────┘");
            Console.Write("Input: ");

            ConsoleKeyInfo key = Console.ReadKey();
            switch (key.Key) {
                case ConsoleKeyInfo.D1:
                    HandleLogin();
                    break;
                case ConsoleKeyInfo.D2:
                    HandleAccountCreation();
                    break;
                case ConsoleKeyInfo.D3:
                    exit = true;
                    DisplayTerminationMessage();
                    break;
                default:
                    Console.Clear();
                    break;
            }
        }
    }

    public void ShowMainMenu() {
        bool exit = false;
        while (!exit)
        {
            User user = _bank.GetLoggedInUser();
            Console.Clear();
            Console.WriteLine($"Logged in as: {user.GetUsername()}");
            Console.WriteLine($"Balance: {user.GetAccountBalance()} ");
            Console.WriteLine("┌──────────── MENU ────────────┐");
            Console.WriteLine("  1. Transfer Money             ");
            Console.WriteLine("  2. Request Money              ");
            Console.WriteLine($"  3. Check Invoices {user.GetInvoicesNumber()}            ");
            Console.WriteLine("  4. Account Details            ");
            Console.WriteLine("  5. Switch Account             ");
            if (user.IsUserAdmin()) {
                Console.WriteLine("  6. Admin menu                 ");
                Console.WriteLine("                                ");
                Console.WriteLine("  7. Exit                       ");
                Console.WriteLine("└──────────────────────────────┘");
            } else {
                Console.WriteLine("                                ");
                Console.WriteLine("  6. Exit                       ");
                Console.WriteLine("└──────────────────────────────┘");
            }
            Console.Write("Input: ");
            ConsoleKeyInfo key = Console.ReadKey();

            // Switch case som kollar vilken tangent du klickar
            // Special då den kollar även ifall du är admin eller inte.
            switch (key.Key) {
                case ConsoleKeyInfo.D1:
                    HandleTransfer(user);
                    break;
                case ConsoleKeyInfo.D2:
                    HandleRequest(user);
                    break;
                case ConsoleKeyInfo.D3:
                    CheckInvoices(user);
                    break;
                case ConsoleKeyInfo.D4:
                    DisplayAccountDetails(user);
                    break;
                case ConsoleKeyInfo.D5:
                    exit = true;
                    DisplayGuestMenu();
                    break;
                case ConsoleKeyInfo.D6 when user.IsUserAdmin():
                    DisplayAdminMenu();
                    break;
                case ConsoleKeyInfo.D6:
                case ConsoleKeyInfo.D7 when user.IsUserAdmin():
                    exit = true;
                    DisplayTerminationMessage();
                    break;
                default:
                    Console.Clear();
                    break;
            }
        }
    }

    private void DisplayAdminMenu() {
        bool exit = false;
        while (!exit) {
            User user = _bank.GetLoggedInUser();
            Console.Clear();
            Console.WriteLine($"Logged in as: {user.GetUsername()}");
            Console.WriteLine("┌──────────── MENU ────────────┐");
            Console.WriteLine("  1. Add Money To Account       ");
            Console.WriteLine("  2. Remove Money From Account  ");
            Console.WriteLine("  3. Manage Invoices            ");
            Console.WriteLine("  4. Manage Accounts            ");
            Console.WriteLine("  5. Back                       ");
            Console.WriteLine("  6. Exit                       ");
            Console.WriteLine("└──────────────────────────────┘");
            Console.Write("Input: ");
            ConsoleKeyInfo key = Console.ReadKey();
            switch (key.Key) {
                case ConsoleKeyInfo.D1:
                    HandleAddMoney();
                    break;
                case ConsoleKeyInfo.D2:
                    HandleRemoveMoney();
                    break;
                case ConsoleKeyInfo.D3:
                    DisplayInvoiceMenu();
                    break;
                case ConsoleKeyInfo.D4:
                    ManageAccounts();
                    break;
                case ConsoleKeyInfo.D5:
                    exit = true;
                    ShowMainMenu();
                    break;
                case ConsoleKeyInfo.D6:
                    exit = true;
                    DisplayTerminationMessage();
                    break;
                default:
                    Console.Clear();
                    break;
            }
        }
    }

    private void HandleLogin() {
        Console.Clear();
        while (true)
        {
            Console.WriteLine("Login to system.");
            Console.Write("Username: ");
            string? username = Console.ReadLine();
            Console.Write("Password: ");
            string? password = Console.ReadLine();

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password)) {
                if (_bank.CheckLogin(username, password)) {
                    Console.WriteLine("Login successful!");
                    ShowMainMenu();
                    break;
                } else {   
                    Console.Clear();
                    Console.WriteLine("Login failed. Invalid username or password.");
                }
            } else {
                Console.Clear();
                Console.WriteLine("Username or password cannot be empty.");
            }
        }
    }

    private void HandleCreateAccount() {
        Console.Clear();
        string name = PromptNonEmptyInput("Enter your full name: ");
        string username = PromptUniqueUsername();
        string password = PromptNonEmptyInput("Enter a password: ");

        User newUser = new User(1, _bank.GetBankName(), _bank.GetAllUsers().Count + 1, name, username, password, false);
        _bank.AddUser(newUser);

        Console.WriteLine("Account created successfully!");
        if (PromptYesNo("Do you want to login to your new account? (yes/no)")) {
            Console.WriteLine("Logging in...");
            _bank.CheckLogin(username, password);
            Console.WriteLine($"Logged in as: {_bank.GetLoggedInUser().GetUsername()}");
            Console.ReadLine();
            ShowMainMenu();
        } else {
            DisplayGuestMenu();
        }
    }
    
    private void DisplayAccountDetails(User user) {
        Console.Clear();
        user.PrintUserDetails();
        Console.Write("Press any key to go back!");
        Console.ReadKey();
        Console.Clear();
    }

    private void DisplayTerminationMessage() {
        Console.Clear();
        Console.WriteLine("┌────────────────────┐");
        Console.WriteLine("  System Terminated!  ");
        Console.WriteLine("└────────────────────┘");
    }

    private void HandleAddMoney() {
        Console.Clear();
        Console.WriteLine("Enter transfer details:");
        int accountId = PromptIntInput("Account ID: ");
        while (_bank.GetAccountById(accountId) == null) {
            Console.WriteLine("Invalid account ID.");
            accountId = PromptIntInput("Account ID: ");
        }
        int amount = PromptIntInput("Amount: ");
        while (amount <= 0) {
            Console.WriteLine("Amount must be positive.");
            amount = PromptIntInput("Amount: ");
        }
        _bank.GetAccountById(accountId).Deposit(amount);
        Console.WriteLine($"You added {amount:C} to { _bank.GetAccountById(accountId).GetUserName() }'s account!");
        Console.ReadKey();
    }
}