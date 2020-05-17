using Xamarin.Forms;

namespace WhiteMvvm.Animations
{
    public class TranslateParameters
    {
        public Easing Easing { get; }
        public double X { get; }
        public double Y { get; }
        public uint Length { get; }
        public TranslateParameters(double x, double y, uint length , Easing easing = null)
        {
            Easing = easing;
            X = x;
            Y = y;
            Length = length;
        }
    }
}
