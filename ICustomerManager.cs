interface ICustomerManager
{
    public bool IsExist (string email);
    public Customer AddCustomer(Customer customer);
    public bool Login(string email, string password);
    public Customer Get(int id);                                                   
    public Customer GetByAccountNo(string accountNo);
    public void CreditWallet(double amount);
    public void Transfer(int senderId, int receiverId, double amount );
}