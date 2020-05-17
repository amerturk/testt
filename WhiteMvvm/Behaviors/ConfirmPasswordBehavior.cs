using Xamarin.Forms;

namespace WhiteMvvm.Behaviors
{
    public class ConfirmPasswordBehavior : Behavior<Entry>
    {
        private static readonly BindablePropertyKey IsNotValidPropertyKey =
            BindableProperty.CreateReadOnly("IsNotValid",
                typeof(bool), typeof(ConfirmPasswordBehavior), false);
        public static readonly BindableProperty IsNotValidProperty = IsNotValidPropertyKey.BindableProperty;

        public static readonly BindableProperty CompareToTextProperty =
            BindableProperty.Create("CompareToText", typeof(string),
                typeof(ConfirmPasswordBehavior), string.Empty);

        public string CompareToText
        {
            get => (string)base.GetValue(CompareToTextProperty);
            set => base.SetValue(CompareToTextProperty, value);
        }
        public bool IsNotValid
        {
            get => (bool)base.GetValue(IsNotValidProperty);
            private set => base.SetValue(IsNotValidPropertyKey, value);
        }
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
            base.OnAttachedTo(bindable);
        }
        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(bindable);
        }

        private void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            string password = CompareToText;
            string confirmPassword = e.NewTextValue;
            if (string.IsNullOrEmpty(password))
            {
                IsNotValid = false;
                return;
            }
            IsNotValid = !password.Equals(confirmPassword);
        }
    }
}
