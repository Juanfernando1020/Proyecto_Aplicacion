using System;
using Xamarin.CommonToolkit.PagesBase;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aplicacion.Views.Shared.Entries
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IconEntryView : ContentView
    {
        #region Bindable Properties
        public static BindableProperty IconProperty = BindableProperty.Create(
            nameof(Icon),
            typeof(string),
            typeof(IconEntryView),
            string.Empty,
            propertyChanged: IconPropertyChanged
            );

        public static BindableProperty LabelProperty = BindableProperty.Create(
            nameof(Label),
            typeof(string),
            typeof(IconEntryView),
            string.Empty,
            propertyChanged: LabelPropertyChanged
            );

        public static BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(IconEntryView),
            string.Empty,
            BindingMode.TwoWay,
            propertyChanged: TextPropertyChanged
            );
        
        public static BindableProperty IsEnabledProperty = BindableProperty.Create(
            nameof(IsEnabled),
            typeof(bool),
            typeof(IconEntryView),
            true
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
        public bool IsEnabled
        { 
            get => (bool)GetValue(IsEnabledProperty); 
            set => SetValue(IsEnabledProperty, value); 
        }
        #endregion

        #region Methods
        private static void IconPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((IconEntryView)bindable).iconEntry.Source = (string)newValue;
        }
        private static void LabelPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((IconEntryView)bindable).labelEntry.Text = (string)newValue;
        }
        private static void TextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((IconEntryView)bindable).entry.Text = (string)newValue;
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