using System.Collections.Generic;

namespace IsoPlan.Data.Entities
{
    public class ContractType
    {
        public const string Definite = "Definite";
        public const string Indefinite = "Indefinite";

        public static List<string> ContractTypeList = new List<string> { Definite, Indefinite };
    }
}
