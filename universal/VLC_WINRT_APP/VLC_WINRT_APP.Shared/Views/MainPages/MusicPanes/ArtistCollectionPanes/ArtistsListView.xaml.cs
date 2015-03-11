﻿using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Xaml.Interactivity;
using VLC_WINRT_APP.Helpers;
using VLC_WINRT_APP.Model.Music;
using VLC_WINRT_APP.Model.Video;
using VLC_WINRT_APP.ViewModels;
using WinRTXamlToolkit.Controls.Extensions;

namespace VLC_WINRT_APP.Views.MainPages.MusicPanes.ArtistCollectionPanes
{
    public sealed partial class ArtistsListView : UserControl
    {
        private bool isNarrow = false;
        public ArtistsListView()
        {
            this.InitializeComponent();
            this.Loaded += ArtistCollectionBase_Loaded;
        }

        void ArtistCollectionBase_Loaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged += Current_SizeChanged;
            this.Unloaded += ArtistCollectionBase_Unloaded;
            Responsive();
        }

        void ArtistCollectionBase_Unloaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged -= Current_SizeChanged;
        }

        void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            Responsive();
        }

        async void Responsive()
        {
#if WINDOWS_APP
            if (Window.Current.Bounds.Width < 900)
            {
                var wrapGrid = (ArtistListView).GetFirstDescendantOfType<ItemsWrapGrid>();
                if (wrapGrid != null)
                {
                    wrapGrid.Orientation = Orientation.Horizontal;
                    TemplateSizer.ComputeAlbums(wrapGrid, TemplateSize.Compact, this.ActualWidth);
                }
                if (!isNarrow)
                {
                    VisualStateUtilities.GoToState(this, "Narrow", false);
                    isNarrow = true;
                }
            }
            else
#endif
            {
                if (isNarrow)
                {
                    VisualStateUtilities.GoToState(this, "Wide", false);
                    isNarrow = false;
                }
            }
        }

        private void ArtistListView_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var artist = e.ClickedItem as ArtistItem;
            if (Window.Current.Bounds.Width > 800)
                Locator.MusicLibraryVM.CurrentArtist = artist;
            else Locator.MusicLibraryVM.ArtistClickedCommand.Execute(artist);
        }
    }
}
