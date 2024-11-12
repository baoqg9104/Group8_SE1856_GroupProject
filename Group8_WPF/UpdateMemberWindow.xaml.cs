using BusinessObjects;
using DataAccessLayers.DTOs.Member;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Group8_WPF
{
    /// <summary>
    /// Interaction logic for UpdateMemberWindow.xaml
    /// </summary>
    public partial class UpdateMemberWindow : Window
    {
        private readonly IUserService userService;
        private readonly IMemberService memberService;

        private readonly Member _member;

        public UpdateMemberWindow(Member member)
        {
            InitializeComponent();
            userService = new UserService();
            memberService = new MemberService();

            _member = member;

            MemberNameTextBox.Text = member.User.Name;
            EmailTextBox.Text = member.User.Email;
            PasswordTextBox.Text = member.User.Password;
            DateOfBirthDatePicker.SelectedDate = member.DateOfBirth;
            PhoneTextBox.Text = member.Phone;
            GenderComboBox.SelectedIndex = -1;

            if (member.Gender == 0)
            {
                GenderComboBox.SelectedIndex = 1;
            }
            else if (member.Gender == 1)
            {
                GenderComboBox.SelectedIndex = 0;
            }
        }

        private void SaveMember(object sender, RoutedEventArgs e)
        {
            string memberName = MemberNameTextBox.Text.Trim();
            string email = EmailTextBox.Text.Trim();
            string password = PasswordTextBox.Text;
            DateTime? dateOfBirth = DateOfBirthDatePicker.SelectedDate;
            string? phone = PhoneTextBox.Text;
            int? gender = GenderComboBox.SelectedIndex == -1 ? null : GenderComboBox.SelectedIndex;



            try
            {
                if (string.IsNullOrWhiteSpace(memberName))
                {
                    throw new ArgumentException("Name is required.");
                }

                if (!System.Text.RegularExpressions.Regex.IsMatch(memberName, @"^[a-zA-Z\s]+$") ||
            System.Text.RegularExpressions.Regex.IsMatch(memberName, @"\s{2,}"))
                {
                    throw new Exception("Name can only contain letters and spaces, with no more than one space between words.");
                }

                if (userService.GetUsers().Any(u => u.Email == email && u.UserId != _member.UserId))
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

                if (password.Contains(" "))
                {
                    throw new Exception("Password cannot contain spaces.");
                }

                if (string.IsNullOrEmpty(password))
                {
                    throw new Exception("Password cannot be empty.");
                }

                var updateMember = new UpdateMemberDTO()
                {
                    UserId = _member.UserId,
                    Name = memberName,
                    Email = email,
                    Password = password,
                    DateOfBirth = dateOfBirth,
                    Phone = phone,
                    Gender = gender,
                };

                memberService.UpdateMember(updateMember);
                MessageBox.Show("Update member successful");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private static bool IsValidEmail(string email)
        {
            const string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
