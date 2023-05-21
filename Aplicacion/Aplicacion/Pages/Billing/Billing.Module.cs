namespace Aplicacion.Pages.Billing.Module
{
    internal static class Billing
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
        }

        private static void InitializeDependencyPages()
        {
            Create.Module.BillingCreate.Initialize();
            Delay.Module.BillingDelay.Initialize();
            Details.Module.BillingDetails.Initialize();
            List.Module.BillingList.Initialize();
            ListPlan.Module.ListPlan.Initialize();
        }

    }
}
