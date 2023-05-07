using System;

namespace Aplicacion.Common.MVVM.Alerts.Messages
{
    internal class PromptMessage : BaseAlertMessage
    {
        public PromptMessage(string title, string message, string acceptButton = "Ok", string cancelButton = "Cancel", Action onAction = null) : base(title, message, acceptButton, cancelButton, onAction)
        {
        }
    }
}
