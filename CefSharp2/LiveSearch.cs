using Stylet;

namespace CefSharp2
{
    public class LiveSearch
    {
        public string Title { get; set; }

        public string Url { get; set; }

        public string Content { get; set; }

        public BindableCollection<ItemResult> ItemResults { get; set; }

        public LiveSearch(string title, string url, string content)
        {
            Title = title;
            Url = url;
            Content = content;
        }
    }
}