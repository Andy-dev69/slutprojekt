class Bank {
    private int Id { get; set; }
    private string Name { get; set; }
    private List<User> users;
    private User? loggedInUser;

    public Bank(int Id, string Name) {
        this.Id = Id;
        this.Name = Name;
        this.users = new List<User>(); // Initialize the list of users
    }

    public string GetBankName() {
        return Name;
    }

    public int GetBankNumber() {
        return Id;
    }

    public void AddUser(User user) {
        users.Add(user);
    }

    public List<User> GetAllUsers() {
        return users;
    }

    public User GetLoggedInUser() {
        return loggedInUser;
    }

    public User GetAccountById(int id) {
        foreach (var user in users) {
            if (user.GetUserId() == id) {
                return user;
            }
        }
        return null;
    }

    public bool CheckLogin(string username, string password) {
        foreach (var user in users) {
            if (user.ValidateCredentials(username, password)) {
                loggedInUser = user;
                return true;
            }
        }
        return false;
    }

    public void CreateAndSendInvoice(int id, int userId, string description, int amount, DateTime dueDate, bool isPaid) {
        Invoice invoice;
        if (isPaid) {
            DateTime paymentDate = DateTime.Now;
            invoice = new PaidInvoice(id, userId, description, amount, dueDate, paymentDate);
        } else {
            invoice = new UnpaidInvoice(id, userId, description, amount, dueDate);
        }

        // Send invoice to user
        GetAccountById(id).ReceiveInvoice(invoice);
    }
}