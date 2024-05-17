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
                case ConsoleKey.D1:
                    exit = true;
                    HandleLogin();
                    break;
                case ConsoleKey.D2:
                    exit = true;
                    HandleCreateAccount();
                    break;
                case ConsoleKey.D3:
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
                case ConsoleKey.D1:
                    HandleTransfer(user);
                    break;
                case ConsoleKey.D2:
                    HandleRequest(user);
                    break;
                case ConsoleKey.D3:
                    CheckInvoices(user);
                    break;
                case ConsoleKey.D4:
                    DisplayAccountDetails(user);
                    break;
                case ConsoleKey.D5:
                    exit = true;
                    DisplayGuestMenu();
                    break;
                case ConsoleKey.D6 when user.IsUserAdmin():
                    DisplayAdminMenu();
                    break;
                case ConsoleKey.D6:
                case ConsoleKey.D7 when user.IsUserAdmin():
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
            Console.WriteLine("                                ");
            Console.WriteLine("  5. Back                       ");
            Console.WriteLine("  6. Exit                       ");
            Console.WriteLine("└──────────────────────────────┘");
            Console.Write("Input: ");
            ConsoleKeyInfo key = Console.ReadKey();
            switch (key.Key) {
                case ConsoleKey.D1:
                    HandleAddMoney();
                    break;
                case ConsoleKey.D2:
                    HandleRemoveMoney();
                    break;
                case ConsoleKey.D3:
                    DisplayInvoiceMenu();
                    break;
                case ConsoleKey.D4:
                    ManageAccounts();
                    break;
                case ConsoleKey.D5:
                    exit = true;
                    ShowMainMenu();
                    break;
                case ConsoleKey.D6:
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
        user.printUserDetails();
        Console.Write("Press any key to go back!");
        Console.ReadKey();
        Console.Clear();
    }

    private void DisplayTerminationMessage() {
        Console.Clear();
        Console.WriteLine("┌────────────────────┐");
        Console.WriteLine("  System Terminated!  ");
        Console.WriteLine("└────────────────────┘");
        Environment.Exit(1);
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

    private void HandleRemoveMoney() {
        Console.Clear();
        Console.WriteLine("Enter transfer details:");
        int accountId = PromptIntInput("Account ID: ");
        while (_bank.GetAccountById(accountId) == null) {
            Console.WriteLine("Invalid account ID.");
            accountId = PromptIntInput("Account ID: ");
        }
        int amount = PromptIntInput("Amount: ");
        User account = _bank.GetAccountById(accountId);
        while (amount <= 0 || amount > account.GetNormalAccountBalance()) {
            Console.WriteLine("Invalid amount.");
            amount = PromptIntInput("Amount: ");
        }
        account.Withdraw(amount);
        Console.WriteLine($"You removed {amount:C} from { account.GetUserName() }'s account!");
        Console.ReadKey();
    }

    private void HandleTransfer(User user) {
        Console.Clear();
        Console.WriteLine("Transfer Money:");
        int recipientId = PromptIntInput("Recipient Account ID: ");
        while (_bank.GetAccountById(recipientId) == null) {
            Console.WriteLine("Invalid account ID.");
            recipientId = PromptIntInput("Recipient Account ID: ");
        }
        int amount = PromptIntInput("Amount: ");
        while (amount <= 0 || amount > user.GetNormalAccountBalance()) {
            Console.WriteLine("Invalid amount.");
            amount = PromptIntInput("Amount: ");
        }
        user.Withdraw(amount);
        _bank.GetAccountById(recipientId).Deposit(amount);
        Console.WriteLine($"You transferred {amount:C} to { _bank.GetAccountById(recipientId).GetUserName() }!");
        Console.ReadKey();
    }

    private void HandleRequest(User user) {
        
    }

    private void CheckInvoices(User user) {}

    private void DisplayInvoiceMenu() {}

    private void ManageAccounts() {
        Console.Write("Do you want to sort the list by admin, ids or name? ");
        string? answer = Console.ReadLine();
        Console.Clear();
        Console.WriteLine("List of User Accounts:");
        // Sorting the accounts by the answer
        if (answer == "admin") {
            var sortedAccounts = _bank.GetUserAccounts().OrderByDescending(u => u.IsUserAdmin());
            foreach (var usr in sortedAccounts) {
                Console.WriteLine($"ID: | {usr.GetUserId()} | Name: {usr.GetUserName()} | Admin: {usr.IsUserAdmin()}");
            }
        } else if (answer == "name") {
            var sortedAccounts = _bank.GetUserAccounts().OrderBy(u => u.GetUserName());
            foreach (var usr in sortedAccounts) {
                Console.WriteLine($"ID: | {usr.GetUserId()} | Name: {usr.GetUserName()} | Admin: {usr.IsUserAdmin()}");
            }
        } else {
            foreach (var usr in _bank.GetUserAccounts()) {
                Console.WriteLine($"ID: | {usr.GetUserId()} | Name: {usr.GetUserName()} | Admin: {usr.IsUserAdmin()}");
            }
        }

        Console.WriteLine("Enter the ID of the user you want to modify: ");
        Console.Write("Input: ");
        int userId;
        while (!int.TryParse(Console.ReadLine(), out userId)) {
            Console.WriteLine("Invalid input. Please enter a valid user ID:");
            Console.Write("Input: ");
        }

        User? selectedUser = _bank.GetUserAccounts().FirstOrDefault(u => u.GetUserId() == userId);
        if (selectedUser == null) {
            Console.WriteLine("User not found.");
            Console.ReadKey();
        }

        Console.WriteLine($"Selected User: ID: {selectedUser?.GetUserId()} Name: {selectedUser?.GetUserName()} Admin: {selectedUser?.IsUserAdmin()}");

        Console.Write("Do you want to change the admin status of this user? (yes/no): ");
        string? changeAdminResponse = Console.ReadLine();
        if (changeAdminResponse?.ToLower() == "yes") {
            Console.Clear();
            selectedUser?.SetUserAdmin();
            Console.WriteLine($"Admin status of user '{selectedUser?.GetUserName()}' changed to {selectedUser?.IsUserAdmin()}.");
        }
    }

    private string PromptNonEmptyInput(string message) {
        string input;

        // Using do-while to show the message from the beggining.
        do {
            Console.Write(message);
            input = Console.ReadLine();
        } while (string.IsNullOrEmpty(input));
        return input;
    }

    private string PromptUniqueUsername() {
        string username;
        do {
            username = PromptNonEmptyInput("Enter a unique username: ");
            if (_bank.GetAllUsers().Any(u => u.GetUsername() == username)) {
                Console.WriteLine("Username already exists. Try another one.");
                username = null;
            }
        } while (username == null);
        return username;
    }

    private int PromptIntInput(string message) {
        int value;
        string input;
        do {
            Console.Write(message);
            input = Console.ReadLine();
        } while (!int.TryParse(input, out value));
        return value;
    }

    private bool PromptYesNo(string message) {
        Console.Write(message);
        string input = Console.ReadLine();
        return input != null && input.ToLower() == "yes";
    }
}