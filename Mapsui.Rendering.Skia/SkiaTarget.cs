using Mapsui.Providers;
using Mapsui.Styles;
using SkiaSharp;
using System;
using System.Collections.Generic;

namespace Mapsui.Rendering.Skia
{
    public class SkiaTarget
    {
        public IDictionary<uint, Tuple<IFeature, IStyle>> _dictionary = new Dictionary<uint, Tuple<IFeature, IStyle>>();

        public SKCanvas Canvas { get; set; }

        public bool InfoMode { get; set; }

        public void AddInfoId(uint id, IFeature feature, IStyle style)
        {
            _dictionary[id] = new Tuple<IFeature, IStyle>(feature, style);
        }

    }
}
