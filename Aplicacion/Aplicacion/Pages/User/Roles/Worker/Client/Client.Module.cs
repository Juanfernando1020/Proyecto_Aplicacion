namespace Aplicacion.Pages.User.Roles.Worker.Client.Module
{
    internal static class Client
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
        }

        private static void InitializeDependencyPages()
        {
            Billing.Module.Billing.Initialize();
            Create.Module.ClientCreate.Initialize();
            Details.Module.ClientDetails.Initialize();
            List.Module.ClientList.Initialize();
            Loan.Module.Loan.Initialize();
        }
    }
}
