class Invoice {
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Description { get; set; }
    public int Amount { get; set; }
    public DateTime DueDate { get; set; }

    public Invoice(int id, int userId, string description, int amount, DateTime dueDate) {
        Id = id;
        UserId = userId;
        Description = description;
        Amount = amount;
        DueDate = dueDate;
    }

    public virtual void PrintInvoice() {
        Console.WriteLine($"Invoice ID: {Id}");
        Console.WriteLine($"Account ID: {UserId}");
        Console.WriteLine($"Description: {Description}");
        Console.WriteLine($"Amount: {Amount:C}");
        Console.WriteLine($"Due Date: {DueDate:d}");
    }
}