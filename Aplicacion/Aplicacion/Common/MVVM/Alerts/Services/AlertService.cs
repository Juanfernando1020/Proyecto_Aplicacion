using Acr.UserDialogs;
using Aplicacion.Common.MVVM.Alerts.Interfaces;
using Aplicacion.Common.MVVM.Alerts.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Common.MVVM.Alerts.Services
{
    internal class AlertService : IAlertService
    {
        public async Task ShowAlert(BaseAlertMessage alertMessage)
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

        public async Task<bool> ShowConfirmAlert(ConfirmationMessage alertMessage)
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

        public async Task<string> ShowPrompt(PromptMessage promptMessage)
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
