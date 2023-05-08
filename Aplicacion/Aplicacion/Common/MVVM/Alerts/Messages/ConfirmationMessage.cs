using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Common.MVVM.Alerts.Messages
{
    public class ConfirmationMessage : BaseAlertMessage
    {
        public ConfirmationMessage(string message, string title = "Confirmation", string acceptButton = "Ok", string cancelButton = "Cancel", Action onAction = null) : 
            base(title, message, acceptButton, cancelButton, onAction)
        {
        }
    }
}
