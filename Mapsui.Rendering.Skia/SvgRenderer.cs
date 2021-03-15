using System;
using Mapsui.Styles;
using SkiaSharp;

namespace Mapsui.Rendering.Skia
{
    public class SvgRenderer
    {
        public static void Draw(SKCanvas canvas, SKPicture picture, float x, float y, float orientation = 0,
            float offsetX = 0, float offsetY = 0,
            LabelStyle.HorizontalAlignmentEnum horizontalAlignment = LabelStyle.HorizontalAlignmentEnum.Left,
            LabelStyle.VerticalAlignmentEnum verticalAlignment = LabelStyle.VerticalAlignmentEnum.Top,
            float opacity = 1f,
            float scale = 1f)
        {
            // todo: I assume we also need to apply opacity.
            // todo: It seems horizontalAlignment and verticalAlignment would make sense too. Is this similar to Anchor?

            canvas.Save();

            if (x != 0.0f || y != 0.0f)
                canvas.Translate(x, y);
            if (orientation != 0.0f)
                canvas.RotateDegrees(orientation);
            if (scale != 1.0f)
                canvas.Scale(scale);

            var x2 = - picture.CullRect.Width / 2 + offsetX;
            var y2 = - picture.CullRect.Height / 2 - offsetY;
            // 0/0 are assumed at center of image, but Svg has 0/0 at left top position
            if (x2 != 0.0f || y2 != 0.0f)
                canvas.Translate(x2, y2);

            var alpha = Convert.ToByte(255 * opacity);
            var transparency = SKColors.White.WithAlpha(alpha);
            using var cf = SKColorFilter.CreateBlendMode(transparency, SKBlendMode.DstIn);
            using var paint = new SKPaint { IsAntialias = true, ColorFilter = cf };
            canvas.DrawPicture(picture, paint);
            canvas.Restore();
        }
    }
}
