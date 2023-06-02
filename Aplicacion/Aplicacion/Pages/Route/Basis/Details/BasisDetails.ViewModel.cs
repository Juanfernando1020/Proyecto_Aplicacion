using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Aplicacion.Config;
using Aplicacion.Config.Messages;
using Aplicacion.Models;
using Aplicacion.Pages.Route.Basis.Specifications;
using Aplicacion.Pages.Route.Channels;
using Aplicacion.Pages.Route.Config;
using Aplicacion.Pages.User.Enums;
using Xamarin.CommonToolkit.Mvvm.Alerts.Messages;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.CommonToolkit.Mvvm.Services.Interfaces;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.CommonToolkit.Result;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Aplicacion.Pages.Route.Basis.Details.ViewModel
{
    internal class BasisDetails : ViewModelBase, IRouteBudgetsChangedChannel
    {
        #region Variables
        private readonly IGenericService<Basises, Guid> _genericService;
        private readonly IGenericService<Routes, Guid> _genericRouteService;
        private Users _userInfo;
        private Routes _routeInfo;
        private decimal _total;
        #endregion

        #region Properties
        private bool _canCreate;
        public bool CanCreate
        {
            get => _canCreate;
            set => SetProperty(ref _canCreate, value);
        }
        
        private decimal _amount;
        public decimal Amount
        {
            get => _amount;
            set => SetProperty(ref _amount, value);
        }
        
        private decimal _availableBudget;
        public decimal AvailableBudget
        {
            get => _availableBudget;
            set => SetProperty(ref _availableBudget, value);
        }
        
        private Basises _basis;
        public Basises Basis
        {
            get => _basis;
            set => SetProperty(ref _basis, value);
        }

        public ICommand CreateBasisCommand => new AsyncCommand(CreateBasisController);

        #endregion

        #region Methods

        private async Task CreateBasisController()
        {
            List<Budgets> budgetList = _routeInfo.Budgets.ToList();
            IsBusy = true;

            Basis.Date = DateTime.Now;
            ResultBase result = await _genericService.InsertAsync(Basis);

            if (result.IsSuccess)
            {
                Budgets newBudget = new Budgets()
                {
                    Id = Guid.NewGuid(),
                    Admin = _userInfo,
                    Amount = -Amount
                };

                budgetList.Add(newBudget);
                INavigationParameters parameters = new NavigationParameters();
                parameters.Add(ArgKeys.Budget, newBudget);

                _routeInfo.Budgets = budgetList.ToArray();
                ResultBase routeResult = await _genericRouteService.UpdateAsync(_routeInfo.Id, _routeInfo);

                if (routeResult.IsSuccess)
                {
                    MessagingCenter.Send<IRouteBudgetsChangedChannel, INavigationParameters>(this, nameof(IRouteBudgetsChangedChannel), parameters);
                    await AlertService.ShowAlert(new SuccessMessage(CommonMessages.Success.InformationMessage));
                    await NavigationService.PopAsync();
                }
                else
                {
                    await _genericService.DeleteAsync(Basis.Id);
                    await AlertService.ShowAlert(new ErrorMessage(routeResult.Message));
                }
            }
            else
            {
                await AlertService.ShowAlert(new ErrorMessage(result.Message));
            }

            IsBusy = false;
        }

        #endregion

        #region Constructor

        public BasisDetails()
        {
            CanCreate = false;
            Basis = new Basises()
            {
                Date = DateTime.Now
            };
            _genericService = GetGenericService<Basises, Guid>();
            _genericRouteService = GetGenericService<Routes, Guid>();
        }

        #endregion

        #region Overrides
        public override async void OnInitialize(INavigationParameters parameters)
        {
            base.OnInitialize(parameters);
            await OnLoad(parameters);
        }

        public override void CallBack(INavigationParameters parameters)
        {
            base.CallBack(parameters);
            OnCallBack(parameters);
        }

        protected override async void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(Amount):
                    if (Amount < RoutesConfig.MIN_AMOUNT)
                    {
                        Amount = RoutesConfig.MIN_AMOUNT;
                        await AlertService.ShowAlert(new WarningMessage($"Debes elegir una cantidad superior a {RoutesConfig.MIN_AMOUNT}"));
                    }
                    else if (Amount > _total)
                    {
                        Amount = _total;
                        await AlertService.ShowAlert(new WarningMessage($"No puedes elegir una cantidad superior del presupuesto de la ruta."));
                    }
                    else
                    {
                        Basis.Amount = Amount;
                        AvailableBudget = _total - Amount;
                    }
                    break;
            }
        }

        #endregion

        #region OnLoad

        private async Task OnLoad(INavigationParameters parameters)
        {
            if (Aplicacion.Module.App.UserInfo is Users user)
            {
                _userInfo = user;
            }

            if (parameters != null)
            {
                if (parameters[ArgKeys.Route] is Routes route)
                {
                    _routeInfo = route;
                    ResultBase<Basises> result = await _genericService.GetBySpecificacionAsync(new BasisByRouteIdAndDateNowSpecification(route.Id));

                    if (result.IsSuccess)
                    {
                        if (result.Data is Basises basis)
                        {
                            Basis = basis;
                        }
                        else
                        {
                            await ShowErrorInit(CommonMessages.Console.NullDataWhenIsSuccess);
                        }
                    }
                    else
                    {
                        if (_userInfo.Role == (int)RolesEnum.Admin)
                        {
                            Basis.Route = route.Id;
                            CanCreate = true;
                        }
                        else
                        {
                            await NavigationService.PopAsync();
                        }

                        await AlertService.ShowAlert(new InfoMessage("No se ha encontrado una base para el día de hoy."));
                    }

                    _total = 0;
                    foreach (Budgets budget in route.Budgets)
                    {
                        _total += budget.Amount;
                    }
                    AvailableBudget = _total;
                }
                else
                {
                    await ShowErrorInit(String.Format(CommonMessages.Console.MissingKey, ArgKeys.Route));
                }
            }
            else
            {
                await ShowErrorInit(String.Format(CommonMessages.Console.NullKey, nameof(parameters)));
            }
        }

        private async Task ShowErrorInit(string consoleError)
        {
            Console.WriteLine(consoleError);
            await AlertService.ShowAlert(new InfoMessage(CommonMessages.Error.InformationMessage));
            await NavigationService.PopAsync();
        }
        #endregion

        #region OnCallBack

        private void OnCallBack(INavigationParameters parameters)
        {
            if (parameters != null)
            {
                if (parameters[ArgKeys.Basis] is Basises basis)
                {
                    Basis = basis;
                }
            }
        }

        #endregion
    }
}
