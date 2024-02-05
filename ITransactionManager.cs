interface ITransactionManager
{
    public void Add(TransactionHistory history);
    public List<TransactionHistory> GetByCustomerId(int customerId);
}