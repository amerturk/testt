using Xamarin.Forms;

namespace WhiteMvvm.Animations
{
    public class Animation
    {
        public static readonly BindableProperty TranslateProperty =
            BindableProperty.CreateAttached(
                propertyName: "Translate",
                returnType: typeof(TranslateParameters),
                declaringType: typeof(View),
                defaultValue: new TranslateParameters(0,0,0),
                defaultBindingMode: BindingMode.OneWay,
                validateValue: null,
                propertyChanged: OnTranslateChanged);

        private static void OnTranslateChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is View view)|| newvalue == null)
                return;
            
            var translateDetails = (TranslateParameters) newvalue;
            
            Device.BeginInvokeOnMainThread(async () =>
            {
                await view.TranslateTo(translateDetails.X, translateDetails.Y, length: translateDetails.Length,translateDetails.Easing);
            });                        
        }


        public static TranslateParameters GetTranslate(BindableObject bindable)
        {
            return (TranslateParameters)bindable.GetValue(TranslateProperty);
        }

        public static void SetTranslate(BindableObject bindable, TranslateParameters value)
        {
            bindable.SetValue(TranslateProperty, value);
        }
    }
}
