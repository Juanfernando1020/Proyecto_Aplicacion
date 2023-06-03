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
            internal const string InformationMessage = "No se puede realizar esta operación ahora mismo. Intentalo más tarde.";
        }

        internal static class Warning
        {
            internal const string Unavailable = "No es posible hacer uso de esta funcionalidad. Intentalo más tarde.";
            internal const string Unsupported = "Tu dispositivo no soporta esta funcionalidad.";
        }

        internal static class Success
        {
            internal const string Create = "Dato agregado correctamente.";
        }

        internal static class Console
        {
            internal const string NullDataWhenIsSuccess = "The data is null when the service returns success.";
            internal const string NullKey = "'{0}' cannot be null.";
            internal const string MissingKey = "It's missing the '{0}' key.";
        }

        internal static class Form
        {
            internal const string NullOrEmptyInfo = "Aún faltan campos por rellenar.";
        }
    }
}
