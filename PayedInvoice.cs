// Paid invoice (extending the functionality of the base class)
class PaidInvoice : Invoice {
    public DateTime PaymentDate { get; set; }

    public PaidInvoice(int id, int userId, int invoiceSenderID, string description, int amount, DateTime dueDate, DateTime paymentDate, bool isPayed)
        : base(id, userId, invoiceSenderID, description, amount, dueDate, isPayed) {
        // Initialize the paid invoice using the base class constructor
        PaymentDate = paymentDate; // Initialize the payment date
    }

    // Override the PrintInvoice method to include the payment date
    public override void PrintInvoice() {
        base.PrintInvoice();
        Console.WriteLine($"Payment Date: {PaymentDate:d}");
    }
}