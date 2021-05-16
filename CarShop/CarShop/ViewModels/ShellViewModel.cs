using Caliburn.Micro;
using CarShop.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CarShop.ViewModels {
    public partial class ShellViewModel : Conductor<object>, IHandle<Visibility>
    {
        private readonly IEventAggregator eventAggregator;
        private Visibility isBorderVis = Visibility.Collapsed;
        private Car selectedCar;
        private BindableCollection<Car> cars = new BindableCollection<Car>();
        private int sliderValue = 820;
        private int selectedCarIndex;

        public BindableCollection<Car> Cars
        {
            get => cars;
            set
            {
                cars = value;
                NotifyOfPropertyChange(() => Cars);
            }
        }
        public Car SelectedCar
        {
            get => selectedCar;
            set
            {
                selectedCar = value;
                NotifyOfPropertyChange(() => SelectedCar);
            }
        }
        public Visibility IsBorderVis
        {
            get => isBorderVis;
            set
            {
                isBorderVis = value;
                NotifyOfPropertyChange(() => IsBorderVis);
            }
        }
        public int SliderValue
        {
            get => sliderValue;
            set
            {
                sliderValue = value;
                NotifyOfPropertyChange(() => SliderValue);
            }
        }

        public ShellViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);
            ReadCars();
            InitializeSort();
            InitializeTheme();
            InitializeLanguage();
        }


        private void ReadCars()
        {
            try
            {
                Cars = JsonConvert.DeserializeObject<BindableCollection<Car>>(File.ReadAllText("Cars.json"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source);
            }
        }

        public void AddCar()
        {
            IsBorderVis = Visibility.Visible;
            ActivateItem(new AddCarViewModel(eventAggregator));
            eventAggregator.PublishOnUIThread(Cars);
            selectedCarIndex = Cars.Count;
        }

        public bool CanEditCar(Car selectedItem)
        {
            return selectedItem != null;
        }

        public void EditCar(Car selectedItem)
        {
            IsBorderVis = Visibility.Visible;
            ActivateItem(new EditCarViewModel(eventAggregator));
            eventAggregator.PublishOnUIThread(selectedCar);
            eventAggregator.PublishOnUIThread(Cars);
            selectedCarIndex = Cars.IndexOf(selectedCar);
        }

        public bool CanDeleteCar(Car selectedItem)
        {
            return selectedItem != null;
        }

        public void DeleteCar(Car selectedItem)
        {
            selectedCarIndex = Cars.IndexOf(selectedItem);
            Cars.Remove(selectedCar);
            if (selectedCarIndex != Cars.Count)
            {
                SelectedCar = Cars[selectedCarIndex];
            }
            else if (Cars.Count != 0)
            {
                SelectedCar = Cars[selectedCarIndex - 1];
            }
        }       

        public void Handle(Visibility message)
        {
            IsBorderVis = message;
            if (selectedCarIndex <= Cars.Count - 1)
                SelectedCar = Cars[selectedCarIndex];
        }

        public override void CanClose(Action<bool> callback)
        {
            File.WriteAllText("Cars.json", JsonConvert.SerializeObject(cars, Formatting.Indented));
            base.CanClose(callback);
        }
    }
}