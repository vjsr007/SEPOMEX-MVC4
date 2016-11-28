using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalogos.Models.Seguridad
{
    public partial class UsuarioFuncion
    {
                
        public System.Nullable<int> UsuarioID { get; set; }
        
        public System.Nullable<int> RolID { get; set; }
        
        public string Rol { get; set; }

        public System.Nullable<int> FuncionID { get; set; }

        public string Funcion { get; set; }

        public System.Nullable<int> FuncionPadreID { get; set; }

        public string FuncionPadre { get; set; }

        public string FuncionDescripcion { get; set; }

        public string Url { get; set; }

        public System.Nullable<bool> Recordarme { get; set; }

        public string Foto { get; set; }

        public string NombreCompleto { get; set; }

        public string Metadata { get; set; }

        public System.Nullable<bool> RolActivo { get; set; }

        public System.Nullable<bool> FuncionActivo { get; set; }

        public string Nombre { get; set; }

        public System.Nullable<bool> Activo { get; set; }

    }
}