using Newtonsoft.Json;
using System;

namespace Aplicacion.Common.MVVM.Navigation.Models
{
    public class NavigationResult
    {
        public bool IsNavigated { get; set; }
        public Exception Exception { get; set; }

        public NavigationResult(bool isNavigated, Exception exception = null)
        {
            IsNavigated = isNavigated;
            Exception = exception;
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
