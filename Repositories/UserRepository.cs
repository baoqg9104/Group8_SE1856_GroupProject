using BusinessObjects;
using DataAccessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        public void AddUser(User user)
        {
            UserDAO.AddUser(user);
        }

        public void DeleteUser(User user)
        {
            UserDAO.DeleteUser(user);
        }

        public User GetUser(int id)
        {
            return UserDAO.GetUser(id);
        }

        public List<User> GetUsers()
        {
            return UserDAO.GetUsers();
        }

        public User Login(string email)
        {
            return UserDAO.Login(email);
        }

        public void Signup(string fullName, string email, string password)
        {
            UserDAO.Signup(fullName, email, password);
        }

        public void UpdateUser(User user)
        {
            UserDAO.UpdateUser(user);
        }
    }
}
