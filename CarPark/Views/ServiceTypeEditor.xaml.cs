using CarPark.ViewModels;
using System.Windows;

namespace CarPark.Views
{
    public partial class ServiceTypeEditor : Window
    {
        public ServiceTypeEditor()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ServiceTypeViewModel; 
            if (vm == null) return;

            if (string.IsNullOrWhiteSpace(vm.Selected.EngineKey) ||
                string.IsNullOrWhiteSpace(vm.Selected.Name))
            {
                MessageBox.Show("Пожалуйста, выберите тип двигателя и введите название услуги.",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DialogResult = true;
        }

    }
}
