using CarPark.Data;
using CarPark.Models;
using CarPark.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace CarPark.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ParkContext _ctx = new ParkContext();

        public ObservableCollection<Vehicle> Vehicles { get; }
        public ObservableCollection<MaintenanceRecord> Records { get; }

        public Vehicle SelectedVehicle { get; set; }
        public MaintenanceRecord SelectedRecord { get; set; }

        public RelayCommand AddVehicleCmd { get; }
        public RelayCommand EditVehicleCmd { get; }
        public RelayCommand DeleteVehicleCmd { get; }

        public RelayCommand AddRecordCmd { get; }
        public RelayCommand EditRecordCmd { get; }
        public RelayCommand DeleteRecordCmd { get; }

        public RelayCommand ManageServicesCmd { get; }

        public MainViewModel()
        {
            _ctx.Database.EnsureCreated();
            DbSeeder.Seed(_ctx);

            Vehicles = new ObservableCollection<Vehicle>(_ctx.Vehicles.ToList());
            Records = new ObservableCollection<MaintenanceRecord>(
                          _ctx.Records.ToList());

            AddVehicleCmd = new RelayCommand(_ =>
            {
                var vm = new VehicleViewModel(new Vehicle());
                var dlg = new VehicleEditor { DataContext = vm };
                if (dlg.ShowDialog() == true)
                {
                    _ctx.Vehicles.Add(vm.Vehicle);
                    _ctx.SaveChanges();
                    Vehicles.Add(vm.Vehicle);
                }
            });

            EditVehicleCmd = new RelayCommand(_ =>
            {
                var vm = new VehicleViewModel(SelectedVehicle);
                var dlg = new VehicleEditor { DataContext = vm };

                bool? result = dlg.ShowDialog();
                if (result == true)
                {
                    _ctx.SaveChanges();
                }
                else
                {
                    _ctx.Entry(SelectedVehicle).Reload();
                }

                Vehicles.Clear();
                foreach (var v in _ctx.Vehicles.ToList())
                    Vehicles.Add(v);

            }, _ => SelectedVehicle != null);



            DeleteVehicleCmd = new RelayCommand(_ =>
            {
                if (MessageBox.Show("Удалить?", "Внимание", MessageBoxButton.YesNo)
                    != MessageBoxResult.Yes) return;
                _ctx.Vehicles.Remove(SelectedVehicle);
                _ctx.SaveChanges();
                Vehicles.Remove(SelectedVehicle);
            }, _ => SelectedVehicle != null);

            AddRecordCmd = new RelayCommand(_ =>
            {
                var vm = new MaintenanceRecordViewModel(
                            new MaintenanceRecord(), Vehicles);
                var dlg = new RecordEditor { DataContext = vm };
                if (dlg.ShowDialog() == true)
                {
                    _ctx.Records.Add(vm.Record);
                    _ctx.SaveChanges();
                    Records.Add(vm.Record);
                }
            });

            EditRecordCmd = new RelayCommand(_ =>
            {
                var vm = new MaintenanceRecordViewModel(SelectedRecord, Vehicles);
                var dlg = new RecordEditor { DataContext = vm };

                bool? result = dlg.ShowDialog();
                if (result == true)
                {
                    _ctx.SaveChanges();
                }
                else
                {
                    _ctx.Entry(SelectedRecord).Reload();
                }

                Records.Clear();
                var allRecords = _ctx.Records                                      
                                      .ToList();
                foreach (var rec in allRecords)
                    Records.Add(rec);

            }, _ => SelectedRecord != null);

            DeleteRecordCmd = new RelayCommand(_ =>
            {
                if (MessageBox.Show("Удалить?", "Внимание", MessageBoxButton.YesNo)
                    != MessageBoxResult.Yes) return;
                _ctx.Records.Remove(SelectedRecord);
                _ctx.SaveChanges();
                Records.Remove(SelectedRecord);
            }, _ => SelectedRecord != null);

            ManageServicesCmd = new RelayCommand(_ =>
            {
                var vm = new ServiceTypeViewModel();
                var win = new ServiceTypeManager { DataContext = vm };
                win.ShowDialog();
            });
        }
    }
}
