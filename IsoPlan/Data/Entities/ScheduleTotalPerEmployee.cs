namespace IsoPlan.Data.Entities
{
    public class ScheduleTotalPerEmployee
    {
        public Employee Employee { get; set; }
        public int TotalDays { get; set; }
        public double Salary { get; set; }
    }
}
