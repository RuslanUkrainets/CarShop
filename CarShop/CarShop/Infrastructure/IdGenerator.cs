using Caliburn.Micro;
using CarShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Infrastructure
{
    public static class IdGenerator
    {
        private static int id = 0;

        public static int GetId(BindableCollection<Car> collection)
        {
            if(collection.Count != 0)
            {
                id = collection.Max(x => x.Id);
                return ++id;
            }
            else
            {
                return ++id;
            }
            
        }
    }
}
