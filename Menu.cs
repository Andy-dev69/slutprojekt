// The menu class contains all the menu related functions , including all the necesary methods.
// The methods are named in a easy way so that they are being self explanatory
class Menu
{
    private Bank _bank;

    public Menu(Bank bank) {
        _bank = bank;
    }

    /// <summary>
    /// Displays the guest menu
    /// </summary>
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

            ConsoleKeyInfo key = Console.ReadKey(); // Read a key input from the user

            switch (key.Key) {
                case ConsoleKey.D1:
                    Console.Clear();
                    exit = true;
                    HandleLogin();
                    break;
                case ConsoleKey.D2:
                    Console.Clear();
                    exit = true;
                    HandleCreateAccount();
                    break;
                case ConsoleKey.D3:
                    Console.Clear();
                    exit = true;
                    DisplayTerminationMessage();
                    break;
                default:
                    Console.Clear();
                    break;
            }
        }
    }
    /// <summary>
    /// Displays the main menu
    /// </summary>
    public void ShowMainMenu() {
        bool exit = false;
        while (!exit)
        {
            User user = _bank.GetLoggedInUser();
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
            ConsoleKeyInfo key = Console.ReadKey(); // Read a key input from the user

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
                    Console.Clear();
                    exit = true;
                    DisplayGuestMenu();
                    break;
                case ConsoleKey.D6 when user.IsUserAdmin():
                    Console.Clear();
                    exit = true;
                    DisplayAdminMenu();
                    break;
                case ConsoleKey.D6:
                case ConsoleKey.D7 when user.IsUserAdmin():
                    Console.Clear();
                    exit = true;
                    DisplayTerminationMessage();
                    break;
                default:
                    Console.Clear();
                    break;
            }
        }
    }
    /// <summary>
    /// Displays the admin menu
    /// </summary>
    private void DisplayAdminMenu() {
        bool exit = false;
        while (!exit) {
            User user = _bank.GetLoggedInUser();
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
            ConsoleKeyInfo key = Console.ReadKey(); // Read a key input from the user

            switch (key.Key) {
                case ConsoleKey.D1:
                    HandleAddMoney();
                    break;
                case ConsoleKey.D2:
                    HandleRemoveMoney();
                    break;
                case ConsoleKey.D3:
                    Console.Clear();
                    exit = true;
                    DisplayInvoiceMenu();
                    break;
                case ConsoleKey.D4:
                    ManageAccounts();
                    break;
                case ConsoleKey.D5:
                    Console.Clear();
                    exit = true;
                    ShowMainMenu();
                    break;
                case ConsoleKey.D6:
                    Console.Clear();
                    exit = true;
                    DisplayTerminationMessage();
                    break;
                default:
                    Console.Clear();
                    break;
            }
        }
    }
    /// <summary>
    /// Display the admin invoice menu
    /// </summary>
    private void DisplayInvoiceMenu() {
        Console.Clear();
        bool stop = false;
        while (!stop)
        {
            User user = _bank.GetLoggedInUser();
            Console.WriteLine($"Logged in as: {user.GetUsername()}");
            Console.WriteLine("┌──────────── MENU ────────────┐");
            Console.WriteLine("  1. Create Invoice             ");
            Console.WriteLine("  2. Remove Invoice             ");
            Console.WriteLine("                                ");
            Console.WriteLine("  3. Back                       ");
            Console.WriteLine("└──────────────────────────────┘");
            Console.Write("Input: ");

            ConsoleKeyInfo key = Console.ReadKey(); // Read a key input from the user

            switch (key.Key)
            {
                case ConsoleKey.D1:
                    CreateAdminInvoice();
                    break;
                case ConsoleKey.D2:
                    RemoveInvoice();
                    break;
                case ConsoleKey.D3:
                    stop = true;
                    Console.Clear();
                    ShowMainMenu();
                    break;
                default:
                    Console.Clear();
                    break;
            }
        }
    }
    /// <summary>
    /// Handle the user login feature
    /// </summary>
    private void HandleLogin() {
        Console.Clear();
        while (true)
        {
            Console.WriteLine("Login to system.");
            Console.Write("Username: ");
            string? username = Console.ReadLine();
            Console.Write("Password: ");
            string? password = Console.ReadLine();
            // Checking the input
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password)) {
                if (_bank.CheckLogin(username, password)) {
                    Console.WriteLine("Login successful!");
                    Console.Clear();
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
    /// <summary>
    /// Handle the create account feature
    /// </summary>
    private void HandleCreateAccount() {
        Console.Clear();
        string name = PromptNonEmptyInput("Enter your full name: ");
        string username = PromptUniqueUsername();
        string password = PromptNonEmptyInput("Enter a password: ");

        // Creating a new user at the specific bank.
        User newUser = new User(1, _bank.GetBankName(), _bank.GetAllUsers().Count + 1, name, username, password, false);
        _bank.AddUser(newUser);

        Console.WriteLine("Account created successfully!");
        if (PromptYesNo("Do you want to login to your new account? (yes/no): ")) {
            Console.WriteLine("Logging in...");
            _bank.CheckLogin(username, password);
            Console.WriteLine($"Logged in as: {_bank.GetLoggedInUser().GetUsername()}");
            Console.ReadLine();
            ShowMainMenu();
        } else {
            DisplayGuestMenu();
        }
    }
    /// <summary>
    /// Displays user account details
    /// </summary>
    /// <param name="user"></param>
    private void DisplayAccountDetails(User user) {
        Console.Clear();
        user.printUserDetails();
        Console.Write("Press any key to go back!");
        Console.ReadKey();
        Console.Clear();
    }
    /// <summary>
    /// Display the exit message
    /// </summary>
    private void DisplayTerminationMessage() {
        Console.Clear();
        Console.WriteLine("┌────────────────────┐");
        Console.WriteLine("  System Terminated!  ");
        Console.WriteLine("└────────────────────┘");
        Environment.Exit(1);
    }
    /// <summary>
    /// Handle add money
    /// </summary>
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

        // Adding funds
        _bank.GetAccountById(accountId).Deposit(amount);
        Console.WriteLine($"You added {amount:C} to { _bank.GetAccountById(accountId).GetUserName() }'s account!");
        Console.ReadKey();
        Console.Clear();
    }
    /// <summary>
    /// Handle remove money
    /// </summary>
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

        // Taking funds
        account.Withdraw(amount);
        Console.WriteLine($"You removed {amount:C} from { account.GetUserName() }'s account!");
        Console.ReadKey();
        Console.Clear();
    }
    /// <summary>
    /// Handle transfer money
    /// </summary>
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
        // Making the transaction
        user.Withdraw(amount);
        _bank.GetAccountById(recipientId).Deposit(amount);
        Console.WriteLine($"You transferred {amount:C} to { _bank.GetAccountById(recipientId).GetUserName() }!");
        Console.ReadKey();
        Console.Clear();
    }
    /// <summary>
    /// Handle Request money from user by sending a invoice
    /// </summary>
    /// <param name="user"></param>
    private void HandleRequest(User user) {
        Console.Clear();

        // Asking for user inputs
        Console.WriteLine("Request Money:");
        int recipientId = PromptIntInput("Recipient Account ID: ");
        while (_bank.GetAccountById(recipientId) == null) {
            Console.WriteLine("Invalid account ID.");
            recipientId = PromptIntInput("Recipient Account ID: ");
        }
        Console.Write("Why are you requesting this amount?: ");
        string? title = Console.ReadLine();
        int amount = PromptIntInput("Amount: ");
        int due = PromptIntInput("Due in? (days): ");

        if (!PromptYesNo($"Are you sure that you want to send an invoice for {amount:C} to {_bank.GetAccountById(recipientId).GetUserName()}'s account? (yes/no): ")) {
            Console.Clear();
            Console.WriteLine("Request terminated.");
            return;
        }

        Console.Write("Confirmation Password: ");
        string? confirmationPassword = Console.ReadLine();
        if (user.CheckPassword(confirmationPassword)) {
            int senderAccountId = user.GetUserId();
            User receiverAccount = _bank.GetAccountById(recipientId);

            // Create and send invoice to the receiver
            Invoice receiverInvoice = new UnpaidInvoice(receiverAccount.GetInvoices().Count + 1, recipientId, senderAccountId, title + "", amount, DateTime.Today.AddDays(due), false);
            receiverAccount.ReceiveInvoice(receiverInvoice);

            // Create and send invoice to the sender as confirmation
            Invoice senderInvoice = new UnpaidInvoice(user.GetInvoices().Count + 1, senderAccountId, recipientId, title + " [sent]", amount, DateTime.Today.AddDays(due), false);
            user.ReceiveInvoice(senderInvoice);

            Console.Clear();
            Console.WriteLine("Invoice sent successfully.");
            return;
        } else {
            Console.Clear();
            Console.WriteLine("Incorrect password!");
            return;
        }
    }
    /// <summary>
    /// Checking user invoices
    /// </summary>
    /// <param name="user"></param>
    private void CheckInvoices(User user) {
        Console.Clear();
        if (user.GetInvoices().Any()) {
            // Displays all the user invoices.
            foreach (var invoice in user.GetInvoices()) {
                Console.WriteLine(
                    invoice.IsPayed ? $"[{invoice.Id}] {invoice.Description} - Paid: {invoice.IsPayed}"
                    : $"[{invoice.Id}] {invoice.Description} - Paid: {invoice.IsPayed}\nDue Date: {invoice.DueDate:d}"
                    );
            }
            Console.Write("Enter the ID of the invoice you want to interact with (or '0' to go back): ");
            int invoiceId;
            while (!int.TryParse(Console.ReadLine(), out invoiceId) || invoiceId < 0) {
                Console.WriteLine("Invalid input. Please enter a valid invoice ID (or '0' to go back): ");
            } 
            if (invoiceId != 0) {
                // Gets the selected invoice by the id.
                Invoice selectedInvoice = user.GetInvoices().FirstOrDefault(i => i.Id == invoiceId);
                if (selectedInvoice == null) {
                    Console.Clear();
                    Console.WriteLine("Invoice not found.");
                    return;
                }

                if (selectedInvoice.IsPayed) {
                    Console.Clear();
                    Console.WriteLine("This invoice has already been paid.");
                    return;
                }

                if (selectedInvoice.Description.Contains("[sent]")) {
                    Console.Clear();
                    Console.WriteLine("You can't pay your own invoice.");
                    return;
                }

                Console.WriteLine($"Invoice ID: {selectedInvoice.Id}");
                Console.WriteLine($"From: {_bank.GetAccountById(selectedInvoice.InvoiceSenderID).GetUserName()}");
                Console.WriteLine($"Description: {selectedInvoice.Description}");
                Console.WriteLine($"Amount: {selectedInvoice.Amount:C}");
                Console.WriteLine($"Due Date: {selectedInvoice.DueDate:d}");

                if (PromptYesNo("Do you want to pay this invoice? (yes/no): ")) {
                    // Performs the transfer
                    MoneyTransfer moneyTransfer = new MoneyTransfer(_bank);
                    bool transferSuccess = moneyTransfer.TransferMoney(user.GetUserId(), selectedInvoice.InvoiceSenderID, selectedInvoice.Amount, "DSAUDJSAA");
                    if (transferSuccess) {
                        // setting the invoices as paid.
                        selectedInvoice.SetPaidStatus(true);

                        var sender = _bank.GetAccountById(selectedInvoice.InvoiceSenderID);
                        var relatedInvoice = sender.GetInvoices().FirstOrDefault(i => i.InvoiceSenderID == user.GetUserId() && !i.IsPayed && i.Id == invoiceId);
                        relatedInvoice?.SetPaidStatus(true);

                        Console.WriteLine("Invoice paid successfully.");
                        Console.WriteLine($"New balance: {user.GetAccountBalance():C}");
                    } else {
                        Console.WriteLine("Transfer failed. Insufficient funds.");
                    }
                    Console.ReadKey();
                    Console.Clear();
                }
                return;
            } else {
                Console.Clear();
                return;
            }
        } else {
            Console.Clear();
            Console.WriteLine("You have no invoices!");
            return;
        }
    }
    /// <summary>
    /// Manage account menu for the admin members
    /// </summary>
    private void ManageAccounts() {
        Console.Clear();
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
        int userId = 0;
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

        if (PromptYesNo("Do you want to change the admin status of this user? (yes/no): ")) {
            Console.Clear();
            selectedUser?.SetUserAdmin();
            Console.WriteLine($"Admin status of user '{selectedUser?.GetUserName()}' changed to {selectedUser?.IsUserAdmin()}.");
        } 

        Console.Clear();
    }
    /// <summary>
    /// Create invoice by admin
    /// </summary>
    private void CreateAdminInvoice() {
        Console.Clear();
        int senderAccountId = PromptIntInput("Sender Account ID: ");
        while (_bank.GetAccountById(senderAccountId) == null) {
            Console.WriteLine("Invalid account ID.");
            senderAccountId = PromptIntInput("Sender Account ID: ");
        }

        int receiverAccountId = PromptIntInput("Recipient Account ID: ");
        while (_bank.GetAccountById(receiverAccountId) == null) {
            Console.WriteLine("Invalid account ID.");
            receiverAccountId = PromptIntInput("Recipient Account ID: ");
        }

        Console.Write("Why are you requesting this amount?: ");
        string? title = Console.ReadLine();

        int amount = PromptIntInput("Amount: ");
        int due = PromptIntInput("Due in? (days): ");

        if (!PromptYesNo($"Are you sure that you want to send an invoice for {amount:C} to {_bank.GetAccountById(receiverAccountId).GetUserName()}'s account? (yes/no): ")) {
            Console.Clear();
            Console.WriteLine("Request terminated.");
            return;
        }

        Console.Clear();

        User senderAccount = _bank.GetAccountById(senderAccountId);
        User receiverAccount = _bank.GetAccountById(receiverAccountId);

        // Create and send invoice to the receiver
        Invoice receiverInvoice = new UnpaidInvoice(receiverAccount.GetInvoices().Count + 1, receiverAccountId, senderAccountId, title + "", amount, DateTime.Today.AddDays(due), false);
        receiverAccount.ReceiveInvoice(receiverInvoice);

        // Create and send invoice to the sender as confirmation
        Invoice senderInvoice = new UnpaidInvoice(senderAccount.GetInvoices().Count + 1, senderAccountId, receiverAccountId, title + " [sent]", amount, DateTime.Today.AddDays(due), false);
        senderAccount.ReceiveInvoice(senderInvoice);

        Console.Clear();
        Console.WriteLine("Invoice sent successfully.");
    }
    /// <summary>
    /// Remove invoice from user by choice
    /// </summary>
    private void RemoveInvoice() {
        Console.Clear();
        // Ask user for request details
        int accountId = PromptIntInput("Account ID: ");
        while (_bank.GetAccountById(accountId) == null) {
            Console.WriteLine("Invalid account ID.");
            accountId = PromptIntInput("Account ID: ");
        }

        Console.Clear();

        User user1 = _bank.GetAccountById(accountId);

        if (user1.GetInvoices().Count != 0) {
            foreach (var invoice in user1.GetInvoices()) {
                if (!invoice.IsPayed) {
                    Console.WriteLine($"[{invoice.Id}] {invoice.Description} - Paid: {invoice.IsPayed}\nDue Date: {invoice.DueDate:d}");
                } else {
                    Console.WriteLine($"[{invoice.Id}] {invoice.Description} - Paid: {invoice.IsPayed}");
                }
            }

            Console.Write("Enter the ID of the invoice you want to interact with (or '0' to go back): ");
            int invoiceId = 0;
            while (!int.TryParse(Console.ReadLine(), out invoiceId) || invoiceId < 0) {
                Console.WriteLine("Invalid input. Please enter a valid invoice ID (or '0' to go back): ");
            }
            if (invoiceId == 0) {
                Console.Clear();
            } else {
                Invoice? selectedInvoice = user1.GetInvoices().FirstOrDefault(i => i.Id == invoiceId);
                if (selectedInvoice != null) {
                    Console.WriteLine($"Invoice ID: {selectedInvoice.Id}");
                    Console.WriteLine($"From: {_bank.GetAccountById(selectedInvoice.InvoiceSenderID).GetUserName()}");
                    Console.WriteLine($"Description: {selectedInvoice.Description}");
                    Console.WriteLine($"Amount: {selectedInvoice.Amount:C}");
                    Console.WriteLine($"Due Date: {selectedInvoice.DueDate:d}");

                    if (PromptYesNo("Do you want to delete this invoice? (yes/no): ")) {
                        Console.Clear();
                        Console.WriteLine($"Removeing invoice #{selectedInvoice.Id}...");
                        user1.RemoveInvoice(selectedInvoice);
                        Console.WriteLine("Invoice removed successfully.");
                    } else {
                        Console.Clear();
                    }
                } else {
                    Console.Clear();
                    Console.WriteLine("Invoice not found.");
                }
            }
        } else {
            Console.Clear();
            Console.WriteLine("User has no invoices!");   
        }   
    }
    /// <summary>
    /// Method that checks for non empty inputs
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    private string PromptNonEmptyInput(string message) {
        string input;

        // Using do-while to show the message from the beggining.
        do {
            Console.Write(message);
            input = Console.ReadLine();
        } while (string.IsNullOrEmpty(input));

        return input;
    }
    /// <summary>
    /// Method that checks if a username is already used
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// Method that checks if the input is a valid number
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    private int PromptIntInput(string message) {
        int value;
        string input;
        int time = 0;
        do {
            if (time > 0) {
                Console.Clear();
                Console.WriteLine("Invalid input!");
            }
            time++;
            Console.Write(message);
            input = Console.ReadLine();
        } while (!int.TryParse(input, out value));
        return value;
    }
    /// <summary>
    /// Method that returns if the input is yes or no (true or false)
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    private bool PromptYesNo(string message) {
        Console.Write(message);
        string? input = Console.ReadLine();
        return input != null && input.ToLower() == "yes";
    }
}