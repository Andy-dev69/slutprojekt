class User : Bank
{
    private int UserId { get; set; }
    private string Name { get; set; }
    private bool IsAdmin { get; set; }
    private string Username { get; set; }
    private string Password { get; set; }
    private int Balance { get; set; }
    private List<Invoice> invoices { get; set; }
    public User(int Id, string BankName, int UserId, string Name, string Username, string Password, bool IsAdmin) : base(Id, BankName) {
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

    public string GetAccountBalance() {
        return Balance.ToString("C"); // Display & format balance in Euros
    }

    public int GetNormalAccountBalance() {
        return Balance;
    }

    public bool CheckPassword(string password) {
        return Password.Equals(password);
    }

    public void Withdraw(int amount) {
        Balance = Balance - amount;
    }

    public void Deposit(int amount) {
        Balance = Balance + amount;
    }

    public bool ValidateCredentials(string username, string password) {
        return Username == username && Password == password;
    }

    public List<Invoice> GetInvoices() {
        return invoices;
    }

    public string GetInvoicesNumber() {
        if (invoices.Count == 0) {
            return "";
        }
        return $"[{invoices.Count}]";
    }

    public void ReceiveInvoice(Invoice invoice) {
        invoices.Add(invoice);
        Console.WriteLine("Received Invoice:");
        invoice.PrintInvoice();
    }
}