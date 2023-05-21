namespace Aplicacion.Pages.Module
{
    internal static class Pages
    {
        internal static void Initialize()
        {
            InitializeDependecyPages();
        }

        private static void InitializeDependecyPages()
        {
            Account.Module.Account.Initialize();
            Billing.Create.Module.BillingCreate.Initialize();
            Client.Module.Client.Initialize();
            Finance.Module.Finance.Initialize();
            Loan.Module.Loan.Initialize();
            Main.Module.Main.Initialize();
            User.Module.User.Initialize();
            Day.Module.Day.Initialize();
            Route.Module.Route.Initialize();
            Basis.Module.Basis.Initialize();
        }
    }
}
