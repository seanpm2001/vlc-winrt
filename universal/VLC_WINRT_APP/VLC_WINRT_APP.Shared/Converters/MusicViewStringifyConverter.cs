﻿using System;
using System.Collections.Generic;
using System.Text;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Data;
using VLC_WINRT_APP.Model;
using VLC_WINRT_APP.Model.Music;

namespace VLC_WINRT_APP.Converters
{
    public class MusicViewStringifyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var resourceLoader = new ResourceLoader();
            if (value is MusicView)
            {
                switch ((MusicView)value)
                {
                    case MusicView.Albums:
                        return resourceLoader.GetString("Albums").ToLower();
                    case MusicView.Artists:
                        return resourceLoader.GetString("Artists").ToLower();
                    case MusicView.Playlists:
                        return resourceLoader.GetString("Playlists").ToLower();
                    case MusicView.Songs:
                        return resourceLoader.GetString("Songs").ToLower();
                }
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
