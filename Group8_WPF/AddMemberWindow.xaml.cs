using BusinessObjects;
using DataAccessLayers;
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
    /// Interaction logic for AddMemberWindow.xaml
    /// </summary>
    public partial class AddMemberWindow : Window
    {
        private readonly IUserService userService;
        private readonly IMemberService memberService;

        public AddMemberWindow()
        {
            InitializeComponent();
            userService = new UserService();
            memberService = new MemberService();
        }

        private void AddMember(object sender, RoutedEventArgs e)
        {
            string memberName = MemberNameTextBox.Text.Trim();
            string email = EmailTextBox.Text.Trim();
            string password = PasswordTextBox.Text;
            DateTime? dateOfBirth = DateOfBirthDatePicker.SelectedDate;
            string? phone = PhoneTextBox.Text;
            int? gender = null;

            if (GenderComboBox.SelectedIndex == 0)
            {
                gender = 1;
            }
            else if (GenderComboBox.SelectedIndex == 1)
            {
                gender = 0;
            }

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

                if (userService.GetUsers().Any(u => u.Email == email))
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


                var user = new User()
                {
                    Name = memberName,
                    Email = email,
                    Password = password,
                    RoleId = 2,
                    Status = UserStatus.Active,
                };

                userService.AddUser(user);

                var member = new Member()
                {
                    UserId = user.UserId,
                    DateOfBirth = dateOfBirth,
                    Phone = phone,
                    Gender = gender,
                };

                memberService.AddMember(member);
                MessageBox.Show("Add new member successful");
                ClearForm();
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

        private void ClearForm()
        {
            MemberNameTextBox.Text = string.Empty;
            EmailTextBox.Text = string.Empty;
            PasswordTextBox.Text = string.Empty;
            DateOfBirthDatePicker.SelectedDate = null;
            PhoneTextBox.Text = string.Empty;
            GenderComboBox.SelectedIndex = -1;
        }
    }
}
