
class UnpaidInvoice : Invoice {
    public UnpaidInvoice(int id, int userId, int invoiceSenderID, string description, int amount, DateTime dueDate, bool isPayed)
        : base(id, userId, invoiceSenderID, description, amount, dueDate, isPayed) {
    }
}