using System.Collections.Generic;

namespace IsoPlan.Data.Entities
{
    public class Role
    {
        public const string Admin = "Admin";
        public const string User = "User";
        public const string Manager = "Manager";

        public static List<string> RoleList = new List<string> { Admin, Manager, User };
    }
}
