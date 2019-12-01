using System.Collections.Generic;

namespace IsoPlan.Data.Entities
{
    public class DevisStatus
    {
        public const string InProgress = "InProgress";
        public const string Valid = "Valid";
        public const string Rejected = "Rejected";

        public static List<string> DevisStatusList = new List<string> { InProgress, Valid, Rejected };
    }
}
