public enum AccountType { Checking, Savings }

public interface IInterestCalculator {
    decimal CalculateInterest(decimal balance);
}

public class SavingsInterestCalculator : IInterestCalculator {
    public decimal CalculateInterest(decimal balance) {
        return balance * 0.05m; // 5% interest
    }
}

public class CheckingInterestCalculator : IInterestCalculator {
    public decimal CalculateInterest(decimal balance) {
        return balance * 0.001m;
    }
}

public class BankAccount {
    public string AccountNumber { get; set; }
    public string AccountHolder { get; set; }
    public decimal Balance { get; private set; }
    public AccountType AccountType { get; set; }
    private IInterestCalculator interestCalculator;

    public BankAccount(string accountNumber, string accountHolder, AccountType accountType, IInterestCalculator calculator) {
        AccountNumber = accountNumber;
        AccountHolder = accountHolder;
        AccountType = accountType;
        interestCalculator = calculator;
    }

    public void Deposit(decimal amount) {
        Balance += amount;
        Console.WriteLine($"Deposited {amount:C2}. New Balance: {Balance:C2}");
        var interest = interestCalculator.CalculateInterest(Balance); // potential interest
        LogTransaction("Deposit", amount, interest);
    }

    public void Withdraw(decimal amount) {
        if (amount <= Balance) {
            Balance -= amount;
            Console.WriteLine($"Deposited {amount:C2}. New Balance: {Balance:C2}");
        var interest = interestCalculator.CalculateInterest(Balance);
            LogTransaction("Withdrawal", amount, interest);
        } else {
            Console.WriteLine("Insufficient funds for withdrawal.");
        }
    }

    public decimal AddInterest() {
        var interest = interestCalculator.CalculateInterest(Balance);
        Balance += interest;
        Console.WriteLine($"Interest added: {interest}. New Balance: {Balance}");
        return interest;
    }

    public override string ToString() {
        return $"AccountNumber: {AccountNumber}, AccountHolder: {AccountHolder}, Balance: {Balance}, AccountType: {AccountType}";
    }

    private void LogTransaction(string transactionType, decimal amount, decimal potentialInterest) {
        string timestamp = DateTime.Now.ToString("o"); // ISO 8601 format
        Console.WriteLine($"{timestamp}: {transactionType} of {amount:C2}, Potential Interest: {potentialInterest:C2}.");
    }
}