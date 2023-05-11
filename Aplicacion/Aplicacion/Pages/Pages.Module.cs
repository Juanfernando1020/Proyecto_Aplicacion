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
            Main.Module.Main.Initialize();
            User.Module.User.Initialize();
        }
    }
}
