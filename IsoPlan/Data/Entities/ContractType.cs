using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsoPlan.Data.Entities
{
    public class ContractType
    {
        public const string Definite = "Definite";
        public const string Indefinite = "Indefinite";

        public static List<string> ContractTypeList = new List<string> { Definite, Indefinite };
    }
}
