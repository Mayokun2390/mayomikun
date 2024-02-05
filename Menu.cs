class Menu
{
    ICustomerManager _customerManager = new CustomerManager();
    ITransactionManager _transactionManager = new TransactionManager();
    public void MainMenu()
    {
        bool isContinue = true;
        Console.WriteLine("WELCOME TO MAZSTAR BANK");
        while (isContinue)
        {
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("0. Exit");
            int input = int.Parse(Console.ReadLine());
            switch (input)
            {
                case 1:
                    Login();
                    break;
                case 2:
                    Register();
                    break;
                case 0:
                    isContinue = false;
                    break;
                default:
                    Console.WriteLine("Invalid Input");
                    break;
            }

        }
    }

    public void CustomerMenu()
    {
        bool isContinue = true;
        var loggedInUser = _customerManager.Get(Customer.LoggedInUserId);
        Console.WriteLine($"Good afternoon {loggedInUser.LastName} {loggedInUser.FirstName}");
        while (isContinue)
        {
            Console.WriteLine("1. Credit Wallet");
            Console.WriteLine("2. Check Balance");
            Console.WriteLine("3. Transfer");
            Console.WriteLine("4. View Transaction History");
            Console.WriteLine("5. Account Details");
            Console.WriteLine("0. Logout");

            int input = int.Parse(Console.ReadLine());
            switch (input)
            {
                case 1:
                    CreditWallet();
                    break;
                case 2:
                    CheckBalance();
                    break;
                case 3:
                    Transfer();
                    break;
                case 4:
                    ViewTransactionHistory();
                    break;
                case 5:
                    ViewAccountDetails();
                    break;
                case 0:
                    isContinue = false;
                    break;
                default:
                    Console.WriteLine("Invalid Input");
                    break;
            }

        }
    }

    private void Register()
    {
        Console.Write("Enter your first name: ");
        string firstName = Console.ReadLine();

        Console.Write("Enter your last name: ");
        string lastName = Console.ReadLine();

        Console.Write("Enter your email: ");
        string email = Console.ReadLine();

        Console.Write("Enter your password: ");
        string password = Console.ReadLine();

        Customer customer = new Customer(firstName, lastName, email, password);
        _customerManager.AddCustomer(customer);
    }

    private void Login()
    {
        Console.Write("Enter your email: ");
        string email = Console.ReadLine();

        Console.Write("Enter your password: ");
        string password = Console.ReadLine();

        if (_customerManager.Login(email, password))
        {
            Console.WriteLine("Login Successful!");
            CustomerMenu();
        }
        else
        {
            Console.WriteLine("Invalid Login Details");
        }
    }

    private void CreditWallet()
    {
        Console.Write("Enter amount to credit : ");
        double amount = double.Parse(Console.ReadLine());
        if (amount < 0)
        {
            Console.WriteLine("Amount can't be negative!");
            return;
        }
        _customerManager.CreditWallet(amount);
        TransactionHistory history = new TransactionHistory("Credit", $"You credited your account with {amount:n}", Customer.LoggedInUserId);
        _transactionManager.Add(history);
        Read();
    }

    private void CheckBalance()
    {
        var customer = _customerManager.Get(Customer.LoggedInUserId);
        Console.WriteLine($"Your Wallet Balance is N{customer.WalletBallance:n}");
        Read();
    }

    private void Transfer()
    {
        Console.Write("Enter account no of user to transfer: ");
        string accountNo = Console.ReadLine().ToUpper();
        var customer = _customerManager.GetByAccountNo(accountNo);
        var loggedInCustomer = _customerManager.Get(Customer.LoggedInUserId);
        if (customer == null)
        {
            Console.WriteLine($"No user with account no {accountNo}");
            Read();
            return;
        }
        else
        {
            Console.WriteLine($"Are you sure you want to transfer to {customer.LastName} {customer.FirstName} (y/n)");
            char opt = char.Parse(Console.ReadLine().ToLower());
            if (opt == 'y')
            {
                Console.Write($"Enter amount to transfer to {customer.LastName} {customer.FirstName} : ");
                double amount = double.Parse(Console.ReadLine());
                if (amount > loggedInCustomer.WalletBallance)
                {
                    Console.WriteLine("Insufficient Balance!");
                    Read();
                    return;
                }
                else
                {
                    _customerManager.Transfer(loggedInCustomer.Id, customer.Id, amount);
                    Console.WriteLine($"You've successfully transferred N{amount:n} to {customer.LastName} {customer.FirstName}");
                    TransactionHistory sendHistory = new TransactionHistory("Debit", $"You transferred N{amount:n} to {customer.LastName} {customer.FirstName} ", Customer.LoggedInUserId);
                    TransactionHistory recieveHistory = new TransactionHistory("Credit", $"{loggedInCustomer.LastName} {loggedInCustomer.FirstName} transferred N{amount:n} to  you", customer.Id);
                    _transactionManager.Add(sendHistory);
                    _transactionManager.Add(recieveHistory);
                    Read();
                }
            }
        }
    }

    private void Read()
    {
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    private void ViewAccountDetails()
    {
        var customer = _customerManager.Get(Customer.LoggedInUserId);
        Console.WriteLine($"Account Number: {customer.AccountNumber}");
        Console.WriteLine($"Account Name: {customer.LastName} {customer.FirstName}");
        Read();
    }

    private void ViewTransactionHistory()
    {
        var histories = _transactionManager.GetByCustomerId(Customer.LoggedInUserId);
        if (histories.Count == 0)
        {
            Console.WriteLine("Nothing in Transaction History");
            return;
        }
        foreach (var history in histories)
        {
            Console.ForegroundColor = history.Title.ToLower() == "credit" ? ConsoleColor.Green : ConsoleColor.Red;
            Console.Write($"{history.Title.ToUpper()}");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"\t\t {history.CreatedAt.ToString("MMMM dd, yyyy")}");
            Console.WriteLine($"{history.Description}");
            Console.WriteLine("");
        }
        Read();
    }
}