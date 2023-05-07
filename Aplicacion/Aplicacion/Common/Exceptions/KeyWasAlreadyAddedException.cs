using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Common.Exceptions
{
    public class KeyWasAlreadyAddedException : Exception
    {
        public KeyWasAlreadyAddedException(string key) : base($"The key {key} was already added.")
        {

        }
    }
}
