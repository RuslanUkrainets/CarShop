using CarShop.Infrastructure;
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
        private bool englishLanguage;
        private bool russianLanguage;

        public bool EnglishLanguage
        {
            get => englishLanguage;
            set
            {
                englishLanguage = value;
                if (englishLanguage == true)
                {
                    App.SwichLanguage(Languages.English);
                    File.WriteAllText("SelectedLanguage.txt", "english");
                    NotifyOfPropertyChange(() => EnglishLanguage);
                }
            }
        }
        public bool RussianLanguage
        {
            get => russianLanguage;
            set
            {
                russianLanguage = value;
                if (russianLanguage == true)
                {
                    App.SwichLanguage(Languages.Russian);
                    File.WriteAllText("SelectedLanguage.txt", "russian");
                    NotifyOfPropertyChange(() => RussianLanguage);
                }
            }
        }

        private void InitializeLanguage()
        {
            SetLanguage();
        }

        private void SetLanguage()
        {
            try
            {
                var theme = File.ReadAllText("SelectedLanguage.txt");
                if (theme == "russian") RussianLanguage = true;
                else if (theme == "english") EnglishLanguage = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source);
            }
        }
    }
}
