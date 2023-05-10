using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Models
{
    internal class Routes_Users
    {
        public Routes_Users(Guid fk_Route, Guid fk_Users, bool isManager, decimal @base, bool isActive)
        {
            this.fk_Route = fk_Route;
            this.fk_Users = fk_Users;
            IsManager = isManager;
            Base = @base;
            IsActive = isActive;
        }

        public Guid fk_Route { get; set; }
        public Guid fk_Users { get; set; }
        public Boolean IsManager { get; set; }
        public decimal Base { get; set; }
        public bool IsActive { get; set; }

        public DateTime AuditCreationDate { get; set; }
        public DateTime AuditUpdateDate { get; set; }

    }
}
