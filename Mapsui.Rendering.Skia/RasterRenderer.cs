using System;
using System.Collections.Generic;
using System.Data;
using Mapsui.Geometries;
using Mapsui.Logging;
using Mapsui.Providers;
using Mapsui.Styles;
using SkiaSharp;

namespace Mapsui.Rendering.Skia
{
    public static class RasterRenderer
    {
        public static void Draw(
            SKCanvas canvas, IReadOnlyViewport viewport,
            IRaster raster,
            float opacity,
            Dictionary<object, BitmapInfo> tileCache,
            long currentIteration)
        {
            try
            {
                if (!tileCache.TryGetValue(raster, out var bitmapInfo))
                {
                    bitmapInfo = BitmapHelper.LoadBitmap(raster.Data);
                    tileCache[raster] = bitmapInfo;
                }

                bitmapInfo.IterationUsed = currentIteration;
                tileCache[raster] = bitmapInfo;

                var boundingBox = raster.BoundingBox;

                if (viewport.IsRotated)
                {
                    canvas.Save();
                    UpdateMatrix(canvas, viewport, boundingBox);
                    var destination = new SKRect(0.0f, 0.0f, (float)boundingBox.Width, (float)boundingBox.Height);
                    BitmapRenderer.DrawInRect(canvas, bitmapInfo.Image, destination, opacity);
                    canvas.Restore();
                }
                else
                {
                    var destination = WorldToScreen(viewport, raster.BoundingBox);
                    BitmapRenderer.DrawInRect(canvas, bitmapInfo.Image, RoundToPixel(destination).ToSkia(), opacity);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, ex.Message, ex);
            }
        }

        private static void UpdateMatrix(SKCanvas canvas, IReadOnlyViewport viewport, BoundingBox boundingBox)
        {
            canvas.Translate((float)viewport.Width / 2.0f, (float)viewport.Height / 2.0f);
            canvas.RotateDegrees((float)viewport.Rotation);

            float scale = 1.0f / (float)viewport.Resolution;
            if (scale != 1.0f)
                canvas.Scale(scale);
            float dx = (float)(boundingBox.Left - viewport.Center.X);
            float dy = (float)(viewport.Center.Y - boundingBox.Top);
            canvas.Translate(dx, dy);

            // We'll concatenate them like so: incomingMatrix * centerInScreen * userRotation * zoomScale * focalPointOffset
        }

        private static BoundingBox WorldToScreen(IReadOnlyViewport viewport, BoundingBox boundingBox)
        {
            var first = viewport.WorldToScreen(boundingBox.Min);
            var second = viewport.WorldToScreen(boundingBox.Max);
            return new BoundingBox
                (
                    Math.Min(first.X, second.X),
                    Math.Min(first.Y, second.Y),
                    Math.Max(first.X, second.X),
                    Math.Max(first.Y, second.Y)
                );
        }

        private static BoundingBox RoundToPixel(BoundingBox boundingBox)
        {
            return new BoundingBox(
                (float)Math.Round(boundingBox.Left),
                (float)Math.Round(Math.Min(boundingBox.Top, boundingBox.Bottom)),
                (float)Math.Round(boundingBox.Right),
                (float)Math.Round(Math.Max(boundingBox.Top, boundingBox.Bottom)));
        }
    }
}
