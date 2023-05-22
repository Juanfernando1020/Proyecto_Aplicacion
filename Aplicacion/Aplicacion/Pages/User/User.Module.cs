namespace Aplicacion.Pages.User.Module
{
    internal static class User
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
        }

        private static void InitializeDependencyPages()
        {
            Create.Module.UserCreate.Initialize();
            Details.Module.UserDetails.Initialize();
            List.Module.UserList.Initialize();
            Views.Module.Views.Initialize();
        }
    }
}
