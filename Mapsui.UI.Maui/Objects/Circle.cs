﻿using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Mapsui.GeometryLayer;
using Mapsui.Providers;
using Mapsui.Styles;
using Mapsui.UI.Maui.Extensions;
using Mapsui.UI.Objects;
using Microsoft.Maui.Controls;


namespace Mapsui.UI.Maui
{
    public class Circle : Drawable
    {
        public static readonly BindableProperty CenterProperty = BindableProperty.Create(nameof(Center), typeof(Position), typeof(Circle), default(Position));
        public static readonly BindableProperty RadiusProperty = BindableProperty.Create(nameof(Radius), typeof(Distance), typeof(Circle), Distance.FromMeters(1));
        public static readonly BindableProperty QualityProperty = BindableProperty.Create(nameof(Quality), typeof(double), typeof(Circle), 3.0);
        public static readonly BindableProperty FillColorProperty = BindableProperty.Create(nameof(FillColor), typeof(Microsoft.Maui.Graphics.Color), typeof(Circle), Microsoft.Maui.Graphics.Color.FromArgb("#FFA9A9A9"));

        public Circle()
        {
            CreateFeature();
        }

        /// <summary>
        /// Center of circle
        /// </summary>
        public Position Center
        {
            get => (Position)GetValue(CenterProperty);
            set => SetValue(CenterProperty, value);
        }

        /// <summary>
        /// Radius of circle in meters
        /// </summary>
        public Distance Radius
        {
            get => (Distance)GetValue(RadiusProperty);
            set => SetValue(RadiusProperty, value);
        }

        /// <summary>
        /// Color to fill circle with
        /// </summary>
        public Microsoft.Maui.Graphics.Color FillColor
        {
            get => (Microsoft.Maui.Graphics.Color)GetValue(FillColorProperty);
            set => SetValue(FillColorProperty, value);
        }

        /// <summary>
        /// Quality for circle. Determines, how many points used to draw circle. 3 is poorest, 360 best quality.
        /// </summary>
        public double Quality
        {
            get => (double)GetValue(QualityProperty);
            set => SetValue(QualityProperty, value);
        }

        protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(Center):
                    UpdateFeature();
                    break;
                case nameof(Radius):
                    UpdateFeature();
                    break;
                case nameof(Quality):
                    UpdateFeature();
                    break;
                case nameof(FillColor):
                    if (Feature != null)
                        ((VectorStyle)Feature.Styles.First()).Fill = new Styles.Brush(FillColor.ToMapsui());
                    break;
                case nameof(StrokeWidth):
                    if (Feature != null)
                    {
                        var outline = ((VectorStyle)Feature.Styles.First()).Outline;
                        if (outline != null)
                            outline.Width = StrokeWidth;
                    }

                    break;
                case nameof(StrokeColor):
                    if (Feature != null)
                    {
                        var outline = ((VectorStyle)Feature.Styles.First()).Outline;
                        if (outline != null)
                            outline.Color = StrokeColor.ToMapsui();
                    }

                    break;
            }
        }

        private readonly object _sync = new object();

        private void CreateFeature()
        {
            lock (_sync)
            {
                if (Feature == null)
                {
                    // Create a new one
                    Feature = new GeometryFeature
                    {
                        Geometry = new Geometries.Polygon(),
                        ["Label"] = Label,
                    };
                    Feature.Styles.Clear();
                    Feature.Styles.Add(new VectorStyle
                    {
                        Outline = new Pen { Width = StrokeWidth, Color = StrokeColor.ToMapsui() },
                        Fill = new Styles.Brush { Color = FillColor.ToMapsui() }
                    });
                }
            }
        }

        private void UpdateFeature()
        {
            if (Feature == null)
            {
                // Create a new one
                CreateFeature();
            }

            // Create new circle
            var centerX = Center.ToMapsui().X;
            var centerY = Center.ToMapsui().Y;
            var radius = Radius.Meters / Math.Cos(Center.Latitude / 180.0 * Math.PI);
            var increment = 360.0 / (Quality < 3.0 ? 3.0 : (Quality > 360.0 ? 360.0 : Quality));
            var exteriorRing = new Geometries.LinearRing();

            for (double angle = 0; angle < 360; angle += increment)
            {
                var angleRad = angle / 180.0 * Math.PI;
                exteriorRing.Vertices.Add(new Geometries.Point(radius * Math.Sin(angleRad) + centerX, radius * Math.Cos(angleRad) + centerY));
            }

            Feature!.Geometry = new Geometries.Polygon(exteriorRing);
        }
    }
}
