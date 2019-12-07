using IsoPlan.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace IsoPlan.Services
{
    public interface ICustomAuthService
    {
        bool CheckToken(string jwt);
        int GetIdFromToken(string jwt);
    }
    public class CustomAuthService : ICustomAuthService
    {
        private readonly AppDbContext _context;

        public CustomAuthService(AppDbContext context)
        {
            _context = context;
        }
        public bool CheckToken(string jwt)
        {
            if (jwt == null)
            {
                return false;
            }
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);
            int id = int.Parse(token.Claims.FirstOrDefault(x => x.Type == "unique_name").Value);
            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            return user != null;
        }
        public int GetIdFromToken(string jwt)
        {
            if (jwt == null)
            {
                return 0;
            }
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);
            return int.Parse(token.Claims.FirstOrDefault(x => x.Type == "unique_name").Value);
        }
    }
}
