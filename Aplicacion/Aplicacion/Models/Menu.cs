using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Page { get; set; }
        public int PageType { get; set; }
        public int Role { get; set; }
    }
}
