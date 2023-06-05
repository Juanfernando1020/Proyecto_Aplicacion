using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Aplicacion.Config;
using Aplicacion.Models;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Services.Interfaces;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Aplicacion.Pages.Expense.Create.ViewModel
{
    internal class ExpenseCreate : PopupViewModelBase
    {
        #region Variables

        private Routes _routeInfo;
        private readonly IGenericService<Expenses, Guid> _genericService;
        private readonly IGenericService<Basises, Guid> _genericBasisService;

        #endregion

        #region Properties

        private Expenses _expense;
        public Expenses Expense
        {
            get => _expense;
            set => SetProperty(ref _expense, value);
        }

        public ICommand CreateExpenseCommand => new AsyncCommand(CreateExpenseController);

        #endregion

        #region Methods

        private async Task CreateExpenseController()
        {
            IsBusy = true;

            if (await IsValid())
            {

            }

            IsBusy = false;
        }

        private async Task<bool> IsValid()
        {
            


            return true;
        }

        #endregion

        #region Constructor

        public ExpenseCreate()
        {
            Expense = new Expenses()
            {
                Id = Guid.NewGuid()
            };

            _genericService = GetGenericService<Expenses, Guid>();
            _genericBasisService = GetGenericService<Basises, Guid>();
        }

        #endregion

        #region Overrides

        public override async void OnInitialize(INavigationParameters parameters)
        {
            base.OnInitialize(parameters);

            await OnLoad(parameters);
        }

        private async Task OnLoad(INavigationParameters parameters)
        {
            IsBusy = true;
            if (parameters != null)
            {
                if (parameters[ArgKeys.Route] is Routes route)
                {
                    _routeInfo = route;
                }
            }
            else
            {
                await NavigationPopupService.PopPopupAsync(this);
            }
            IsBusy = false;
        }

        #endregion
    }
}
