using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsoPlan.Data.Entities
{
    public class JobStatus
    {
        public const string Started = "Started";
        public const string Completed = "Completed";

        public static List<string> JobStatusList = new List<string> { Started, Completed };
    }
}
