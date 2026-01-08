using System.Windows;
using System.Windows.Input;
using WpfChatApp.ViewModels;

namespace WpfChatApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
        private void SearchBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SearchBox.Focus();
        }

        private void InputBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.V && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                if (Clipboard.ContainsImage())
                {
                    var image = Clipboard.GetImage();
                    if (image != null)
                    {
                        var tempPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"paste_{System.DateTime.Now.Ticks}.png");
                        using (var fileStream = new System.IO.FileStream(tempPath, System.IO.FileMode.Create))
                        {
                            var encoder = new System.Windows.Media.Imaging.PngBitmapEncoder();
                            encoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(image));
                            encoder.Save(fileStream);
                        }
                        
                        if (DataContext is MainViewModel vm)
                        {
                            vm.AttachFileFromPath(tempPath);
                            e.Handled = true;
                        }
                    }
                }
                else if (Clipboard.ContainsFileDropList())
                {
                    var files = Clipboard.GetFileDropList();
                    if (files.Count > 0)
                    {
                        if (DataContext is MainViewModel vm)
                        {
                            vm.AttachFileFromPath(files[0]);
                            e.Handled = true;
                        }
                    }
                }
            }
        }
    }
}