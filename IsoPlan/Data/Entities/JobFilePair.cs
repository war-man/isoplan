using System.Collections.Generic;

namespace IsoPlan.Data.Entities
{
    public class JobFilePair
    {
        public string Header { get; set; }
        public List<JobFile> Items { get; set; }
    }
}
