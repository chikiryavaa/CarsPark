using CarPark.ViewModels;
using System.Windows;

namespace CarPark.Views
{
    public partial class VehicleEditor : Window
    {
        public VehicleEditor()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as VehicleViewModel;
            if (vm == null) return;

            var vehicle = vm.Vehicle;

            if (string.IsNullOrWhiteSpace(vehicle.Brand) ||
                string.IsNullOrWhiteSpace(vehicle.Model) ||
                string.IsNullOrWhiteSpace(vehicle.LicensePlate) ||
                vm.SelectedEngine == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля и выберите тип двигателя.",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DialogResult = true;
        }

    }
}
