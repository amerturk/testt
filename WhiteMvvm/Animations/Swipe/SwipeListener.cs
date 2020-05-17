using Xamarin.Forms;

namespace WhiteMvvm.Animations.Swipe
{
    public class SwipeListener : PanGestureRecognizer, ISwipeListener
    {
        public event SwipedEventHandler SwipedDown;

        public event SwipedEventHandler SwipedLeft;

        public event SwipedEventHandler SwipedNothing;

        public event SwipedEventHandler SwipedRight;

        public event SwipedEventHandler SwipedUp;

        double _totalX = 0, _totalY = 0;

        public double TotalX => _totalX;

        public double TotalY => _totalY;

        readonly View _view;

        public SwipeListener(View view) : base()
        {
            _view = view;
            _view.GestureRecognizers.Add(this);
            PanUpdated += OnPanUpdated;
        }

        void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    _totalX = e.TotalX;
                    _totalY = e.TotalY;
                    if (_totalX < 0)
                    {
                        OnSwipedLeft(_totalX, _totalY);
                    }
                    else if (_totalX > 0)
                    {
                        OnSwipedRight(_totalX, _totalY);
                    }
                    if (_totalY < 0)
                    {
                        OnSwipedDown(_totalX, _totalY);
                    }
                    else if (_totalY > 0)
                    {
                        OnSwipedUp(_totalX, _totalY);
                    }
                    break;
                case GestureStatus.Completed:
                    if (_totalX < 0)
                    {
                        OnSwipedLeft(_totalX, _totalY);
                    }
                    else if (_totalX > 0)
                    {
                        OnSwipedRight(_totalX, _totalY);
                    }
                    if (_totalY < 0)
                    {
                        OnSwipedDown(_totalX, _totalY);
                    }
                    else if (_totalY > 0)
                    {
                        OnSwipedUp(_totalX, _totalY);
                    }
                    break;
            }
        }
        protected virtual void OnSwipedDown(double x, double y)
        {
            SwipedDown?.Invoke(this, new SwipeEventArgs(_view, x, y));
        }
        protected virtual void OnSwipedLeft(double x, double y)
        {
            SwipedLeft?.Invoke(this, new SwipeEventArgs(_view, x, y));
        }
        protected virtual void OnSwipedNothing(double x, double y)
        {
            SwipedNothing?.Invoke(this, new SwipeEventArgs(_view, x, y));
        }
        protected virtual void OnSwipedRight(double x, double y)
        {
            SwipedRight?.Invoke(this, new SwipeEventArgs(_view, x, y));
        }
        protected virtual void OnSwipedUp(double x, double y)
        {
            SwipedUp?.Invoke(this, new SwipeEventArgs(_view, x, y));
        }
    }

}
