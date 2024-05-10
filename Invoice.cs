// Basic invoice
class Invoice {
    // Properties
    public int Id { get; set; }
    public int UserId { get; set; }
    public int InvoiceSenderID { get; set; }
    public string Description { get; set; }
    public int Amount { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsPayed { get; set; }

    public Invoice(int id, int userId, int invoiceSenderID, string description, int amount, DateTime dueDate, bool isPayed) {
        // Initialize properties
        Id = id;
        UserId = userId;
        InvoiceSenderID = invoiceSenderID;
        Description = description;
        Amount = amount;
        DueDate = dueDate;
        IsPayed = isPayed;
    }

    // Method to set the paid status of the invoice
    public void SetPaidStatus(bool isPayed) {
        IsPayed = isPayed;
    }

    // Virtual method to print the details of the invoice
    // Using the virtual attribute so that it can be overided and display diffrent information in case the invoice is not payed!
    public virtual void PrintInvoice() {
        Console.WriteLine($"Invoice ID: {Id}");
        Console.WriteLine($"Account ID: {UserId}");
        Console.WriteLine($"Description: {Description}");
        Console.WriteLine($"Amount: {Amount:C}");
        Console.WriteLine($"Due Date: {DueDate:d}");
    }
}