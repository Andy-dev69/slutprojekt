// The Bank class is a system with functionality to manage users,
// handle logins, retrieve user accounts, and create/send invoices.
class Bank {
    // Properties
    private int Id { get; set; }
    private string Name { get; set; }
    private List<User> users;
    private User? loggedInUser;

    public Bank(int Id, string Name) {
        this.Id = Id;
        this.Name = Name;
        users = new List<User>(); // Initialize the list of users
    }

    // Method to get the bank name
    public string GetBankName() {
        return Name;
    }

    // Method to get the bank number
    public int GetBankNumber() {
        return Id;
    }

    // Method to add a user to the bank
    public void AddUser(User user) {
        users.Add(user);
    }

    // Method to retrieve all users
    public List<User> GetAllUsers() {
        return users;
    }

    // Method to retrieve the currently logged in user
    public User GetLoggedInUser() {
        if (loggedInUser == null) {
            throw new InvalidOperationException("No user is currently logged in.");
        }
        return loggedInUser;
    }

    // Method to retrieve a user account by ID
    public User GetAccountById(int id) {
        foreach (var user in users) {
            if (user.GetUserId() == id) {
                return user;
            }
        }
        return null;
    }

    // Method to check login credentials and set the logged in user
    public bool CheckLogin(string username, string password) {
        foreach (var user in users) {
            if (user.ValidateCredentials(username, password)) {
                loggedInUser = user;
                return true;
            }
        }
        return false;
    }
    public List<User> GetUserAccounts() {
        return users;
    }

    // Method to create and send an invoice to a user
    public void CreateAndSendInvoice(int id, int userId, int invoiceSenderID, string description, int amount, DateTime dueDate, bool isPaid) {
        Invoice invoice;
        // Create an instance of PaidInvoice or UnpaidInvoice based on isPaid
        if (isPaid) {
            DateTime paymentDate = DateTime.Now;
            invoice = new PaidInvoice(id, userId, invoiceSenderID,  description, amount, dueDate, paymentDate, isPaid);
        } else {
            invoice = new UnpaidInvoice(id, userId, invoiceSenderID, description, amount, dueDate, isPaid);
        }

        // Send invoice to user
        GetAccountById(id).ReceiveInvoice(invoice);
    }
}