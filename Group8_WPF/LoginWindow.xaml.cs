using BusinessObjects;
using DataAccessLayers;
using Microsoft.Extensions.Configuration;
using Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Group8_WPF
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly IUserService userService;

        public LoginWindow()
        {
            InitializeComponent();
            userService = new UserService();   
        }

        private void HandleLogin(object sender, RoutedEventArgs e)
        {
            string email = Email.Text;
            string password = Password.Password;

            var user = userService.Login(email);

            if (user != null && user.Password == password && user.Status != UserStatus.Deleted)
            {

                if (user.Status == UserStatus.Banned)
                {
                    MessageBox.Show("Your account has been banned!");
                    return;
                }

                Application.Current.Properties["UserId"] = user.UserId.ToString();

                MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

                if (user.RoleId == 1)
                {
                    mainWindow.SetUser("Admin", user.Name);
                }
                else if (user.RoleId == 2)
                {
                    mainWindow.SetUser("Member", user.Name);
                }
                else if (user.RoleId == 3)
                {
                    mainWindow.SetUser("Company", user.Name);
                }

                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid email or password!");
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                HandleLogin(sender, e);
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
