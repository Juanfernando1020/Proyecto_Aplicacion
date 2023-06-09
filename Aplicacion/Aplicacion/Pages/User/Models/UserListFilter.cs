using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Pages.User.Models
{
    internal class UserListFilter
    {
        public string Name { get; set; }
        public UserListFilterType Filter { get; set; }
    }

    internal enum UserListFilterType
    {
        All = 0,
        ExpiredWorker = 1,
        InactiveWorker = 2,
        Today = 3,
        Admin = 4,
    }
}