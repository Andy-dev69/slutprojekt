
class UnpaidInvoice : Invoice {
    public UnpaidInvoice(int id, int userId, string description, int amount, DateTime dueDate)
        : base(id, userId, description, amount, dueDate) {
    }
}