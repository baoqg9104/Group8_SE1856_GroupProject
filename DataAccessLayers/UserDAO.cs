using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class UserDAO
    {
        public static List<User> GetUsers()
        {
            var list = new List<User>();
            try
            {
                using var context = new JobFinderContext();
                list = context.Users.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public static User GetUser(int id)
        {
            using var context = new JobFinderContext();
            return context.Users
                .Include(u => u.Member)
                .FirstOrDefault(u => u.UserId == id);
        }

        public static void AddUser(User user)
        {
            using var context = new JobFinderContext();
            context.Users.Add(user);
            context.SaveChanges();
        }

        public static void UpdateUser(User user)
        {
            using var context = new JobFinderContext();

            var name = user.Name.Trim();
            var email = user.Email.Trim();
            var password = user.Password;

            if (context.Users.Any(u => u.UserId != user.UserId && u.Email == email))
            {
                throw new Exception("Email already exists.");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email is required.");
            }

            if (!IsValidEmail(email))
            {
                throw new ArgumentException("Invalid email format.");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name is required.");
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(name, @"^[a-zA-Z\s]+$") ||
        System.Text.RegularExpressions.Regex.IsMatch(name, @"\s{2,}"))
            {
                throw new Exception("Name can only contain letters and spaces, with no more than one space between words.");
            }

            if (password.Contains(" "))
            {
                throw new Exception("Password cannot contain spaces.");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("Password cannot be empty.");
            }

            context.Entry<User>(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

        public static void DeleteUser(User user)
        {
            using var context = new JobFinderContext();
            context.Users.Remove(user);
            context.SaveChanges();
        }

        public static void Signup(string fullName, string email, string password)
        {
            fullName = fullName.Trim();
            email = email.Trim();
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentException("Full name is required.");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email is required.");
            }

            if (!IsValidEmail(email))
            {
                throw new ArgumentException("Invalid email format.");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Password is required.");
            }

            using var context = new JobFinderContext();

            if (context.Users.Any(u => u.Email == email))
            {
                throw new ArgumentException("Email already exists.");
            }

            var user = new User()
            {
                Name = fullName,
                Email = email,
                Password = password,
                RoleId = 2
            };

            AddUser(user);

            var member = new Member()
            {
                UserId = user.UserId, 
            };

            MemberDAO.AddMember(member);
        }

        private static bool IsValidEmail(string email)
        {
            const string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        public static User Login(string email)
        {
            using var context = new JobFinderContext();

            var user = context.Users.FirstOrDefault(u => u.Email == email);

            return user;
        }
    }
}
