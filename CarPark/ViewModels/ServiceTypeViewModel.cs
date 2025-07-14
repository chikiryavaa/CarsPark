using CarPark.Data;
using CarPark.Models;
using CarPark.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;

namespace CarPark.ViewModels
{
    public class ServiceTypeViewModel : BaseViewModel
    {
        private readonly ParkContext _ctx = new ParkContext();

        public ObservableCollection<ServiceType> Items { get; }

        private ServiceType _selected;
        public ServiceType Selected
        {
            get => _selected;
            set
            {
                if (_selected == value) return;
                _selected = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> EngineKeys { get; }

        public RelayCommand AddCmd { get; }
        public RelayCommand EditCmd { get; }
        public RelayCommand DeleteCmd { get; }

        public ServiceTypeViewModel()
        {
            Items = new ObservableCollection<ServiceType>(_ctx.ServiceTypes.ToList());
            EngineKeys = new ObservableCollection<string>(
                EngineFactory.AllTypes().Select(e => e.Key));

            AddCmd = new RelayCommand(_ =>
            {
                Selected = new ServiceType();
                var dlg = new ServiceTypeEditor { DataContext = this };
                if (dlg.ShowDialog() == true)
                {
                    _ctx.ServiceTypes.Add(Selected);
                    _ctx.SaveChanges();
                    Items.Add(Selected);
                }
                Selected = null;
            });

            EditCmd = new RelayCommand(_ =>
            {
                var dlg = new ServiceTypeEditor { DataContext = this };
                if (dlg.ShowDialog() == true)
                {
                    _ctx.ServiceTypes.Update(Selected);
                    _ctx.SaveChanges();
                    CollectionViewSource.GetDefaultView(Items).Refresh();
                }
            }, _ => Selected != null);

            DeleteCmd = new RelayCommand(_ =>
            {
                _ctx.ServiceTypes.Remove(Selected);
                _ctx.SaveChanges();
                Items.Remove(Selected);
                Selected = null;
            }, _ => Selected != null);
        }
    }
}
