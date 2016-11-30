using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalogos.Models.Catalogos
{
    public class CodigoPostalViewModel
    {
        public int CodigoPostalID { get; set; }
        public string CodigoPostal1 { get; set; }
        public Nullable<int> MunicipioID { get; set; }
        public Nullable<int> CiudadID { get; set; }
        public string Asentamiento { get; set; }
        public string TipoAsentamiento { get; set; }
        public string Zona { get; set; }
        public Nullable<System.DateTime> FechaUltimaModificacion { get; set; }
        public Nullable<int> UsuarioID { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> EstadoID { get; set; }
        public Nullable<int> PaisID { get; set; }
    }
}