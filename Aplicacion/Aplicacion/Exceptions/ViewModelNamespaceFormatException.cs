using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Exceptions
{
    internal class ViewModelNamespaceFormatException : Exception
    {
        public ViewModelNamespaceFormatException(string viewModel) : base($"The view model {viewModel} don't have the format of the view model namespace. Remember add 'ViewModel' at the final of the namespace.")
        {
            
        }
    }
}
