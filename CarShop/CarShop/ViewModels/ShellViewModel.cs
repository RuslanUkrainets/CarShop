using Caliburn.Micro;
using CarShop.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace CarShop.ViewModels {
    public class ShellViewModel : Conductor<object>, IHandle<Visibility>
    {
        private readonly IEventAggregator eventAggregator;
        private Visibility isBorderVis = Visibility.Collapsed;
        private Car selectedCar;
        private BindableCollection<Car> cars = new BindableCollection<Car>();
        private string selectedSort;

        public Dictionary<string, System.Action> Sort { get; set; } = new Dictionary<string, System.Action>();
        public string SelectedSort
        {
            get => selectedSort;
            set
            {
                selectedSort = value;
                Sort[selectedSort]?.Invoke();
            }
        }
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
        private bool reversSort;

        public bool ReversSort
        {
            get => reversSort;
            set
            {
                reversSort = value;
                Sort[selectedSort]?.Invoke();
            }
        }



        public ShellViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);
            ReadCars();
            InitializeSort();
            
        }

        private void InitializeSort()
        {
            Sort.Add("Id", IdSort);
            Sort.Add("Title", NameSort);
            Sort.Add("Model", ModelSort);
            Sort.Add("Color", ColorSort);
            Sort.Add("Year", YearSort);
            Sort.Add("Price", PriceSort);
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
        }

        public void EditCar()
        {
            if (SelectedCar == null) return;
            IsBorderVis = Visibility.Visible;
            ActivateItem(new EditCarViewModel(eventAggregator));
            eventAggregator.PublishOnUIThread(SelectedCar);
            eventAggregator.PublishOnUIThread(Cars);
        }

        public void DeleteCar()
        {
            if (SelectedCar == null) return;
            Cars.Remove(SelectedCar);
        }

        public void IdSort()
        {
            IOrderedEnumerable<Car> temp;
            if (ReversSort == false) temp = Cars.OrderBy(x => x.Id);
            else temp = Cars.OrderByDescending(x => x.Id);

            Cars = GetSortedCollection(temp);
        }
        public void NameSort()
        {
            IOrderedEnumerable<Car> temp;
            if (ReversSort == false) temp = Cars.OrderBy(x => x.Title);
            else temp = Cars.OrderByDescending(x => x.Title);
         
            Cars = GetSortedCollection(temp);
        }
        public void ModelSort()
        {
            IOrderedEnumerable<Car> temp;
            if (ReversSort == false) temp = Cars.OrderBy(x => x.Model);
            else temp = Cars.OrderByDescending(x => x.Model);

            Cars = GetSortedCollection(temp);
        }
        public void ColorSort()
        {
            IOrderedEnumerable<Car> temp;
            if (ReversSort == false) temp = Cars.OrderBy(x => x.Color);
            else temp = Cars.OrderByDescending(x => x.Color);

            Cars = GetSortedCollection(temp);
        }
        public void YearSort()
        {
            IOrderedEnumerable<Car> temp;
            if (ReversSort == false) temp = Cars.OrderBy(x => x.Year);
            else temp = Cars.OrderByDescending(x => x.Year);

            Cars = GetSortedCollection(temp);
        }
        public void PriceSort()
        {
            IOrderedEnumerable<Car> temp;
            if (ReversSort == false) temp = Cars.OrderBy(x => x.Price);
            else temp = Cars.OrderByDescending(x => x.Price);

            Cars = GetSortedCollection(temp);
        }

        public BindableCollection<Car> GetSortedCollection(IOrderedEnumerable<Car> temp)=> new BindableCollection<Car>(temp);


        public void Handle(Visibility message)
        {
            IsBorderVis = message;
        }
    }
}