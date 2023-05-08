using Aplicacion.Common.MVVM.Alerts.Messages;
using System.Threading.Tasks;

namespace Aplicacion.Common.MVVM.Alerts.Interfaces
{
    public interface IAlertService
    {
        Task ShowAlert(BaseAlertMessage alertMessage);
        Task<bool> ShowConfirmAlert(ConfirmationMessage alertMessage);
        Task<string> ShowPrompt(PromptMessage promptMessage);
    }
}
