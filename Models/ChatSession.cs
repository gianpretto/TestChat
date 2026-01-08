using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WpfChatApp.Models
{
    public class ChatSession
    {
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public List<ChatMessage> Messages { get; set; }

        public ChatSession(string title, IEnumerable<ChatMessage> messages)
        {
            Title = title;
            DateCreated = DateTime.Now;
            Messages = new List<ChatMessage>(messages);
        }
    }
}
