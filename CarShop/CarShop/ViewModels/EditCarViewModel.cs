using Caliburn.Micro;
using CarShop.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CarShop.ViewModels
{
    class EditCarViewModel : Screen, IHandle<Car>, IHandle<BindableCollection<Car>>
    {
        private IEventAggregator eventAggregator;
        private Car newCar;
        private Car oldCar;
        private BindableCollection<Car> cars;

        public Car Car
        {
            get => newCar;
            set
            {
                newCar = value;
                NotifyOfPropertyChange(() => Car);
            }
        }

        public EditCarViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);
        }

        public void Save()
        {
            int indexCar = cars.IndexOf(oldCar);
            cars.RemoveAt(indexCar);
            cars.Insert(indexCar, newCar);
            ClosePage();
        }

        public void ClosePage()
        {
            eventAggregator.PublishOnUIThread(Visibility.Collapsed);
            eventAggregator.Unsubscribe(this);
            this.TryClose();
        }




        public void Handle(Car message)
        {
            oldCar = message;
            Car = new Car
            {
                Id = oldCar.Id,
                Title = oldCar.Title,
                Model = oldCar.Model,
                Color = oldCar.Color,
                Year = oldCar.Year,
                Price = oldCar.Price,
                Image = oldCar.Image
            };
        }

        public void Handle(BindableCollection<Car> message)
        {
            cars = message;
        }
    }
}
