using System.Collections.Generic;

namespace CefSharp2
{
    public class SearchResult
    {
        public string Url { get; set; }

        public List<Types.SearchTokenRequestResponse> Responses { get; set; }
    }
}