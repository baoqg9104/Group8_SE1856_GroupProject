using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IUserService
    {
        List<User> GetUsers();
        User GetUser(int id);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        void Signup(string fullName, string email, string password);
        User Login(string email);
    }
}
