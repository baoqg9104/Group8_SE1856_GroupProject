using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
         
        public UserService()
        {
            userRepository = new UserRepository();
        }

        public void AddUser(User user)
        {
            userRepository.AddUser(user);
        }

        public void DeleteUser(User user)
        {
            userRepository.DeleteUser(user);
        }

        public User GetUser(int id)
        {
            return userRepository.GetUser(id);
        }

        public List<User> GetUsers()
        {
            return userRepository.GetUsers();
        }

        public User Login(string email)
        {
            return userRepository.Login(email);
        }

        public void Signup(string fullName, string email, string password)
        {
            userRepository.Signup(fullName, email, password); 
        }

        public void UpdateUser(User user)
        {
            userRepository.UpdateUser(user);
        }
    }
}
