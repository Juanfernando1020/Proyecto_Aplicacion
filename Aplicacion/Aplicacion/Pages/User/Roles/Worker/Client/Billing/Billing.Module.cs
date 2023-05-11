namespace Aplicacion.Pages.User.Roles.Worker.Client.Billing.Module
{
    internal static class Billing
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
        }

        private static void InitializeDependencyPages()
        {
            Details.Module.BillingDetails.Initialize();
            Create.Module.BillingCreate.Initialize();
            Delay.Module.BillingDelay.Initialize();
            List.Module.BillingList.Initialize();
        }

    }
}
