using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aplicacion.Pages.Worker.AddNewExpense
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddNewExpensePage : ContentPage
	{
		public AddNewExpensePage ()
		{
			InitializeComponent ();
		}

        private void Add_New_Expense_Botton_Clicked(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }
    }
}