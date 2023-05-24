using System.Collections;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aplicacion.Views.Shared
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [ContentProperty("Picker")]
    public partial class IconPickerView : ContentView
    {
        #region Bindable Properties
        public static readonly BindableProperty IconProperty = BindableProperty.Create(
            nameof(Icon),
            typeof(string),
            typeof(IconPickerView),
            string.Empty,
            propertyChanged: IconPropertyChanged
            );

        public static readonly BindableProperty LabelProperty = BindableProperty.Create(
            nameof(Label),
            typeof(string),
            typeof(IconPickerView),
            string.Empty,
            propertyChanged: LabelPropertyChanged
            );

        public new static readonly BindableProperty PickerProperty = BindableProperty.Create(
            nameof(Picker),
            typeof(Picker),
            typeof(IconPickerView),
            default,
            propertyChanged: OnPickerPropertyChanged
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
        public Picker Picker
        {
            get => (Picker)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }
        #endregion

        #region Methods
        private static void IconPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((IconPickerView)bindable).iconEntry.Source = (string)newValue;
        }
        private static void LabelPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((IconPickerView)bindable).labelEntry.Text = (string)newValue;
        }
        private static void OnPickerPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((IconPickerView)bindable).contentView.Content = (Picker)newValue;
        }
        #endregion

        public IconPickerView()
        {
            InitializeComponent();
        }
    }
}