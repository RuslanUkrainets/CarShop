using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CarShop.ViewModels
{
    public partial class ShellViewModel
    {
        private bool darkTheme;
        private bool lightTheme;

        public bool DarkTheme
        {
            get => darkTheme;
            set
            {
                darkTheme = value;
                if (darkTheme == true)
                {
                    App.SwichTheme(MaterialDesignThemes.Wpf.BaseTheme.Dark);
                    File.WriteAllText("SelectedTheme.txt", "dark");
                    NotifyOfPropertyChange(() => DarkTheme);
                }
            }
        }
        public bool LightTheme
        {
            get => lightTheme;
            set
            {
                lightTheme = value;
                if (lightTheme == true)
                {
                    App.SwichTheme(MaterialDesignThemes.Wpf.BaseTheme.Light);
                    File.WriteAllText("SelectedTheme.txt", "light");
                    NotifyOfPropertyChange(() => LightTheme);
                }
            }
        }

        private void InitializeTheme()
        {
            SetTheme();
        }

        private void SetTheme()
        {
            try
            {
                var theme = File.ReadAllText("SelectedTheme.txt");
                if (theme == "dark") DarkTheme = true;
                else if (theme == "light") LightTheme = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source);
            }
        }
    }
}
