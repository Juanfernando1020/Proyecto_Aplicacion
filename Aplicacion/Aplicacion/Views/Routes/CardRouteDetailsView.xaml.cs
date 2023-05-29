using Aplicacion.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aplicacion.Views.Routes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardRouteDetailsView : ContentView
    {
        #region Bindable Properties
        public static readonly BindableProperty RouteProperty = BindableProperty.Create(
            nameof(Route),
            typeof(Models.Routes), 
            typeof(CardRouteDetailsView),
            default,
            propertyChanged: OnRoutePropertyChanged
            );
        #endregion

        #region Properties
        public Models.Routes Route
        {
            get => (Models.Routes)GetValue(RouteProperty);
            set => SetValue(RouteProperty, value);
        }
        #endregion

        #region Methods
        private static void OnRoutePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            CardRouteDetailsView control = (CardRouteDetailsView)bindable;
            Models.Routes route = (Models.Routes)newvalue;

            if (route != null)
            {
                control.name.Text = route.Name;
                control.zone.Text = route.Zone;
                control.worker.Text = route.Worker.Name;
                control.manager.Text = route.Manager.Name;

                decimal total = 0;

                if (route.Budgets != null)
                {
                    foreach (Budgets budget in route.Budgets)
                    {
                        total += budget.Amount;
                    }
                }

                control.budget.Text = string.Format(Application.Current.Resources["FormatMoney"] as string ?? "{0}", total);
            }
        }
        #endregion

        public CardRouteDetailsView()
        {
            InitializeComponent();
        }
    }
}