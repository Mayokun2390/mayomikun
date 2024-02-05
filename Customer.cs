class Customer : Base
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public double WalletBallance { get; set; }

    public string AccountNumber { get; set; }

    public static int CustomerCount = 0;

    public static int LoggedInUserId { get; set; }

    public Customer(string firstName, string lastName, string email, string password)
    {
        Id = CustomerCount + 1;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        WalletBallance = 0;
        AccountNumber = GenerateAccountNumber();
        CreatedAt = DateTime.Now;
        CustomerCount++;
    }

    private string GenerateAccountNumber()
    {
        Random random = new Random();
        int num = random.Next(10000, 99999);
        return $"{FirstName[0]}{LastName[0]}{num}".ToUpper();
    }
}

