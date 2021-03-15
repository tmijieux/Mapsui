using Mapsui.Geometries;
using Mapsui.Styles;
using Mapsui.Widgets;

namespace Mapsui.Rendering.Skia
{
    /// <summary>
    /// Type of CalloutStyle
    /// </summary>
    public enum CalloutType
    {
        /// <summary>
        /// Only one line is shown
        /// </summary>
        Single,
        /// <summary>
        /// Header and detail is shown
        /// </summary>
        Detail,
        /// <summary>
        /// Content is custom, the bitmap given in Content is shown
        /// </summary>
        Custom,
    }

    /// <summary>
    /// Determins, where the pointer is
    /// </summary>
    public enum ArrowAlignment
    {
        /// <summary>
        /// Callout arrow is at bottom side of bubble
        /// </summary>
        Bottom,
        /// <summary>
        /// Callout arrow is at left side of bubble
        /// </summary>
        Left,
        /// <summary>
        /// Callout arrow is at top side of bubble
        /// </summary>
        Top,
        /// <summary>
        /// Callout arrow is at right side of bubble
        /// </summary>
        Right,
    }

    public class CalloutStyle : SymbolStyle
    {
        private CalloutType _type = CalloutType.Single;
        private ArrowAlignment _arrowAlignment = ArrowAlignment.Bottom;
        private float _arrowWidth = 8f;
        private float _arrowHeight = 8f;
        private float _arrowPosition = 0.5f;
        private float _rectRadius = 4f;
        private float _shadowWidth = 2f;
        private BoundingBox _padding = new BoundingBox(3f, 3f, 3f, 3f);
        private Color _color = Color.Black;
        private Color _backgroundColor = Color.White;
        private float _strokeWidth = 1f;
        private int _content = -1;
        private Offset _offset = new Offset(0, 0);
        private string _title;
        private string _subtitle;
        private Alignment _titleTextAlignment;
        private Alignment _subtitleTextAlignment;
        private double _spacing;
        private double _maxWidth;
        private Color _titleFontColor;
        private Color _subtitleFontColor;
        private bool _invalidated;

        public new static double DefaultWidth { get; set; } = 100;
        public new static double DefaultHeight { get; set; } = 30;

        public CalloutStyle()
        {
        }

        /// <summary>
        /// Type of Callout
        /// </summary>
        /// <remarks>
        /// Could be single, detail or custom. The last is a bitmap id for an owner drawn image.
        /// </remarks>
        public CalloutType Type
        {
            get => _type;
            set => SetAndInvalidateIfChanged(ref _type, value);
        }

        /// <summary>
        /// Offset position in pixels of Callout
        /// </summary>
        public Offset Offset
        {
            get => _offset;
            set => SetAndInvalidateIfChanged(ref _offset, value);
        }

        /// <summary>
        /// BoundingBox relative to offset point
        /// </summary>
        public BoundingBox BoundingBox = new BoundingBox();

        /// <summary>
        /// Gets or sets the rotation of the Callout in degrees (clockwise is positive)
        /// </summary>
        public double Rotation
        {
            get => SymbolRotation;
            set => SymbolRotation = value;
        }

        /// <summary>
        /// Anchor position of Callout
        /// </summary>
        public ArrowAlignment ArrowAlignment
        {
            get => _arrowAlignment;
            set => SetAndInvalidateIfChanged(ref _arrowAlignment, value);
        }

        /// <summary>
        /// Width of opening of anchor of Callout
        /// </summary>
        public float ArrowWidth
        {
            get => _arrowWidth;
            set => SetAndInvalidateIfChanged(ref _arrowWidth, value);
        }

        /// <summary>
        /// Height of anchor of Callout
        /// </summary>
        public float ArrowHeight
        {
            get => _arrowHeight;
            set => SetAndInvalidateIfChanged(ref _arrowWidth, value);
        }

        /// <summary>
        /// Relative position of anchor of Callout on the side given by AnchorType
        /// </summary>
        public float ArrowPosition
        {
            get => _arrowPosition;
            set => SetAndInvalidateIfChanged(ref _arrowPosition, value);
        }

        /// <summary>
        /// Color of stroke around Callout
        /// </summary>
        public Color Color
        {
            get => _color;
            set => SetAndInvalidateIfChanged(ref _color, value);
        }

        /// <summary>
        /// BackgroundColor of Callout
        /// </summary>
        public Color BackgroundColor
        {
            get => _backgroundColor;
            set => SetAndInvalidateIfChanged(ref _backgroundColor, value);
        }

        /// <summary>
        /// Stroke width of frame around Callout
        /// </summary>
        public float StrokeWidth
        {
            get => _strokeWidth;
            set => SetAndInvalidateIfChanged(ref _strokeWidth, value);
        }

        /// <summary>
        /// Radius of rounded corners of Callout
        /// </summary>
        public float RectRadius
        {
            get => _rectRadius;
            set => SetAndInvalidateIfChanged(ref _rectRadius, value);
        }

        /// <summary>
        /// Padding around content of Callout
        /// </summary>
        public BoundingBox Padding
        {
            get => _padding;
            set => SetAndInvalidateIfChanged(ref _padding, value);
        }

        /// <summary>
        /// Width of shadow around Callout
        /// </summary>
        public float ShadowWidth
        {
            get => _shadowWidth;
            set => SetAndInvalidateIfChanged(ref _shadowWidth, value);
        }

        /// <summary>
        /// Content of Callout
        /// </summary>
        /// <remarks>
        /// Is a BitmapId of a save image
        /// </remarks>
        public int Content
        {
            get => _content;
            set => SetAndInvalidateIfChanged(ref _content, value);
        }

        /// <summary>
        /// Content of Callout title label
        /// </summary>
        public string Title
        {
            get => _title;
            set => SetAndInvalidateIfChanged(ref _title, value);
        }

        /// <summary>
        /// Font color to render title
        /// </summary>
        public Color TitleFontColor
        {
            get => _titleFontColor;
            set => SetAndInvalidateIfChanged(ref _titleFontColor, value);
        }

        /// <summary>
        /// Text alignment of title
        /// </summary>
        public Alignment TitleTextAlignment
        {
            get => _titleTextAlignment;
            set => SetAndInvalidateIfChanged(ref _titleTextAlignment, value);
        }

        /// <summary>
        /// Content of Callout subtitle label
        /// </summary>
        public string Subtitle
        {
            get => _subtitle;
            set => SetAndInvalidateIfChanged(ref _subtitle, value);
        }

        /// <summary>
        /// Font color to render subtitle
        /// </summary>
        public Color SubtitleFontColor
        {
            get => _subtitleFontColor;
            set => SetAndInvalidateIfChanged(ref _subtitleFontColor, value);
        }

        /// <summary>
        /// Text alignment of subtitle
        /// </summary>
        public Alignment SubtitleTextAlignment
        {
            get => _subtitleTextAlignment;
            set => SetAndInvalidateIfChanged(ref _subtitleTextAlignment, value);
        }

        /// <summary>
        /// Space between Title and Subtitel of Callout
        /// </summary>
        public double Spacing
        {
            get => _spacing;
            set => SetAndInvalidateIfChanged(ref _spacing, value);
        }

        /// <summary>
        /// MaxWidth for Title and Subtitel of Callout
        /// </summary>
        public double MaxWidth
        {
            get => _maxWidth;
            set => SetAndInvalidateIfChanged(ref _maxWidth, value);
        }

        public int InternalContent { get; set; } = -1;

        private Font _titleFont = new Font();
        private Font _subtitleFont = new Font();

        public Font TitleFont
        {
            get => _titleFont;
            set => SetAndInvalidateIfChanged(ref _titleFont, value);
        }

        public Font SubtitleFont
        {
            get => _subtitleFont;
            set => SetAndInvalidateIfChanged(ref _subtitleFont, value);
        }

        public override bool Invalidated
        {
            get
            {
                return _invalidated | TitleFont.Invalidated | SubtitleFont.Invalidated;
            }
            set
            {
                _invalidated = value;
                TitleFont.Invalidated = value;
                SubtitleFont.Invalidated = value;
            }
        }
    }
}
