using IsoPlan.Data;
using IsoPlan.Data.Entities;
using IsoPlan.Exceptions;
using IsoPlan.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace IsoPlan.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
        User GetByUsername(string username);
        User Create(User user, string password);
        void Update(User user, string password = null);
        void Delete(int id, string jwt);
    }

    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly ICustomAuthService _customAuthService;

        public UserService(AppDbContext context, ICustomAuthService customAuthService)
        {
            _context = context;
            _customAuthService = customAuthService;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new AppException("Username or password is incorrect.");
            }

            var user = _context.Users.SingleOrDefault(x => x.Username == username);

            // check if username exists
            if (user == null)
            {
                throw new AppException("Username or password is incorrect.");
            }

            // check if password is correct
            if (!Hash.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                throw new AppException("Username or password is incorrect.");
            }

            // authentication successful
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public User Create(User user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new AppException("Password is required");
            }

            if (!ValidateUserData(user))
            {
                throw new AppException("Some required fields are empty");
            }

            if (_context.Users.Any(x => x.Username == user.Username))
            {
                throw new AppException("Le nom d'utilisateur \"" + user.Username + "\" est pris");
            }

            byte[] passwordHash, passwordSalt;
            Hash.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Update(User userParam, string password = null)
        {
            var user = _context.Users.Find(userParam.Id);

            if (user == null)
            {
                throw new AppException("User not found");
            }

            if (user.Username == "super_admin")
            {
                throw new AppException("Impossible de mettre à jour le super admin");
            }

            if (!ValidateUserData(userParam))
            {
                throw new AppException("Some required fields are empty");
            }

            if (userParam.Username != user.Username)
            {
                // username has changed so check if the new username is already taken
                if (_context.Users.Any(x => x.Username == userParam.Username))
                {
                    throw new AppException("Le nom d'utilisateur \"" + userParam.Username + "\" est pris");
                }
            }

            // update user properties
            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
            user.Username = userParam.Username;
            user.Role = userParam.Role;

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                Hash.CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id, string jwt)
        {
            if (id == _customAuthService.GetIdFromToken(jwt))
            {
                throw new AppException("Impossible de supprimer l'administrateur connecté");
            }
            var user = _context.Users.Find(id);

            if (user == null)
            {
                throw new AppException("User not found");
            }

            if (user.Username == "super_admin")
            {
                throw new AppException("Impossible de supprimer le super admin");
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
        }
        private bool ValidateUserData(User user)
        {
            return (
                !string.IsNullOrWhiteSpace(user.FirstName) &&
                !string.IsNullOrWhiteSpace(user.LastName) &&
                !string.IsNullOrWhiteSpace(user.Username) &&
                !string.IsNullOrWhiteSpace(user.Role) &&
                Role.RoleList.Contains(user.Role)
            );
        }

        public User GetByUsername(string username)
        {
            return _context.Users.SingleOrDefault(x => x.Username == username);
        }
    }
}
