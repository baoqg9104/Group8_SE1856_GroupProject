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
    /// Interaction logic for AddMemberSkillWindow.xaml
    /// </summary>
    public partial class AddMemberSkillWindow : Window
    {
        private readonly IMemberSkillService memberSkillService;
        private readonly ISkillService skillService;
        private readonly IUserService userService;

        public AddMemberSkillWindow()
        {
            InitializeComponent();
            memberSkillService = new MemberSkillService();
            skillService = new SkillService();
            userService = new UserService();

            var skills = skillService
                .GetSkills()
                .Select(s => s.SkillName);
            SkillComboBox.ItemsSource = skills;

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var skill = SkillComboBox.SelectedIndex + 1;
                var level = LevelComboBox.SelectedIndex + 1;

                if (skill == 0)
                {
                    throw new Exception("Skill is require");
                }

                if (level == 0)
                {
                    throw new Exception("Level is require");
                }

                var userId = int.Parse(Application.Current.Properties["UserId"] as string);

                var member = userService.GetUser(userId).Member;

                var memberSkill = new MemberSkill()
                {
                    MemberId = member.MemberId,
                    SkillId = skill,
                    Level = level,
                };

                memberSkillService.AddMemberSkill(memberSkill);
                var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                mainWindow?.LoadMemberSkill();
                MessageBox.Show("Add member skill successful!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
