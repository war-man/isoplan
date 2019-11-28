using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsoPlan.Data.Entities
{
    public class JobFilePair
    {
        public string Header { get; set; }
        public List<JobFile> Items { get; set; }
    }
}
