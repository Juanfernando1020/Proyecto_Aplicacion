using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aplicacion.Views.Shared
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IconPopupOpenerView : ContentView
    {
        #region Bindable Properties
        public static readonly BindableProperty IconProperty = BindableProperty.Create(
            nameof(Icon),
            typeof(string),
            typeof(IconPopupOpenerView),
            string.Empty,
            propertyChanged: IconPropertyChanged
            );

        public static readonly BindableProperty LabelProperty = BindableProperty.Create(
            nameof(Label),
            typeof(string),
            typeof(IconPopupOpenerView),
            string.Empty,
            propertyChanged: LabelPropertyChanged
            );

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(IconPopupOpenerView),
            string.Empty,
            propertyChanged: TextPropertyChanged
            );
        
        public new static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(
            nameof(IsEnabled),
            typeof(bool),
            typeof(IconPopupOpenerView),
            true
            );
        
        public new static readonly BindableProperty OpenPopupCommandProperty = BindableProperty.Create(
            nameof(OpenPopupCommand),
            typeof(ICommand),
            typeof(IconPopupOpenerView),
            default,
            propertyChanged: OpenPopupCommandPropertyChanged
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
        public ICommand OpenPopupCommand
        { 
            get => (ICommand)GetValue(OpenPopupCommandProperty); 
            set => SetValue(OpenPopupCommandProperty, value); 
        }
        #endregion

        #region Methods
        private static void IconPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((IconPopupOpenerView)bindable).iconEntry.Source = (string)newValue;
        }
        private static void LabelPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((IconPopupOpenerView)bindable).labelEntry.Text = (string)newValue;
        }
        private static void TextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((IconPopupOpenerView)bindable).value.Text = (string)newValue;
        }
        private static void OpenPopupCommandPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((IconPopupOpenerView)bindable).chevron.GestureRecognizers.Clear();

            TapGestureRecognizer recognizer = new TapGestureRecognizer();
            recognizer.Command = (ICommand)newvalue;

            ((IconPopupOpenerView)bindable).chevron.GestureRecognizers.Add(recognizer);
        }
        #endregion

        public IconPopupOpenerView()
        {
            InitializeComponent();
        }
    }
}