using KP.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Логика взаимодействия для AddEditStanki.xaml
    /// </summary>
    /// 

    public partial class AddEditStanki : Page
    {
        private Machine _currentMachine = new Machine();

        public AddEditStanki(Machine selectedMachine)
        {

            InitializeComponent();

            if (selectedMachine != null)
            {
                _currentMachine = selectedMachine;
            }
            else
            {
                _currentMachine.DateOfPurchase = DateTime.Now;
            }


                DataContext = _currentMachine;
            CmbStatus.ItemsSource = StankiEntities.GetContext().Status.ToList();

        }

        private void AddEdit_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            var CurrentStatus = CmbStatus.SelectedItem as Status;
            var Creator = Authorization.Globals.userinfo.FullName;
            _currentMachine.Creator = Creator;

            if (String.IsNullOrEmpty(_currentMachine.Name))
                errors.AppendLine("Укажите название станка");
            if (DateOfPurchase.SelectedDate == null)
                errors.AppendLine("Выберите дату покупки");
            if (CurrentStatus == null)
                errors.AppendLine("Выберите статус");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentMachine.Id >= 0)
                _currentMachine.DateOfPurchase = DateOfPurchase.SelectedDate.Value;
                StankiEntities.GetContext().Machine.AddOrUpdate(_currentMachine);

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
