using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CarShop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            
        }

        public static void SwichTheme(MaterialDesignThemes.Wpf.BaseTheme theme)
        {            
            var res = Current.Resources.MergedDictionaries.FirstOrDefault(x => x.Source.ToString().Contains("Light") || x.Source.ToString().Contains("Dark"));
            if (theme == MaterialDesignThemes.Wpf.BaseTheme.Dark)
            {
                res.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Dark.xaml");
            }
            else if(theme == MaterialDesignThemes.Wpf.BaseTheme.Light)
            {
                res.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml");
            }
        }
    }
}
