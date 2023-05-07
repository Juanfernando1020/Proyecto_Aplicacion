using System.Linq;

namespace Aplicacion.Common.Utils
{
    public class MultipleValuesVerification
    {
        public static bool AllTrueVerification(params bool[] parameters)
        {
            return !parameters.Any(parameter => !parameter);
        }
    }
}
