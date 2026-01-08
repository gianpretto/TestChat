using System;
using System.Linq;
using System.Windows;

namespace WpfChatApp.Services
{
    public class LanguageService
    {
        private const string EnSource = "pack://application:,,,/Resources/Strings.en.xaml";
        private const string EsSource = "pack://application:,,,/Resources/Strings.es.xaml";

        public enum LanguageType { English, Spanish }

        public LanguageType CurrentLanguage { get; private set; }

        public void SetLanguage(LanguageType language)
        {
            var uri = new Uri(language == LanguageType.English ? EnSource : EsSource);
            var resourceDict = new ResourceDictionary { Source = uri };

            // Find existing language dictionary to remove
            var existingDict = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && (d.Source.OriginalString.Contains("Strings.en.xaml") || d.Source.OriginalString.Contains("Strings.es.xaml")));

            if (existingDict != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(existingDict);
            }
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);

            CurrentLanguage = language;
        }

        public void ToggleLanguage()
        {
            SetLanguage(CurrentLanguage == LanguageType.English ? LanguageType.Spanish : LanguageType.English);
        }
    }
}
