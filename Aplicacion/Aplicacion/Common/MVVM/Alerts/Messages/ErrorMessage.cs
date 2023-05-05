using System;

namespace Aplicacion.Common.MVVM.Alerts.Messages
{
    internal class ErrorMessage : BaseAlertMessage
    {
        public ErrorMessage(string message, string title = "Error", string acceptButton = "Ok", string cancelButton = null, Action onAction = null) : 
            base(title, message, acceptButton, cancelButton, onAction)
        {
        }
    }
}
