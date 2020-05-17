using Xamarin.Forms;

namespace WhiteMvvm.Animations.Swipe
{
    public class SwipeEventArgs : System.EventArgs
    {
        public double X { get; }
        public double Y { get; }
        public View View { get; }

        public SwipeEventArgs(View view, double x, double y)
        {
            View = view;
            X = x;
            Y = y;
        }
    }
}
