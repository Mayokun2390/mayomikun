class TransactionHistory : Base
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int CustomerId { get; set; }

    public static int HistoryCount = 0;
    
    public TransactionHistory(string title, string description, int customerId)
    {
        Id = HistoryCount + 1;
        Title = title;
        Description = description;
        CustomerId = customerId;
        CreatedAt = DateTime.Now;
        HistoryCount++;
    }
}