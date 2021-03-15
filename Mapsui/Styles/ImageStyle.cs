
namespace Mapsui.Styles
{
    public enum UnitType
    {
        Pixel,
        WorldUnit
    }

    public class ImageStyle : VectorStyle
    {
        public UnitType UnitType { get; set; }
        private double _rotation;
        private double _scale = 1.0;
        private bool _rotateWithMap;
        private Offset _offset = new Offset();


        /// <summary>
        ///     Gets or sets the rotation of the symbol in degrees (clockwise is positive)
        /// </summary>
        public double SymbolRotation
        {
            get => _rotation;
            set => SetAndInvalidateIfChanged(ref _rotation, value);
        }

        /// <summary>
        ///     This identifies bitmap in the BitmapRegistry.
        /// </summary>
        public int BitmapId { get; set; } = -1;

        /// <summary>
        /// When true a symbol will rotate along with the rotation of the map.
        /// This is useful if you need to symbolize the direction in which a vehicle
        /// is moving. When the symbol is false it will retain it's position to the
        /// screen. This is useful for pins like symbols. The default is false.
        /// This mode is not supported in the WPF renderer.
        /// </summary>
        public bool RotateWithMap
        {
            get => _rotateWithMap;
            set => SetAndInvalidateIfChanged(ref _rotateWithMap, value);
        }

        /// <summary>
        ///     Scale of the symbol (defaults to 1)
        /// </summary>
        /// <remarks>
        ///     Setting the symbolscale to '2.0' doubles the size of the symbol, where a scale of 0.5 makes the scale half the size
        ///     of the original image
        /// </remarks>
        public double SymbolScale
        {
            get => _scale;
            set => SetAndInvalidateIfChanged(ref _scale, value);
        }

        /// <summary>
        ///     Gets or sets the offset in pixels of the symbol.
        /// </summary>
        /// <remarks>
        ///     The symbol offset is scaled with the <see cref="SymbolScale" /> property and refers to the offset of
        ///     <see cref="SymbolScale" />=1.0.
        /// </remarks>
        public Offset SymbolOffset
        {
            get => _offset;
            set => SetAndInvalidateIfChanged(ref _offset, value);
        }
    }
}
