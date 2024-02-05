using System.Globalization;

class CustomerManager : ICustomerManager
{
    List<Customer> Customers = new List<Customer>();
    public Customer AddCustomer(Customer customer)
    {
        if (IsExist(customer.Email))
        {
            Console.WriteLine($"User with email-{customer.Email} already Exists");
        }
        else
        {
            Customers.Add(customer);
            Console.WriteLine($"Registration Successfull!, your account number is {customer.AccountNumber}");
        }
        return customer;
    }

    public void CreditWallet(double amount)
    {

        foreach (var customer in Customers)
        {
            if (customer.Id == Customer.LoggedInUserId)
            {
                customer.WalletBallance += amount;
                Console.WriteLine($"N{amount:n} was added to your wallet, your new wallet balance is N{customer.WalletBallance:n}");
            }
        }
    }

    public Customer Get(int id)
    {
        foreach (var customer in Customers)
        {
            if (customer.Id == id)
            {
                return customer;
            }
        }
        return null;
    }

    public Customer GetByAccountNo(string accountNo)
    {
        foreach (var customer in Customers)
        {
            if (customer.AccountNumber == accountNo)
            {
                return customer;
            }
        }
        return null;
    }

    public bool IsExist(string email)
    {
        foreach (var customer in Customers)
        {
            if (customer.Email == email)
            {
                return true;
            }
        }
        return false;
    }

    public bool Login(string email, string password)
    {
        foreach (var customer in Customers)
        {
            if (customer.Email == email && customer.Password == password)
            {
                Customer.LoggedInUserId = customer.Id;
                return true;
            }
        }
        return false;
    }
    

    public void Transfer(int senderId, int receiverId, double amount)
    {
        foreach (var customer in Customers)
        {
            if (customer.Id == senderId)
            {
                customer.WalletBallance -= amount;
            }
            else if (customer.Id == receiverId)
            {
                customer.WalletBallance += amount;
            }
        }
    }
}

