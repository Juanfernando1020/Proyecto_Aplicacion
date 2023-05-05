using System;

namespace Aplicacion.Common.MVVM.Alerts.Messages
{
    internal class InfoMessage : BaseAlertMessage
    {
        public InfoMessage(string message, string title = "Info", string acceptButton = "Ok", string cancelButton = null, Action onAction = null) : 
            base(title, message, acceptButton, cancelButton, onAction)
        {
        }
    }
}
