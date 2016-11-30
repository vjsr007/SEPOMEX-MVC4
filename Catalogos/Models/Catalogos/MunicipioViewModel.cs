using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalogos.Models.Catalogos
{
    public class MunicipioViewModel
    {
        public int MunicipioID { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> EstadoID { get; set; }
        public Nullable<System.DateTime> FechaUltimaModificacion { get; set; }
        public Nullable<int> UsuarioID { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> PaisID { get; set; }
    }
}