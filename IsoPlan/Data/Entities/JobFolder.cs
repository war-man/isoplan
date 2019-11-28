using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsoPlan.Data.Entities
{
    public class JobFolder
    {
        public const string Devis = "Devis";
        public const string Contract = "Contrat";
        public const string Facturation = "Facturation";
        public const string Ppsps = "Ppsps";
        public const string Plans = "Plans";
        public const string Others = "Autres";

        public static List<string> JobFolderList = new List<string> { Devis, Contract, Facturation, Ppsps, Plans, Others };
    }
}
