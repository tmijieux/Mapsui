using Mapsui.Styles;
using SkiaSharp;
using System;

namespace Mapsui.Rendering.Skia
{
    class BitmapRenderer
    {
        // The field below is static for performance. Effect has not been measured.
        // Note that the default FilterQuality is None. Setting it explicitly to Low increases the quality.
        private static readonly SKPaint DefaultPaint = new SKPaint { FilterQuality = SKFilterQuality.Low };

        public static void DrawInRect(SKCanvas canvas, SKImage img, SKRect rect, float layerOpacity = 1f)
        {
            canvas.DrawImage(img, rect, GetPaint(layerOpacity));
        }

        private static void DrawImpl(SKCanvas canvas, SKImage img, SKPoint pos, float layerOpacity = 1f)
        {
            canvas.DrawImage(img, pos, GetPaint(layerOpacity));
        }

        public static void Draw(SKCanvas canvas, SKImage img, float x, float y, float rotation = 0.0f,
            float offsetX = 0.0f, float offsetY = 0.0f,
            LabelStyle.HorizontalAlignmentEnum horizontalAlignment = LabelStyle.HorizontalAlignmentEnum.Center,
            LabelStyle.VerticalAlignmentEnum verticalAlignment = LabelStyle.VerticalAlignmentEnum.Center,
            float opacity = 1.0f)
        {
            canvas.Save();
            canvas.Translate(x, y);
            if (rotation != 0.0f)
                canvas.RotateDegrees(rotation);

            var width = img.Width;
            var height = img.Height;

            x = offsetX + DetermineHorizontalAlignmentCorrection(horizontalAlignment, width);
            y = -offsetY + DetermineVerticalAlignmentCorrection(verticalAlignment, height);

            var halfWidth = width >> 1;
            var halfHeight = height >> 1;

            // var rect = new SKRect(x - halfWidth, y - halfHeight,
            //                       x + halfWidth, y + halfHeight);
            //Draw(canvas, bitmap, rect, opacity);

            var pos = new SKPoint(x-halfWidth, y-halfHeight);
            DrawImpl(canvas, img, pos, opacity);
            canvas.Restore();
        }

        private static int DetermineHorizontalAlignmentCorrection(
            LabelStyle.HorizontalAlignmentEnum horizontalAlignment, int width)
        {
            if (horizontalAlignment == LabelStyle.HorizontalAlignmentEnum.Left) return width >> 1;
            if (horizontalAlignment == LabelStyle.HorizontalAlignmentEnum.Right) return -(width >> 1);
            return 0; // center
        }

        private static int DetermineVerticalAlignmentCorrection(
            LabelStyle.VerticalAlignmentEnum verticalAlignment, int height)
        {
            if (verticalAlignment == LabelStyle.VerticalAlignmentEnum.Top) return -(height >> 1);
            if (verticalAlignment == LabelStyle.VerticalAlignmentEnum.Bottom) return height >> 1;
            return 0; // center
        }

        private static SKPaint GetPaint(float layerOpacity)
        {
            if (Math.Abs(layerOpacity - 1) > Utilities.Constants.Epsilon)
            {
                // Unfortunately for opacity we need to set the Color and the Color
                // is part of the Paint object. So we need to recreate the paint on
                // every draw.
                return new SKPaint
                {
                    FilterQuality = SKFilterQuality.Low,
                    Color = new SKColor(255, 255, 255, (byte)(255 * layerOpacity))
                };
            }
            return DefaultPaint;
        }
    }
}
