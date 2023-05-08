using System;

namespace Aplicacion.Common.MVVM.Alerts.Messages
{
    public class WarningMessage : BaseAlertMessage
    {
        public WarningMessage(string message, string title = "Warning", string acceptButton = "Ok", string cancelButton = null, Action onAction = null) : 
            base(title, message, acceptButton, cancelButton, onAction)
        {
        }
    }
}
