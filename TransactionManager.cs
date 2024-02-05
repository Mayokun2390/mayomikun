class TransactionManager : ITransactionManager
{
    List<TransactionHistory> Histories = new List<TransactionHistory>();
    public void Add(TransactionHistory history)
    {
       Histories.Add(history);
    }

    public List<TransactionHistory> GetByCustomerId(int customerId)
    {
        List<TransactionHistory> customerHistory =  new List<TransactionHistory>();
        foreach (var history in Histories)
        {
            if(history.CustomerId == customerId){
                customerHistory.Add(history);
            }
        }
       return customerHistory;
    }
}