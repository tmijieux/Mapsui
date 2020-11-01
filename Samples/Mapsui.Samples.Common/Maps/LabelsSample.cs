using Mapsui.Geometries;
using Mapsui.Layers;
using Mapsui.Providers;
using Mapsui.Styles;
using Mapsui.UI;
using Mapsui.Utilities;

namespace Mapsui.Samples.Common.Maps
{
    public class LabelsSample : ISample
    {
        public string Name => "Labels";
        public string Category => "Symbols";

        public void Setup(IMapControl mapControl)
        {
            mapControl.Map = CreateMap();
        }

        public static Map CreateMap()
        {
            var map = new Map();
            map.Layers.Add(OpenStreetMap.CreateTileLayer());
            map.Layers.Add(CreateLayer());
            return map;
        }

        public static ILayer CreateLayer()
        {
            var features = new Features
            {
                CreateFeatureWithTailTruncation(),
            };

            var memoryProvider = new MemoryProvider(features);

            return new MemoryLayer {Name = "Points with labels", DataSource = memoryProvider};
        }

        private static IFeature CreateFeatureWithTailTruncation()
        {
            var featureWithColors = new Feature { Geometry = new Point(8000000, 2000000) };
            featureWithColors.Styles.Add(new LabelStyle
            {
                Text = "16:TPP-DG14",
                BackColor = new Brush(Color.Transparent),
                ForeColor = Color.White,
                Halo = new Pen(Color.Black, 2),
                HorizontalAlignment = LabelStyle.HorizontalAlignmentEnum.Left,
                VerticalAlignment = LabelStyle.VerticalAlignmentEnum.Center,
                MaxWidth = 10,
                WordWrap = LabelStyle.LineBreakMode.TailTruncation,
                Offset = new Offset(0, 15, false),
                MaxVisible = 400
            });
            return featureWithColors;
        }
    }
}