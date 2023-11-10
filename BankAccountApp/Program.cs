Console.WriteLine("Welcome to the Simple Banking System");

// Request details to create a new account
Console.WriteLine("Enter Account Holder Name:");
string holderName = Console.ReadLine() ?? "Blank Holder";

Console.WriteLine("Choose Account Type: 1 for Savings, 2 for Checking");
AccountType accountType = Console.ReadLine() == "1" ? AccountType.Savings : AccountType.Checking;

// Based on account type, create a specific interest calculator
IInterestCalculator calculator = accountType == AccountType.Savings
    ? new SavingsInterestCalculator()
    : new CheckingInterestCalculator();

// Create a new bank account
BankAccount account = new(Guid.NewGuid().ToString(), holderName, accountType, calculator);

bool exit = false;
while (!exit) {
    Console.WriteLine("\nWhat would you like to do?");
    Console.WriteLine("1. Deposit Money");
    Console.WriteLine("2. Withdraw Money");
    Console.WriteLine("3. Add Interest");
    Console.WriteLine("4. Display Account Details");
    Console.WriteLine("5. Exit");

    string action = Console.ReadLine() ?? "";

    switch (action) {
        case "1":
            Console.WriteLine("Enter amount to deposit:");
            if (decimal.TryParse(Console.ReadLine(), out decimal depositAmount)) {
                account.Deposit(depositAmount);
            } else {
                Console.WriteLine("Invalid amount.");
            }
            break;
        case "2":
            Console.WriteLine("Enter amount to withdraw:");
            if (decimal.TryParse(Console.ReadLine(), out decimal withdrawalAmount)) {
                account.Withdraw(withdrawalAmount);
            } else {
                Console.WriteLine("Invalid amount.");
            }
            break;
        case "3":
            account.AddInterest();
            break;
        case "4": // Display Account
            Console.WriteLine(account);
            break;
        case "5":
            exit = true;
            break;
        default:
            Console.WriteLine("Invalid selection. Please try again.");
            break;
    }
}

Console.WriteLine("Thank you for using the Simple Banking System.");