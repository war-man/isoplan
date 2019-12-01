namespace IsoPlan.Data.Entities
{
    public class EmployeeFile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
