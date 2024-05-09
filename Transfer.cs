class MoneyTransfer {
    private Bank bank;

    public MoneyTransfer(Bank bank) {
        this.bank = bank;
    }

    public bool TransferMoney(int senderAccountId, int receiverAccountId, int amount, string confirmationPassword) {
        // Find sender and receiver accounts
        User senderAccount = bank.GetAccountById(senderAccountId);
        User receiverAccount = bank.GetAccountById(receiverAccountId);

        // Validate sender and receiver accounts
        if (senderAccount == null || receiverAccount == null) {
            Console.WriteLine("Invalid account ID.");
            return false;
        }

        // Validate confirmation password
        if (!senderAccount.CheckPassword(confirmationPassword)) {
            Console.WriteLine("Invalid confirmation password.");
            return false;
        }

        // Check if sender account has sufficient balance
        if (senderAccount.GetNormalAccountBalance() < amount) {
            Console.WriteLine("Insufficient balance.");
            return false;
        }

        // Transfer money
        senderAccount.Withdraw(amount);
        receiverAccount.Deposit(amount);
        Console.WriteLine($"Transfer successful. {amount:C} transferred from account {senderAccountId} to account {receiverAccountId}.");

        return true;
    }
}