using System;
using System.Collections.Generic;
using System.Text;

namespace WhiteMvvm.Animations.Swipe
{
    public delegate void SwipedEventHandler(ISwipeListener sender, SwipeEventArgs e);
    public interface ISwipeListener
    {
        event SwipedEventHandler SwipedDown;

        event SwipedEventHandler SwipedLeft;

        event SwipedEventHandler SwipedNothing;

        event SwipedEventHandler SwipedRight;

        event SwipedEventHandler SwipedUp;

        double TotalX
        {
            get;
        }

        double TotalY
        {
            get;
        }
    }
}
