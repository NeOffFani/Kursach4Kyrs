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
    /// Логика взаимодействия для CloseRequestR.xaml
    /// </summary>
    public partial class CloseRequestR : Page
    {
        private RepairRequest _currentRepair = new RepairRequest();
        public CloseRequestR(RepairRequest selectedRepair)
        {
            InitializeComponent();

            if (selectedRepair != null)
            {
                _currentRepair = selectedRepair;
            }

            DataContext = _currentRepair;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _currentRepair.DateOfClose = DateTime.Now;

            StringBuilder errors = new StringBuilder();

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentRepair.Id >= 0)
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
