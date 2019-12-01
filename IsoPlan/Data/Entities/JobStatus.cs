using System.Collections.Generic;

namespace IsoPlan.Data.Entities
{
    public class JobStatus
    {
        public const string Started = "Started";
        public const string Completed = "Completed";
        public const string Paused = "Paused";

        public static List<string> JobStatusList = new List<string> { Started, Completed, Paused };
    }
}
