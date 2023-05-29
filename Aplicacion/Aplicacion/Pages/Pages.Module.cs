using System.Reflection;
using Xamarin.CommonToolkit.Helpers;

namespace Aplicacion.Pages.Module
{
    internal interface IPages {}

    internal static class Pages
    {
        internal static void Initialize()
        {
            InitializeDependecyPages();
        }

        private static void InitializeDependecyPages()
        {
            string patternView = @"(View|Popup|Page)$";
            string patternViewModel = @"(ViewModel)$";

            //Account.Module.Account.Initialize();
            //Billing.Module.Billing.Initialize();
            //Client.Module.Client.Initialize();
            //Finance.Module.Finance.Initialize();
            //Loan.Module.Loan.Initialize();
            //Main.Module.Main.Initialize();
            //User.Module.User.Initialize();
            //Day.Module.Day.Initialize();
            //Route.Module.Route.Initialize();
            //Basis.Module.Basis.Initialize();

            ViewsManager.RegisterViews<IPages>(patternView, patternViewModel);
        }
    }
}
