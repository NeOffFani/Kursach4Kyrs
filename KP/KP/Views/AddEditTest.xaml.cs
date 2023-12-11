using KP.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KP.Views
{
    /// <summary>
    /// Логика взаимодействия для AddEditTest.xaml
    /// </summary>
    public partial class AddEditTest : Page
    {

        private TestRequests _currentTest = new TestRequests();

        public AddEditTest(TestRequests selectedTest)
        {
            InitializeComponent();


            if (selectedTest != null)
            {
                _currentTest = selectedTest;
            }
            else
            {
                _currentTest.DateOfTesting = DateTime.Now;
            }

            DataContext = _currentTest;
            CmbMachine.ItemsSource = StankiEntities.GetContext().Machine.ToList();
            CmbUsers.ItemsSource = StankiEntities.GetContext().UsersTable.ToList();
        }

        private void AddEdit_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            var CurrentMachine = CmbMachine.SelectedItem as Machine;
            var CurrentUser = CmbUsers.SelectedItem as UsersTable;
            _currentTest.DateOfCreate = DateTime.Now;
            var Creator = Authorization.Globals.userinfo.FullName;
            _currentTest.Creator = Creator;



            if (CurrentMachine == null)
                errors.AppendLine("Выберите станок");
            if (DateOfTesting.SelectedDate == null)
                errors.AppendLine("Выберите дату");
            if (CurrentUser == null)
                errors.AppendLine("Выберите инспектора");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentTest.Id >= 0)
                StankiEntities.GetContext().TestRequests.AddOrUpdate(_currentTest);

            try
            {
                StankiEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");
                Manager.MyFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }
    }
}
