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

        public void EditCar()
        {
            if (SelectedCar == null) return;
            IsBorderVis = Visibility.Visible;
            ActivateItem(new EditCarViewModel(eventAggregator));
            eventAggregator.PublishOnUIThread(SelectedCar);
            eventAggregator.PublishOnUIThread(Cars);
            selectedCarIndex = Cars.IndexOf(SelectedCar);
        }

        public void DeleteCar()
        {
            if (SelectedCar == null || Cars.Count == 0) return;
            selectedCarIndex = Cars.IndexOf(SelectedCar);
            Cars.Remove(SelectedCar);
            if (Cars.Count == 0)
            {
                SelectedCar = null;
            }
            else if(selectedCarIndex == 0)
            {
                SelectedCar = Cars[selectedCarIndex];
            }
            else if (Cars[selectedCarIndex - 1] == Cars[Cars.Count - 1])
            {
                SelectedCar = Cars[selectedCarIndex - 1];
            }
            else
            {
                SelectedCar = Cars[selectedCarIndex];
            }
        }       

        public void Handle(Visibility message)
        {
            IsBorderVis = message;
            if (selectedCarIndex <= Cars.Count - 1)
                SelectedCar = Cars[selectedCarIndex];
        }        
    }
}