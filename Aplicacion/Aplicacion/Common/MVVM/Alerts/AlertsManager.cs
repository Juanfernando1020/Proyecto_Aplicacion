using Acr.UserDialogs;
using Aplicacion.Common.MVVM.Alerts.Messages;
using System.Threading.Tasks;

namespace Aplicacion.Common.MVVM.Alerts
{
    internal static class AlertsManager
    {
        internal static async Task ShowAlert(BaseAlertMessage alertMessage)
        {
            AlertConfig alertConfig = new AlertConfig()
            {
                Title = alertMessage.Title,
                Message = alertMessage.Message,
                OkText = alertMessage.AcceptButton,
                OnAction = alertMessage.OnAction,
            };

            await UserDialogs.Instance.AlertAsync(alertConfig);
        }
        internal static async Task<bool> ShowConfirmAlert(ConfirmationMessage alertMessage)
        {
            ConfirmConfig alertConfig = new ConfirmConfig()
            {
                Title = alertMessage.Title,
                Message = alertMessage.Message,
                OkText = alertMessage.AcceptButton,
                CancelText = alertMessage.CancelButton,
            };

            return await UserDialogs.Instance.ConfirmAsync(alertConfig);
        }
        internal static async Task<string> ShowPrompt(PromptMessage promptMessage)
        {
            PromptConfig promptConfig = new PromptConfig()
            {
                Title = promptMessage.Title,
                Message = promptMessage.Message,
                OkText = promptMessage.AcceptButton,
                CancelText = promptMessage.CancelButton,
            };

            PromptResult result = await UserDialogs.Instance.PromptAsync(promptConfig);

            return result.Ok ? result.Value : null;
        }
    }
}
