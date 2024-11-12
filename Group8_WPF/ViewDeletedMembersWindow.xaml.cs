using BusinessObjects;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for ViewDeletedMembersWindow.xaml
    /// </summary>
    public partial class ViewDeletedMembersWindow : Window
    {
        private readonly IMemberService memberService;

        public ViewDeletedMembersWindow()
        {
            InitializeComponent();
            memberService = new MemberService();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var members = memberService
                    .GetMembers()
                    .Where(x => x.User.Status == UserStatus.Deleted)
                    .ToList();

                if (members != null)
                {
                    DeletedMembersDataGrid.ItemsSource = null;
                    DeletedMembersDataGrid.ItemsSource = members;
                }
            }
            catch
            {
                MessageBox.Show("Error on load deleted members!");
            }
        }
    }
}
