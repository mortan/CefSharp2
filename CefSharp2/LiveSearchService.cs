using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Documents;
using Newtonsoft.Json;
using WebSocketSharp;
using Cookie = WebSocketSharp.Net.Cookie;

namespace CefSharp2
{
    public class LiveSearchService
    {
        private Dictionary<string, WebSocket> urlToWebSocket = new Dictionary<string, WebSocket>();
        public event EventHandler<SearchResult> NewSearchResult;

        public void AddSearch(string url)
        {
            // "wss://www.pathofexile.com/api/trade/live/Standard/029bFg"
            urlToWebSocket[url] = StartNewSearch(url);
        }

        private WebSocket StartNewSearch(string url)
        {
            WebSocket ws = new WebSocket(url);
            ws.EnableRedirection = true;
            ws.Log.Level = LogLevel.Trace;
            ws.Origin = "https://www.pathofexile.com";
            ws.SetCookie(new Cookie("__cfduid", "dbae908658e7525eca710a87b165208291544107321"));
            ws.SetCookie(new Cookie("POESESSID", "ccb0a5e18f4c2a83625b059589d1dcb6"));
            ws.OnMessage += WsOnOnMessage;
            ws.Connect ();

            return ws;
        }

        private void WsOnOnMessage(object sender, MessageEventArgs e)
        {
            var pushNotification = JsonConvert.DeserializeObject<Types.PushNotification>(e.Data);
            using (var webClient = new WebClient())
            {
                List<Types.SearchTokenRequestResponse> list = pushNotification.SearchTokens
                    .Select(searchToken =>
                    {
                        string json = webClient.DownloadString($"https://www.pathofexile.com/api/trade/fetch/{searchToken}?query=029bFg");
                        return Types.SearchTokenRequestResponse.FromJson(json);
                    })
                    .ToList();

                SearchResult searchResult = new SearchResult
                {
                    Url = ((WebSocket) sender).Url.AbsoluteUri,
                    Responses = list
                };

                NewSearchResult?.Invoke(this, searchResult);
            }
        }
    }
}