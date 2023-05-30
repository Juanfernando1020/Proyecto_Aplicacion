using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aplicacion.Views.Shared
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IconEntryView : ContentView
    {
        #region Bindable Properties
        public static readonly BindableProperty IconProperty = BindableProperty.Create(
            nameof(Icon),
            typeof(string),
            typeof(IconEntryView),
            string.Empty,
            propertyChanged: IconPropertyChanged
            );

        public static readonly BindableProperty LabelProperty = BindableProperty.Create(
            nameof(Label),
            typeof(string),
            typeof(IconEntryView),
            string.Empty,
            propertyChanged: LabelPropertyChanged
            );

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(IconEntryView),
            string.Empty,
            BindingMode.TwoWay,
            propertyChanged: TextPropertyChanged
            );
        
        public new static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(
            nameof(IsEnabled),
            typeof(bool),
            typeof(IconEntryView),
            true,
            propertyChanged: IsEnabledPropertyChanged
            );
        
        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(
            nameof(Keyboard),
            typeof(Keyboard),
            typeof(IconEntryView),
            Keyboard.Text,
            propertyChanged: KeyboardPropertyChanged
            );
        #endregion

        #region Properties
        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }
        public string Label
        { 
            get => (string)GetValue(LabelProperty); 
            set => SetValue(LabelProperty, value); 
        }
        public string Text
        { 
            get => (string)GetValue(TextProperty); 
            set => SetValue(TextProperty, value); 
        }
        public new bool IsEnabled
        { 
            get => (bool)GetValue(IsEnabledProperty); 
            set => SetValue(IsEnabledProperty, value); 
        }
        public Keyboard Keyboard
        { 
            get => (Keyboard)GetValue(KeyboardProperty); 
            set => SetValue(KeyboardProperty, value); 
        }


        #endregion

        #region Methods
        private static void IconPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((IconEntryView)bindable).iconEntry.Text = (string)newValue;
        }

        private static void LabelPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((IconEntryView)bindable).labelEntry.Text = (string)newValue;
        }
        private static void TextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((IconEntryView)bindable).entry.Text = (string)newValue;
        }
        private static void IsEnabledPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((IconEntryView)bindable).entry.IsEnabled = (bool)newValue;
        }
        private static void KeyboardPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((IconEntryView)bindable).entry.Keyboard = (Keyboard)newValue;
        }
        #endregion

        public IconEntryView()
        {
            InitializeComponent();
        }

        private void entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Text = e.NewTextValue;
        }
    }
}