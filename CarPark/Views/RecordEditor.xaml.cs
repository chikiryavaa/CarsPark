using CarPark.ViewModels;
using System.Windows;

namespace CarPark.Views
{
    public partial class RecordEditor : Window
    {
        public RecordEditor()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as MaintenanceRecordViewModel;
            if (vm == null) return;

            if (vm.SelectedVehicle == null ||
                string.IsNullOrWhiteSpace(vm.ServiceName))
            {
                MessageBox.Show("Пожалуйста, выберите автомобиль и вид работы.",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DialogResult = true;
        }

    }
}
