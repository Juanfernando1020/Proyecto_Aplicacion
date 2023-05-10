using System.Linq;

namespace Aplicacion.Common.Utils
{
    public class MultipleValuesValidation
    {
        public static bool AllTrueValidation(params bool[] parameters)
        {
            return !parameters.Any(parameter => !parameter);
        }
    }
}
