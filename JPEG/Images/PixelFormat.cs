namespace JPEG.Images
{
    public class PixelFormat
    {
        private string Format;

        private PixelFormat(string format) => Format = format;

        public static PixelFormat RGB => new PixelFormat(nameof(RGB));
        public static PixelFormat YCbCr => new PixelFormat(nameof(YCbCr));

        private bool Equals(PixelFormat other) => string.Equals(Format, other.Format);

        public override bool Equals(object obj) => obj.GetType() == GetType() && Equals((PixelFormat) obj);

        public override int GetHashCode() => (Format != null ? Format.GetHashCode() : 0);

        public static bool operator==(PixelFormat a, PixelFormat b) => a.Equals(b);

        public static bool operator!=(PixelFormat a, PixelFormat b) => !a.Equals(b);

        public override string ToString() => Format;

        ~PixelFormat() => Format = null;
    }
}