using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Signbook.Controls
{
    public class ExtSwitch : Switch
    {
        public delegate void ToggledDroidHandler(object sender, ToggledEventArgs e);
        public event ToggledDroidHandler ToggledDroid;

        public virtual void OnToggledDroid(object sender, ToggledEventArgs e)
        {
            ToggledDroid?.Invoke(this, e);
        }
    }
}
