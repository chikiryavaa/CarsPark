using CarPark.Data;
using CarPark.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace CarPark.ViewModels
{
    public class MaintenanceRecordViewModel : BaseViewModel
    {
        private readonly ParkContext _ctx = new ParkContext();

        public MaintenanceRecord Record { get; }
        public ObservableCollection<Vehicle> Vehicles { get; }

        private Vehicle _selectedVehicle;
        public Vehicle SelectedVehicle
        {
            get => _selectedVehicle;
            set
            {
                if (_selectedVehicle == value) return;
                _selectedVehicle = value;
                OnPropertyChanged();

                if (_selectedVehicle != null)
                {
                    Record.VehicleId = _selectedVehicle.Id;
                    SelectedVehicleVm = new VehicleViewModel(_selectedVehicle);
                    RebuildServices();
                }
                else
                {
                    SelectedVehicleVm = null;
                    AvailableServices.Clear();
                }
            }
        }

        private VehicleViewModel _selectedVehicleVm;
        public VehicleViewModel SelectedVehicleVm
        {
            get => _selectedVehicleVm;
            private set
            {
                if (_selectedVehicleVm == value) return;
                _selectedVehicleVm = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> AvailableServices { get; } = new ObservableCollection<string>();

        public DateTime Date
        {
            get => Record.Date;
            set
            {
                if (Record.Date != value)
                {
                    Record.Date = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _serviceName;
        public string ServiceName
        {
            get => _serviceName;
            set
            {
                if (_serviceName != value)
                {
                    _serviceName = value;
                    Record.ServiceName = value;
                    OnPropertyChanged();
                }
            }
        }

        public MaintenanceRecordViewModel(MaintenanceRecord rec, ObservableCollection<Vehicle> vehicles)
        {
            Record = rec;
            Vehicles = vehicles;

            if (rec.Date == null)
                Record.Date = DateTime.Today;

            SelectedVehicle = Vehicles.FirstOrDefault(v => v.Id == rec.VehicleId)
                           ?? Vehicles.FirstOrDefault();

            RebuildServices();

            ServiceName = rec.ServiceName ?? AvailableServices.FirstOrDefault();
        }

        private void RebuildServices()
        {
            AvailableServices.Clear();

            if (SelectedVehicle == null)
                return;

            var services = _ctx.ServiceTypes
                               .Where(s => s.EngineKey == SelectedVehicle.EngineKey)
                               .Select(s => s.Name)
                               .Distinct()
                               .ToList();

            foreach (var s in services)
                AvailableServices.Add(s);

            if (!AvailableServices.Contains(ServiceName))
                ServiceName = AvailableServices.FirstOrDefault();
        }
    }
}
