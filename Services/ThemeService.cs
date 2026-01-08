using System;
using System.Windows;

namespace WpfChatApp.Services
{
    public class ThemeService
    {
        private const string DarkThemeSource = "pack://application:,,,/Resources/DarkTheme.xaml";
        private const string LightThemeSource = "pack://application:,,,/Resources/LightTheme.xaml";

        public enum ThemeType { Light, Dark }

        public ThemeType CurrentTheme { get; private set; }

        public void SetTheme(ThemeType theme)
        {
            var uri = new Uri(theme == ThemeType.Light ? LightThemeSource : DarkThemeSource);
            var resourceDict = new ResourceDictionary { Source = uri };

            // Find existing theme dictionary to remove
            // We look for dictionaries containing "Theme.xaml"
            var existingDict = System.Linq.Enumerable.FirstOrDefault(
                Application.Current.Resources.MergedDictionaries,
                d => d.Source != null && (d.Source.OriginalString.Contains("DarkTheme.xaml") || d.Source.OriginalString.Contains("LightTheme.xaml")));

            if (existingDict != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(existingDict);
            }
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);

            CurrentTheme = theme;
        }

        public void ToggleTheme()
        {
            SetTheme(CurrentTheme == ThemeType.Dark ? ThemeType.Light : ThemeType.Dark);
        }
    }
}
