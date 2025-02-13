﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;

namespace ReactiveSearch
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SearchServiceClient _client = new SearchServiceClient();
        private IDisposable _subscription;

        public MainWindow()
        {
            InitializeComponent();

            //Install-Package System.Reactive

            var minTextLength = 3;

            var searches = Observable.Empty<string>();


            _subscription =
                searches.Subscribe(
                    results => SearchResults.ItemsSource = results,
                    err => { Debug.WriteLine(value: err); },
                    () =>
                    {
                        /* OnCompleted */
                    });


            #region clearing results for short search terms

            Observable.FromEventPattern(target: SearchBox, eventName: "TextChanged")
                .Select(_ => SearchBox.Text)
                .Where(txt => txt.Length < minTextLength)
                .ObserveOnDispatcher()
                .Subscribe(
                    results => SearchResults.ItemsSource = Enumerable.Empty<string>(),
                    err => { Debug.WriteLine(value: err); },
                    () =>
                    {
                        /* OnCompleted */
                    });

            #endregion
        }

        #region Backup

        private void ReactiveSearch()
        {
            //1. Create Observable from TextChanged event of the SearchBox
            //2. Select the text that was entered
            //3. Keep only strings with >3 characters
            //4. Take only if the time passed > 0.5 sec
            //5. Take only if it's not the same string again
            //6. Asynchronically call the Search service
            //7. Discard results if another search was requested
            //8. Show the results inside the SearchResult listbox  

            var client = new SearchServiceClient();

            _subscription =
                Observable.FromEventPattern(target: SearchBox, nameof(SearchBox.TextChanged))
                    .Select(_ => SearchBox.Text)
                    .Where(txt => txt.Length >= 3)
                    .Throttle(TimeSpan.FromSeconds(0.5))
                    .DistinctUntilChanged()
                    .Select(txt => _client.SearchAsync(searchTerm: txt))
                    .Switch()
                    .ObserveOnDispatcher()
                    .Subscribe(
                        results => SearchResults.ItemsSource = results,
                        err => { Debug.WriteLine(value: err); },
                        () =>
                        {
                            /* OnCompleted */
                        });


            #region clearing results for short search terms

            Observable.FromEventPattern(target: SearchBox, eventName: "TextChanged")
                .Select(_ => SearchBox.Text)
                .Where(txt => txt.Length < 3)
                .ObserveOnDispatcher()
                .Subscribe(
                    results => SearchResults.ItemsSource = Enumerable.Empty<string>(),
                    err => { Debug.WriteLine(value: err); },
                    () =>
                    {
                        /* OnCompleted */
                    });

            #endregion
        }

        #endregion
    }
}