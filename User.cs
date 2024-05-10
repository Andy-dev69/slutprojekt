class User : Bank
{
    // Properties
    private int UserId { get; set; }
    private string Name { get; set; }
    private bool IsAdmin { get; set; }
    private string Username { get; set; }
    private string Password { get; set; }
    private int Balance { get; set; }
    private List<Invoice> invoices { get; set; }

    // Constructor
    public User(int Id, string BankName, int UserId, string Name, string Username, string Password, bool IsAdmin) : base(Id, BankName) {
        // Initialize properties
        this.UserId = UserId;
        this.Name = Name;
        this.Username = Username;
        this.Password = Password;
        this.IsAdmin = IsAdmin;
        Balance = 1000;
        invoices = new List<Invoice>();
        Console.Clear();
        Console.WriteLine("New bank account created!");
    }

    // Methods to retrieve user information
    public int GetUserId() {
        return UserId;
    }

    public string GetUserName() {
        return Name;
    }

    public string GetUsername() {
        return Username;
    }

    public bool IsUserAdmin() {
        return IsAdmin;
    }

    // Method to retrieve account balance in currency format
    public string GetAccountBalance() {
        return Balance.ToString("C");
    }

    // Method to retrieve account balance as int
    public int GetNormalAccountBalance() {
        return Balance;
    }

    // Method to check if provided password matches the user's password
    public bool CheckPassword(string password) {
        return Password.Equals(password);
    }

    // Method to withdraw specified amount from the account balance
    public void Withdraw(int amount) {
        Balance -= amount;
    }

    // Method to deposit specified amount into the account balance
    public void Deposit(int amount) {
        Balance += amount;
    }

    // Method to validate user credentials
    public bool ValidateCredentials(string username, string password) {
        return Username == username && Password == password;
    }

    // Method to retrieve the list of invoices associated with the user
    public List<Invoice> GetInvoices() {
        return invoices;
    }

    // Method to get the number of invoices associated with the user
    public string GetInvoicesNumber() {
        if (invoices.Count == 0) {
            return "";
        }
        return $"[{invoices.Count}]";
    }

    // Method to print user details
    public void printUserDetails() {
        Console.WriteLine($"Account ID: {UserId} ");
        Console.WriteLine($"Username: {Username}");
        Console.WriteLine($"Name: {Name}  ");
        Console.WriteLine($"------------------------");
        Console.WriteLine($"Administrator: {IsAdmin}\n");
    }

    // Method to remove an invoice from the user's list of invoices
    public void RemoveInvoice(Invoice invoice) {
        invoices.Remove(invoice);
    }

    // Method to receive and add an invoice to the user's list of invoices
    public void ReceiveInvoice(Invoice invoice) {
        invoices.Add(invoice);
        Console.WriteLine("Received Invoice:");
        invoice.PrintInvoice();
    }

    // Method to toggle the user's admin status
    public void SetUserAdmin() {
        IsAdmin = !IsAdmin;
    }
}