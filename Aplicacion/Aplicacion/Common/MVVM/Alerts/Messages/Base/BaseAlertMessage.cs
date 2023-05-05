﻿using System;

namespace Aplicacion.Common.MVVM.Alerts.Messages
{
    internal abstract class BaseAlertMessage
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string AcceptButton { get; set; }
        public string CancelButton { get; set; }
        public Action OnAction { get; set; }

        internal BaseAlertMessage(string title, string message, string acceptButton, string cancelButton, Action onAction)
        {
            Title = title;
            Message = message;
            AcceptButton = acceptButton;
            CancelButton = cancelButton;
            OnAction = onAction;
        }
    }
}
