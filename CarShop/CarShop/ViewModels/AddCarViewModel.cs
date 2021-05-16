using Caliburn.Micro;
using CarShop.Infrastructure;
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
    class AddCarViewModel : Screen, IHandle<BindableCollection<Car>>
    {
        private readonly IEventAggregator eventAggregator;
        private BindableCollection<Car> cars;

        public Car Car { get; set; } = new Car();

        public AddCarViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);
        }

        public void Save()
        {
            Car.Id = IdGenerator.GetId(cars);
            cars.Add(Car);
            ClosePage();
        }

        public void Handle(BindableCollection<Car> message)
        {
            cars = message;
        }

        public void ClosePage()
        {
            eventAggregator.PublishOnUIThread(Visibility.Collapsed);
            eventAggregator.Unsubscribe(this);
            this.TryClose();
        }
    }
}
