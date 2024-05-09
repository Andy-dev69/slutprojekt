class PaidInvoice : Invoice {
    public DateTime PaymentDate { get; set; }

    public PaidInvoice(int id, int userId, string description, int amount, DateTime dueDate, DateTime paymentDate)
        : base(id, userId, description, amount, dueDate) {
        PaymentDate = paymentDate;
    }

    public override void PrintInvoice() {
        base.PrintInvoice();
        Console.WriteLine($"Payment Date: {PaymentDate:d}");
    }
}