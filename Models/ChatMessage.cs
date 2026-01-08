using System;

namespace WpfChatApp.Models
{
    public class ChatMessage
    {
        public string Content { get; set; }
        public bool IsUser { get; set; }
        public DateTime Timestamp { get; set; }
        public ChatAttachment Attachment { get; set; }

        public ChatMessage(string content, bool isUser, ChatAttachment attachment = null)
        {
            Content = content;
            IsUser = isUser;
            Timestamp = DateTime.Now;
            Attachment = attachment;
        }
    }
}
