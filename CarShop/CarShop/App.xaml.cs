using CarShop.Infrastructure;
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

        public static void SwichLanguage(Languages language)
        {
            var res = Current.Resources.MergedDictionaries.FirstOrDefault(x => x.Source.ToString().Contains("English") || x.Source.ToString().Contains("Russian"));
            if (language == Languages.Russian)
            {
                res.Source = new Uri("pack://application:,,,/Resourcess/RussianDictionary.xaml");
            }
            else if (language == Languages.English)
            {
                res.Source = new Uri("pack://application:,,,/Resourcess/EnglishDictionary.xaml");
            }
        }
    }
}
