using Caliburn.Micro;
using CarShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.ViewModels
{
    public partial class ShellViewModel
    {
        private string selectedSort;
        private bool reversSort;

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
        public bool ReversSort
        {
            get => reversSort;
            set
            {
                reversSort = value;
                Sort[selectedSort]?.Invoke();
            }
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
        public BindableCollection<Car> GetSortedCollection(IOrderedEnumerable<Car> temp) => new BindableCollection<Car>(temp);
    }
}
