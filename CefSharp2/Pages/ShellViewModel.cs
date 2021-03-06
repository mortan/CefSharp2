using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using Stylet;

namespace CefSharp2.Pages
{
    public class ShellViewModel : Screen
    {
        private readonly LiveSearchService _liveSearchService;
        public BindableCollection<LiveSearch> LiveSearches { get; set; } = new BindableCollection<LiveSearch>();

        private LiveSearch _selectedSearch;
        public LiveSearch SelectedSearch
        {
            get { return _selectedSearch; }
            set => SetAndNotify(ref _selectedSearch, value);
        }

        private bool _isLiveSearchRunning;
        public bool IsLiveSearchRunning
        {
            get => _isLiveSearchRunning;
            set => SetAndNotify(ref _isLiveSearchRunning, value);
        }

        public ShellViewModel(LiveSearchService liveSearchService)
        {
            LiveSearch[] liveSearches = new[]
            {
                new LiveSearch("Test 1", "wss://www.pathofexile.com/api/trade/live/Standard/029bFg", "bar")
                {
                    ItemResults =  new BindableCollection<ItemResult>
                    {
                        //new ItemResult
                        //{
                        //    Name  = "Item 1",
                        //    Price = 100
                        //},
                        //new ItemResult
                        //{
                        //    Name  = "Item 2",
                        //    Price = 200
                        //}
                    }
                //}
                //new LiveSearch("Test 2", "http://test2.com", "bar")
                }
            };
            LiveSearches.AddRange(liveSearches);
            SelectedSearch = LiveSearches[0];

            _liveSearchService = liveSearchService;
            _liveSearchService.NewSearchResult += LiveSearchServiceOnNewSearchResult;
        }

        public void CloseSearch(LiveSearch search)
        {
            LiveSearches.Remove(search);
        }

        public void ToggleLiveSearch()
        {
            //foreach (LiveSearch liveSearch in LiveSearches)
            //{
            //    _liveSearchService.AddSearch(liveSearch.Url);
            //}

            for (int i = 0; i < 10; i++)
            {
                LiveSearches[0].ItemResults.Insert(0,
                    new ItemResult
                    {
                        Name  = $"Item {i}",
                        Price = 100
                    }
                );
            }
        }

        private void LiveSearchServiceOnNewSearchResult(object sender, SearchResult searchResult)
        {
            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                LiveSearch liveSearch = LiveSearches.Single(x => x.Url == searchResult.Url);
                List<Types.Result> results = searchResult.Responses.SelectMany(x => x.Result).ToList();
                List<ItemResult> itemResults = results.Select(x =>
                    new ItemResult
                    {
                        Name = x.Item.Name,
                        Price = x.Listing.Price?.Amount ?? 0,
                        Currency = x.Listing.Price?.Currency ?? string.Empty
                    }).ToList();

                liveSearch.ItemResults.AddRange(itemResults);
            });
        }
    }
}
