using System;

namespace Aplicacion.Common.MVVM.Alerts.Messages
{
    public class SuccessMessage : BaseAlertMessage
    {
        public SuccessMessage(string message, string title = "Success!", string acceptButton = "Ok", string cancelButton = null, Action onAction = null) : 
            base(title, message, acceptButton, cancelButton, onAction)
        {
        }
    }
}
