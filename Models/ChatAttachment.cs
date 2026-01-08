using System;

namespace WpfChatApp.Models
{
    public class ChatAttachment
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }

        public ChatAttachment(string fileName, string filePath, long fileSize)
        {
            FileName = fileName;
            FilePath = filePath;
            FileSize = fileSize;
        }
        
        public string FormattedSize
        {
            get
            {
                if (FileSize < 1024) return $"{FileSize} B";
                if (FileSize < 1024 * 1024) return $"{FileSize / 1024.0:F1} KB";
                return $"{FileSize / (1024.0 * 1024.0):F1} MB";
            }
        }

        public bool IsImage
        {
            get
            {
                if (string.IsNullOrEmpty(FilePath)) return false;
                var ext = System.IO.Path.GetExtension(FilePath).ToLower();
                return ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".bmp" || ext == ".gif" || ext == ".webp";
            }
        }

        public string FileIcon
        {
            get
            {
                if (string.IsNullOrEmpty(FilePath)) return "📄";
                var ext = System.IO.Path.GetExtension(FilePath).ToLower();
                switch (ext)
                {
                    case ".xls":
                    case ".xlsx":
                    case ".csv":
                        return "📊";
                    case ".pdf":
                        return "📕";
                    case ".doc":
                    case ".docx":
                    case ".txt":
                        return "📝";
                    case ".zip":
                    case ".rar":
                    case ".7z":
                        return "📦";
                    case ".cs":
                    case ".html":
                    case ".css":
                    case ".js":
                    case ".json":
                    case ".xml":
                    case ".py":
                    case ".cpp":
                    case ".c":
                        return "💻";
                    case ".mp3":
                    case ".wav":
                        return "🎵";
                    case ".mp4":
                    case ".avi":
                    case ".mkv":
                    case ".mov":
                        return "🎥";
                    case ".jpg":
                    case ".jpeg":
                    case ".png":
                    case ".bmp":
                    case ".gif":
                    case ".webp":
                        return "🖼️";
                    default:
                        return "📄";
                }
            }
        }
    }
}
