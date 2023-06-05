using Xamarin.CommonToolkit.PagesBase;
using Xamarin.Forms.Xaml;

namespace Aplicacion.Pages.Finance.Expense.Create
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ExpenseCreatePage : CustomPopupBase
	{
		public ExpenseCreatePage ()
		{
			InitializeComponent ();
		}
	}
}