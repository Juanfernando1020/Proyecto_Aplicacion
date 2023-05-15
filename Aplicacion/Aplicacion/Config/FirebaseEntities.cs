namespace Aplicacion.Config
{
    internal static class FirebaseEntities
    {
        internal static string Users => nameof(Models.Users);
        internal static string Routes_Users => nameof(Models.Routes_Users);
        internal static string Routes => nameof(Models.Routes);
        internal static string Loans => nameof(Models.Loans);
        internal static string Expenses => nameof(Models.Expenses);
        internal static string Companies => nameof(Models.Companies);
        internal static string Clients => nameof(Models.Clients);
        internal static string Branches => nameof(Models.Branches);
        internal static string Billings => nameof(Models.Billings);

        internal static string Budgets = nameof(Models.Budgets);
    }
}
