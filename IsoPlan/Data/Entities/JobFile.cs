namespace IsoPlan.Data.Entities
{
    public class JobFile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Folder { get; set; }
        public int JobId { get; set; }
        public Job Job { get; set; }
    }
}
