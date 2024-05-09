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

    public bool CheckLogin(string username, string password) {
        foreach (var user in users) {
            if (user.ValidateCredentials(username, password)) {
                loggedInUser = user;
                return true;
            }
        }
        return false;
    }
}