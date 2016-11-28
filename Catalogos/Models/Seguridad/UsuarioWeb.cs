using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Catalogos.Models.Intranet;

namespace Catalogos.Models.Seguridad
{
    public class UsuarioWeb
    {
        public spObtenerUsuario_Result Usuario { get; set; }
        public List<spObtenerUsuarioFunciones_Result> UsuarioFunciones { get; set; }
    }
}