namespace Mapsui.UI.Forms
{
    public enum PinType
    {
        /// <summary>
        /// invalid pin
        /// type the purpose of this is to avoid extra feature and styles creation
        /// during pin constructor if we use initializer expression
        /// </summary>
        None = 0,

        /// <summary>
        /// Pin with image, which could change color
        /// </summary>
        Pin,

        /// <summary>
        /// Pin as icon image
        /// </summary>
        Icon,

        /// <summary>
        /// Pin as Svg image
        /// </summary>
        Svg
    }
}
