using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        User GetUser(int id);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        User Login(string email);
        void Signup(string fullName, string email, string password);
    }
}
