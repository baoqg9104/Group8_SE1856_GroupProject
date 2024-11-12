using BusinessObjects;
using DataAccessLayers.DTOs.Member;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class MemberDAO
    {
        public static List<Member> GetMembers()
        {
            var list = new List<Member>();
            try
            {
                using var context = new JobFinderContext();
                list = context.Members
                    .Include(x => x.User)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public static Member GetMemberById(int id)
        {
            using var context = new JobFinderContext();
            return context.Members.FirstOrDefault(m => m.MemberId == id);
        }

        public static void AddMember(Member member)
        {
            using var context = new JobFinderContext();
            context.Members.Add(member);
            context.SaveChanges();
        }

        //public static void UpdateMember(Member member)
        //{
        //    using var context = new JobFinderContext();
        //    context.Entry<Member>(member).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //    context.SaveChanges();
        //}

        public static void UpdateMember(UpdateMemberDTO updateMember)
        {
            using var context = new JobFinderContext();
            var member = context.Members.FirstOrDefault(m => m.UserId == updateMember.UserId);

            if (member == null)
            {
                return;
            }

            var user = context.Users.FirstOrDefault(u => u.UserId == member.UserId);

            var name = updateMember.Name.Trim();
            var email = updateMember.Email.Trim();
            var phone = updateMember.Phone.Trim() == "" ? null : updateMember.Phone.Trim();

            var existingUser = context.Users
        .FirstOrDefault(u => u.Email == email && u.UserId != user.UserId);

            if (existingUser != null)
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

            if (phone != null && !System.Text.RegularExpressions.Regex.IsMatch(phone, @"^(0|\+84|0084)(1[0-9]|2[0-9]|3[0-9]|4[0-9]|5[0-9]|6[0-9]|7[0-9]|8[0-9]|9[0-9])\d{7}$"))
            {
                throw new Exception("Phone number is not valid.");
            }

            if (updateMember.DateOfBirth != null)
            {
                if (updateMember.DateOfBirth > DateTime.Now)
                {
                    throw new Exception("Date of Birth cannot be a future date.");
                }

                var age = DateTime.Now.Year - updateMember.DateOfBirth.Value.Year;
                if (updateMember.DateOfBirth > DateTime.Now.AddYears(-age)) age--;

                if (age < 15)
                {
                    throw new Exception("You must be at least 15 years old.");
                }
            }

            if (updateMember.Password.Contains(" "))
            {
                throw new Exception("Password cannot contain spaces.");
            }

            if (string.IsNullOrWhiteSpace(updateMember.Password))
            {
                throw new Exception("Password cannot be empty.");
            }

            member.DateOfBirth = updateMember.DateOfBirth;
            member.Phone = phone;

            if (updateMember.Gender != null)
            {
                member.Gender = updateMember.Gender == 0 ? 1 : 0;
            }

            user.Name = name;
            user.Email = email;
            user.Password = updateMember.Password;

            context.SaveChanges();
        }

        public static void DeleteMember(Member member)
        {
            using var context = new JobFinderContext();
            context.Members.Remove(member);
            context.SaveChanges();
        }

        private static bool IsValidEmail(string email)
        {
            const string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
