namespace Aplicacion.Config.Messages
{
    internal static class CommonMessages
    {
        internal static class Exception
        {
            internal const string ResultMessage = "No se puede realizar esta operación ahora mismo. Intentalo más tarde.";
        }

        internal static class Error
        {
            internal const string InformationMessage =
                "No se puede realizar esta operación ahora mismo. Intentalo más tarde.";
        }

        internal static class Success
        {
            internal const string InformationMessage = "Dato agregado correctamente.";
        }

        internal static class Console
        {
            internal const string NullKey = "'{0}' cannot be null.";
            internal const string MissingKey = "It's missing the '{0}' key.";
        }

        internal static class Form
        {
            internal const string NullOrEmptyInfo = "Aún faltan campos por rellenar.";
        }
    }
}
