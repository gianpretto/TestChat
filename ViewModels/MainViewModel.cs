using System;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Threading;
using System.Threading.Tasks;
using WpfChatApp.Core;
using WpfChatApp.Models;
using WpfChatApp.Services;
using System.ComponentModel;
using System.Windows.Data;

namespace WpfChatApp.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly ThemeService _themeService;
        private readonly LanguageService _languageService;
        private string _inputText;
        private bool _isTyping;
        private bool _isGenerating;
        private bool _isSettingsOpen;
        private string _searchText = "";
        private ChatAttachment? _pendingAttachment;
        private CancellationTokenSource? _cancellationTokenSource;
        private bool _hasStartedChat;

        private ChatSession? _currentChatSession;
        private ObservableCollection<ChatMessage> _messages;

        public ObservableCollection<ChatMessage> Messages
        {
            get => _messages;
            set => SetProperty(ref _messages, value);
        }

        public ObservableCollection<ChatSession> ChatHistory { get; } = new ObservableCollection<ChatSession>();
        public ICollectionView ChatHistoryView { get; private set; }

        public string InputText
        {
            get => _inputText;
            set => SetProperty(ref _inputText, value);
        }

        public bool IsTyping
        {
            get => _isTyping;
            set => SetProperty(ref _isTyping, value);
        }

        public ChatAttachment PendingAttachment
        {
            get => _pendingAttachment;
            set => SetProperty(ref _pendingAttachment, value);
        }

        public ICommand SendCommand { get; }
        public ICommand ToggleThemeCommand { get; }
        public ICommand NewChatCommand { get; }
        public ICommand ToggleLanguageCommand { get; }
        public ICommand AttachFileCommand { get; }
        public ICommand RemoveAttachmentCommand { get; }
        public ICommand CopyMessageCommand { get; }
        public ICommand StopGenerationCommand { get; }
        public ICommand ToggleSettingsCommand { get; }
        public ICommand OpenAttachmentCommand { get; }

        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                ChatHistoryView.Refresh();
            }
        }

        public bool IsGenerating
        {
            get => _isGenerating;
            set => SetProperty(ref _isGenerating, value);
        }

        public bool IsSettingsOpen
        {
            get => _isSettingsOpen;
            set => SetProperty(ref _isSettingsOpen, value);
        }



        public bool HasStartedChat
        {
            get => _hasStartedChat;
            set => SetProperty(ref _hasStartedChat, value);
        }

        public MainViewModel()
        {
            _themeService = new ThemeService();
            _languageService = new LanguageService();
            _themeService = new ThemeService();
            _languageService = new LanguageService();
            
            _themeService.SetTheme(ThemeService.ThemeType.Dark);
            _languageService.SetLanguage(LanguageService.LanguageType.Spanish);

            SendCommand = new RelayCommand(async _ => await SendMessageAsync(), _ => !string.IsNullOrWhiteSpace(InputText));
            ToggleThemeCommand = new RelayCommand(_ => _themeService.ToggleTheme());
            NewChatCommand = new RelayCommand(_ => StartNewChat());
            ToggleLanguageCommand = new RelayCommand(_ => _languageService.ToggleLanguage());
            AttachFileCommand = new RelayCommand(_ => AttachFile());
            RemoveAttachmentCommand = new RelayCommand(_ => PendingAttachment = null);
            CopyMessageCommand = new RelayCommand(CopyMessage);
            StopGenerationCommand = new RelayCommand(StopGeneration);
            ToggleSettingsCommand = new RelayCommand(ToggleSettings);
            OpenAttachmentCommand = new RelayCommand(OpenAttachment);

            OpenAttachmentCommand = new RelayCommand(OpenAttachment);

            _messages = new ObservableCollection<ChatMessage>();
            HasStartedChat = false;

            ChatHistoryView = CollectionViewSource.GetDefaultView(ChatHistory);
            ChatHistoryView.Filter = FilterChatHistory;
        }

        private bool FilterChatHistory(object obj)
        {
            if (obj is ChatSession session)
            {
                if (string.IsNullOrWhiteSpace(SearchText)) return true;
                return session.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        private async Task SendMessageAsync()
        {
            if (string.IsNullOrWhiteSpace(InputText) && PendingAttachment == null) return;

            var userMsg = InputText;
            var attachment = PendingAttachment;
            
            InputText = string.Empty;
            PendingAttachment = null;
            HasStartedChat = true;

            Messages.Add(new ChatMessage(userMsg, true, attachment));

            Messages.Add(new ChatMessage(userMsg, true, attachment));

            if (_currentChatSession == null)
            {
                var title = userMsg.Length > 20 ? userMsg.Substring(0, 20) + "..." : userMsg;
                _currentChatSession = new ChatSession(title, Messages);
                ChatHistory.Insert(0, _currentChatSession);
            }

            IsGenerating = true;
            IsTyping = true;
            _cancellationTokenSource = new CancellationTokenSource();

            try
            {
                await Task.Delay(3000, _cancellationTokenSource.Token);

                var aiResponse = _languageService.CurrentLanguage == LanguageService.LanguageType.Spanish
                    ? $"Soy una IA de prueba. Dijiste: \"{userMsg}\"."
                    : $"I am a mock AI. You said: \"{userMsg}\".";

                Messages.Add(new ChatMessage(aiResponse, false));
            }
            catch (TaskCanceledException)
            {
                // Stopped by user
            }
            finally
            {
                IsGenerating = false;
                IsTyping = false;
                _cancellationTokenSource?.Dispose();
                _cancellationTokenSource = null;
            }
        }

        private void StopGeneration(object parameter)
        {
            _cancellationTokenSource?.Cancel();
        }

        private void ToggleSettings(object parameter)
        {
            IsSettingsOpen = !IsSettingsOpen;
        }


        private void StartNewChat()
        {
            Messages = new ObservableCollection<ChatMessage>();
            _currentChatSession = null;
            HasStartedChat = false;
        }
        public void AttachFileFromPath(string filePath)
        {
            if (File.Exists(filePath))
            {
                var fileInfo = new FileInfo(filePath);
                PendingAttachment = new ChatAttachment(fileInfo.Name, fileInfo.FullName, fileInfo.Length);
            }
        }

        private void AttachFile()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                AttachFileFromPath(openFileDialog.FileName);
            }
        }

        private void CopyMessage(object parameter)
        {
            if (parameter is string message)
            {
                Clipboard.SetText(message);
            }
        }

        private void OpenAttachment(object parameter)
        {
            if (parameter is string filePath && File.Exists(filePath))
            {
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = filePath,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    
                }
            }
        }
    }
}
