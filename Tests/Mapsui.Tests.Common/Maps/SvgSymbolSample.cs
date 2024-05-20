﻿using Mapsui.Layers;
using Mapsui.Samples.Common;
using Mapsui.Styles;
using System.Collections.Generic;
using System.Threading.Tasks;

#pragma warning disable IDISP001 // Dispose created

namespace Mapsui.Tests.Common.Maps;

public class SvgSymbolSample : ISample
{
    public string Name => "Svg Symbol";
    public string Category => "Tests";

    public Task<Map> CreateMapAsync() => Task.FromResult(CreateMap());

    public static Map CreateMap()
    {
        var layer = new MemoryLayer
        {
            Style = null,
            Features = CreateFeatures(),
            Name = "Points with Svg"
        };

        var map = new Map
        {
            BackColor = Color.WhiteSmoke,
        };

        map.Navigator.ZoomToBox(layer.Extent!.Grow(layer.Extent.Width * 2));

        map.Layers.Add(layer);

        return map;
    }

    public static IEnumerable<IFeature> CreateFeatures()
    {
        var imageSourceOfPinSymbol = "embedded://mapsui.resources.images.pin.svg";

        return
        [
            new PointFeature(new MPoint(50, 50)) {
                Styles = new[] { CreateSymbolStyle(imageSourceOfPinSymbol) }
            },
            new PointFeature(new MPoint(50, 100)) {
                Styles = new[] { CreateSymbolStyle(imageSourceOfPinSymbol) }
            },
            new PointFeature(new MPoint(100, 50)) {
                Styles = new[] { CreateSymbolStyle(imageSourceOfPinSymbol) }
            },
            new PointFeature(new MPoint(100, 100)) {
                Styles = new[] { CreateSymbolStyle(imageSourceOfPinSymbol) }
            }
        ];
    }

    private static SymbolStyle CreateSymbolStyle(string imageSource) => new()
    {
        ImageSource = imageSource,
        BlendModeColor = Color.FromRgba(0, 177, 0, 255)
    };
}
