using KP.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KP.Views
{
    /// <summary>
    /// Логика взаимодействия для AddEditRepair.xaml
    /// </summary>
    public partial class AddEditRepair : Page
    {

        private RepairRequest _currentRepair = new RepairRequest();

        public AddEditRepair(RepairRequest selectedRepair)
        {
            InitializeComponent();

            if (selectedRepair != null)
            {
                _currentRepair = selectedRepair;
            }
            else
            {
                _currentRepair.DateOfRepairing = DateTime.Now;
            }

            DataContext = _currentRepair;

            CmbMachine.ItemsSource = StankiEntities.GetContext().Machine.ToList();
            CmbUsers.ItemsSource = StankiEntities.GetContext().UsersTable.ToList();
        }

        private void AddEdit_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            var CurrentMachine = CmbMachine.SelectedItem as Machine;
            var CurrentUser = CmbUsers.SelectedItem as UsersTable;
            _currentRepair.DateOfCreate = DateTime.Now;
            var Creator = Authorization.Globals.userinfo.FullName;
            _currentRepair.Creator = Creator;

            if (CurrentMachine == null)
                errors.AppendLine("Выберите станок");
            if (DateOfRepairing.SelectedDate == null)
                errors.AppendLine("Выберите дату"); 
            if (CurrentUser == null)
                errors.AppendLine("Выберите инспектора");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentRepair.Id >= 0)
                _currentRepair.DateOfRepairing = DateOfRepairing.SelectedDate.Value;
                StankiEntities.GetContext().RepairRequest.AddOrUpdate(_currentRepair);

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
