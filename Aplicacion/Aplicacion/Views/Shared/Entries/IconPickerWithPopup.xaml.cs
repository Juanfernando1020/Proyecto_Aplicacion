using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aplicacion.Views.Shared.Entries
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IconPickerWithPopup : ContentView
    {
        #region Bindable Properties
        public static readonly BindableProperty IconProperty = BindableProperty.Create(
            nameof(Icon),
            typeof(string),
            typeof(IconPickerWithPopup),
            string.Empty
        );

        public static readonly BindableProperty LabelProperty = BindableProperty.Create(
            nameof(Label),
            typeof(string),
            typeof(IconPickerWithPopup),
            string.Empty
        );

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(IconPickerWithPopup),
            string.Empty
        );
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command),
            typeof(ICommand),
            typeof(IconPickerWithPopup),
            null,
            propertyChanged: OnCommandPropertyChanged
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
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
        #endregion

        #region Methods
        private static void OnCommandPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((IconPickerWithPopup)bindable).chevron.GestureRecognizers.Clear();

            TapGestureRecognizer recognizer = new TapGestureRecognizer((view) => 
                ((ICommand)newvalue).Execute(0));

            ((IconPickerWithPopup)bindable).chevron.GestureRecognizers.Add(recognizer);
        }
        #endregion

        public IconPickerWithPopup()
        {
            InitializeComponent();
        }
    }
}