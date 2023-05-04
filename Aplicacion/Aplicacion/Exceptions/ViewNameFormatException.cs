using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Exceptions
{
    internal class ViewNameFormatException : Exception
    {
        public ViewNameFormatException(string view) : base($"The view {view} don't have the format of the view names. Remember add 'Page' at the final.")
        {
            
        }
    }
}
