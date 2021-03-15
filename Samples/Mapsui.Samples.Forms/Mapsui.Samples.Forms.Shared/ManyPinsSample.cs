using Mapsui.Rendering.Skia;
using Mapsui.Samples.Common;
using Mapsui.Samples.Common.Maps;
using Mapsui.UI;
using Mapsui.UI.Forms;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Xamarin.Forms;
using XFColor = Xamarin.Forms.Color;

namespace Mapsui.Samples.Forms
{
    public class ManyPinsSample : IFormsSample
    {
        static int markerNum = 1;
        private static readonly Random rnd = new();

        public string Name => "Add many Pins Sample";

        public string Category => "Forms";

        private const float _r = 1.0f/256.0f;
        private static XFColor RandomColor()
        {
            int r = rnd.Next(0,256);
            int g = rnd.Next(0,256);
            int b = rnd.Next(0,256);
            return new XFColor(r*_r,g*_r,b*_r);
        }

        public bool OnClick(object sender, EventArgs args)
        {
            var mapView = sender as MapView;
            var e = args as MapClickedEventArgs;

            var assembly = typeof(AllSamples).GetTypeInfo().Assembly;
            foreach (var str in assembly.GetManifestResourceNames())
                System.Diagnostics.Debug.WriteLine(str);

            switch (e.NumOfTaps)
            {
                case 1:
                    var pin = new Pin {
                        Label = $"PinType.Pin {markerNum++}",
                        Address = e.Point.ToString(),
                        Position = e.Point,
                        Color = RandomColor(),
                        Transparency = 0.5f,
                        Scale = rnd.Next(50, 130) * 0.01f,
                        Type = PinType.Pin,
                    };
                    pin.Callout.Anchor = new Point(0, pin.Height * pin.Scale);
                    pin.Callout.RectRadius = rnd.Next(0, 30);
                    pin.Callout.ArrowHeight = rnd.Next(0, 20);
                    pin.Callout.ArrowWidth = rnd.Next(0, 20);
                    pin.Callout.ArrowAlignment = (ArrowAlignment)rnd.Next(0, 4);
                    pin.Callout.ArrowPosition = rnd.Next(0, 100) * 0.01;
                    pin.Callout.BackgroundColor = Color.White;
                    pin.Callout.Color = pin.Color;
                    if (rnd.Next(0, 3) < 2)
                    {
                        pin.Callout.Type = CalloutType.Detail;
                        pin.Callout.TitleFontSize = rnd.Next(15, 30);
                        pin.Callout.SubtitleFontSize = pin.Callout.TitleFontSize - 5;
                        pin.Callout.TitleFontColor = RandomColor();
                        pin.Callout.SubtitleFontColor = pin.Color;
                    }
                    else
                    {
                        pin.Callout.Type = CalloutType.Detail;
                        pin.Callout.Content = 1;
                    }
                    mapView.Pins.Add(pin);
                    pin.ShowCallout();
                    break;
                case 2:
                    foreach (var r in assembly.GetManifestResourceNames())
                        System.Diagnostics.Debug.WriteLine(r);

                    var stream = assembly.GetManifestResourceStream("Mapsui.Samples.Common.Images.Ghostscript_Tiger.svg");
                    StreamReader reader = new StreamReader(stream);
                    string svgString = reader.ReadToEnd();
                    mapView.Pins.Add(new Pin {
                        Label = $"PinType.Svg {markerNum++}",
                        Position = e.Point,
                        Scale = 0.1f,
                        Svg = svgString,
                        Type = PinType.Svg,
                    });
                    break;
                case 3:
                    var icon = assembly.GetManifestResourceStream("Mapsui.Samples.Common.Images.loc.png").ToBytes();
                    mapView.Pins.Add(new Pin {
                        Label = $"PinType.Icon {markerNum++}",
                        Position = e.Point,
                        Scale = 0.5f,
                        Icon = icon,
                        Type = PinType.Icon,
                    });
                    break;
            }
            return true;
        }

        public void Setup(IMapControl mapControl)
        {
            mapControl.Map = OsmSample.CreateMap();

            ((MapView)mapControl).UseDoubleTap = true;
            ((MapView)mapControl).UniqueCallout = true;

            var sw = new Stopwatch();
            sw.Start();

            // Add 1000 pins
            var list = new System.Collections.Generic.List<Pin>();
            for (var i = 0; i < 1000; i++)
            {
                list.Add(CreatePin(i));
            }

            sw.Stop();
            var timePart1 = sw.Elapsed;
            Debug.WriteLine($"[ELAPSED] 1  = {timePart1}");
            sw.Restart();

            var pins = ((MapView)mapControl).Pins;

            if (pins is ObservableRangeCollection<Pin> range)
            {
                Debug.WriteLine("observable range!");
                range.AddRange(list);
            }
            else
            {
                Debug.WriteLine("not a range!");
                foreach (var i in list)
                {
                    pins.Add(i);
                }
            }

            var timePart2 = sw.Elapsed;
            Debug.WriteLine($"[ELAPSED] 2  = {timePart2}");

            sw.Stop();
        }

        private Pin CreatePin(int num)
        {
            var position = new Position(rnd.Next(-85000, +85000) * 0.001,
                                        rnd.Next(-180000, +180000) * 0.001);

            var pin = new Pin()
            {
                Label = $"PinType.Pin {num++}",
                Address = position.ToString(),
                Position = position,
                Color = RandomColor(),
                Transparency = 0.5f,
                Scale = rnd.Next(50, 130) * 0.01f,
                Type = PinType.Pin,
            };
            pin.Callout.Anchor = new Point(0, pin.Height * pin.Scale);
            pin.Callout.RectRadius = rnd.Next(0, 30);
            pin.Callout.ArrowHeight = rnd.Next(0, 20);
            pin.Callout.ArrowWidth = rnd.Next(0, 20);
            pin.Callout.ArrowAlignment = (ArrowAlignment)rnd.Next(0, 4);
            pin.Callout.ArrowPosition = rnd.Next(0, 100) * 0.01;
            pin.Callout.BackgroundColor = Color.White;
            pin.Callout.Color = pin.Color;
            if (rnd.Next(0, 3) < 2)
            {
                pin.Callout.Type = CalloutType.Detail;
                pin.Callout.TitleFontSize = rnd.Next(15, 30);
                pin.Callout.SubtitleFontSize = pin.Callout.TitleFontSize - 5;
                pin.Callout.TitleFontColor = RandomColor();
                pin.Callout.SubtitleFontColor = pin.Color;
            }
            else
            {
                pin.Callout.Type = CalloutType.Detail;
                pin.Callout.Content = 1;
            }

            return pin;
        }
    }
}
