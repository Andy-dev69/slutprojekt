class PaidInvoice : Invoice {
    public DateTime PaymentDate { get; set; }

    public PaidInvoice(int id, int userId, int invoiceSenderID, string description, int amount, DateTime dueDate, DateTime paymentDate, bool isPayed)
        : base(id, userId, invoiceSenderID, description, amount, dueDate, isPayed) {
        PaymentDate = paymentDate;
    }

    public override void PrintInvoice() {
        base.PrintInvoice();
        Console.WriteLine($"Payment Date: {PaymentDate:d}");
    }
}