using System.Collections.Generic;

namespace IsoPlan.Data.Entities
{
    public class JobItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public float Buy { get; set; }
        public float Sell { get; set; }
        public float Profit { get; set; }
        public int JobId { get; set; }
        public Job Job { get; set; }
        public List<Expense> Expenses { get; set; }
    }
}
