using CarPark.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace CarPark.ViewModels
{
    public class VehicleViewModel : BaseViewModel
    {
        public Vehicle Vehicle { get; }

        public ObservableCollection<EngineType> Engines { get; }

        public VehicleViewModel(Vehicle vehicle)
        {
            Vehicle = vehicle;
            Engines = new ObservableCollection<EngineType>(EngineFactory.AllTypes());

            _selectedEngine = Engines.FirstOrDefault(e => e.Key == Vehicle.EngineKey);
        }

        private EngineType _selectedEngine;
        public EngineType SelectedEngine
        {
            get => _selectedEngine;
            set
            {
                if (_selectedEngine != value)
                {
                    _selectedEngine = value;
                    Vehicle.EngineKey = value.Key;
                    OnPropertyChanged(nameof(SelectedEngine));
   
                }
            }
        }

      
    }
}
